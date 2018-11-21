using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
/// <summary>
/// 作战单位的基础属性，不算任何加成下的属性
/// </summary>
[System.Serializable]
public class CombatUnitData
{
    /// <summary>
    /// 生命值
    /// </summary>
    public LimitInt HP;
    /// <summary>
    /// 受击抗性
    /// </summary>
    public int HitResistant = 1;
    /// <summary>
    /// 移动速度，100为正常值
    /// </summary>
    public float MoveSpeed = 100;




}



public delegate void CombatUnitDie();
/// <summary>
/// 战斗角色单位，包含了战斗单位的基本数据
/// 角色的受击核心逻辑
/// 战斗系统的核心组件
/// </summary>
[RequireComponent(typeof(Role))]
[RequireComponent(typeof(Hit))]
public class CombatUnit : MonoBehaviour
{
    /// <summary>
    /// 作战单位数据
    /// </summary>
    public CombatUnitData combatUnitData;

    /// <summary>
    /// 生命值
    /// </summary>
    public LimitInt HP;
 

    /// <summary>
    /// 角色
    /// </summary>
    public Role TheRole { get; set; }
    /// <summary>
    /// 受击组件
    /// </summary>
    private Hit hit;


    /// <summary>
    /// 攻击状态Buff
    /// </summary>
    public Buff AttackBuff { get; set; }

    /// <summary>
    /// 受击字体偏移
    /// </summary>
    public Vector3 HitTextOffset;

    /// <summary>
    /// 死亡更新事件
    /// </summary>
    public event CombatUnitDie DieUpdate;
    /// <summary>
    /// 死亡开始事件
    /// </summary>
    public event CombatUnitDie DieStart;
    /// <summary>
    /// 实际移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 是否已经走向死亡!
    /// </summary>
    public bool IsDie { get; set; }
    void Awake()
    {


        //初始化移动速度
        MoveSpeed = combatUnitData.MoveSpeed;


        //初始化
        TheRole = GetComponent<Role>();
        hit = GetComponent<Hit>();
        jump = GetComponent<Jump>();




        AttackBuff = new Buff
        {
            name = "attack"
        };

        //定义普通攻击状态Buff
        AttackBuff.OnAdd += delegate (Role role)
        {
            //该buff是视动画播放时间而定的所以99
            AttackBuff.Timer = 99;
          

            if (jump.hopping == HoppingState.OnTheGround)
            {

                //移动速度减少
                MoveSpeed = combatUnitData.MoveSpeed * AttackMoveDebuff;
            }

            //关闭自动的动作动画切换
            role.BeasAni = false;
        };
        AttackBuff.OnUpdate += delegate (Role role)
        {
            // Debug.Log(role.UnityArmature.animation.lastAnimationState.currentTime / role.UnityArmature.animation.lastAnimationState.totalTime);
            if (role.UnityArmature.animation.lastAnimationState.currentTime / role.UnityArmature.animation.lastAnimationState.totalTime > (9f / 10))
            {

                AttackBuff.Timer = 0;
            }
        };
        AttackBuff.OnRemove += delegate (Role role)
        {
            //恢复角色状态
            role.BeasAni = true;
            MoveSpeed = combatUnitData.MoveSpeed;
            //   role.StopAction(role.UnityArmature.animation.lastAnimationName);
        };


        //该角色单位的受击逻辑

        //受击计算开始固有逻辑
        hit.Start += delegate (FightTrigger trigger)
        {
        };
        //受击计算时固有逻辑,受击逻辑核心代码-----------------------------------------------------------------------------------------------------------------------
        hit.Stay += delegate (FightTrigger trigger)
        {
            // Debug.Log("受击");
            //该战斗角色的受击逻辑
            //受控时间，实际受控时间=原受控时间-（抗性/（抗性+递增临界值））
            hit.HitBuff.Timer = trigger.HitTime - (combatUnitData.HitResistant / (combatUnitData.HitResistant + Hit.THRESHOLD));
            //播放受击动画
            TheRole.AddAction("hit", 1);
            //添加受击控制buff
            TheRole.AddBuff(hit.HitBuff);

            //受击伤害显示
            trigger.DisplayFightHitText(transform.position + HitTextOffset + Vector3.back, trigger.damage);
            HP.SetThe(HP.The - trigger.damage);

            //战斗触发器的逻辑
            //帧停操作
            trigger.RoleSotp();

        };
        //受击计算时固有逻辑,受击逻辑核心代码-----------------------------------------------------------------------------------------------------------------------
        //受击计算后固有逻辑
        hit.End += delegate (FightTrigger trigger)
        {
        };


    }


    private void Update()
    {
        //如果生命值小于0就会触发死亡事件
        if (HP.The <= 0)
        {
            try
            {
                DieUpdate();
            }
            catch (System.Exception)
            {

                Debug.Log("没有定义死亡事件");
            }
            if (!IsDie)
            {
                IsDie = true;
                //TheRole.BeasAni = false;
                DieStart();

            }

        }

    }


    /// <summary>
    /// 上半身骨头名字
    /// </summary>
    public string headName;
    /// <summary>
    /// 跳跃组件
    /// </summary>
    public Jump jump { get; set; }
    /// <summary>
    /// 攻击动画名字
    /// </summary>
    public string AttackAniName = "attack";

    /// <summary>
    /// 移动攻击速度
    /// </summary>
    public float AttackMoveDebuff = 0.5f;
    /// <summary>
    /// 普通攻击索引方法
    /// </summary>
    public void Attack()
    {
        if (jump.hopping == HoppingState.OnTheGround)
        {
            TheRole.AddBuff(AttackBuff);
            TheRole.AddAction(AttackAniName, 1);
        }
        else
        {

            TheRole.AddBuff(AttackBuff);
            DragonBones.AnimationState animationState = TheRole.UnityArmature.animation.FadeIn(AttackAniName, -1, 1);
            animationState.AddBoneMask(headName);
        }

    }



    /// <summary>
    /// 是否攻击准备就绪
    /// </summary>
    /// <returns></returns>
    public bool AttackReady()
    {
      //  Debug.Log(TheRole.IsControl && !TheRole.JudgeBuff("attack"));
        return TheRole.IsControl && !TheRole.JudgeBuff("attack");
    }

    /// <summary>
    /// 获得移动速度(移动速度换算公式)
    /// </summary>
    /// <param name="AxisX"></param>
    /// <returns></returns>
    public float GetMoveSpeed(float AxisX) {
        return AxisX * 0.1f * MoveSpeed;
    }
}
