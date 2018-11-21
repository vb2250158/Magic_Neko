using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受击类型，即包含伤害类型
/// </summary>
public enum HitType {
    ordinary, magic
}

/// <summary>
/// 击中特效设置
/// </summary>
[System.Serializable]
public class EffectsSettings {
    /// <summary>
    /// 击中特效
    /// </summary>
    public ActionObject HitEffects;
    /// <summary>
    /// 特效产生位置
    /// </summary>
    public Vector3 EffectsPosition;
}

/// <summary>
/// 战斗触发器,可以设置击中时间，
/// </summary>
[RequireComponent(typeof(TriggerComponent))]
public class FightTrigger : MonoBehaviour
{
    /// <summary>
    /// 击中特效设置
    /// </summary>
    public EffectsSettings effects;
    /// <summary>
    /// 受击类型
    /// </summary>
    public HitType hitType;
    /// <summary>
    /// 受击伤害值
    /// </summary>
    public int damage;
    /// <summary>
    /// 碰到后受击时间
    /// </summary>
    public float HitTime;
    /// <summary>
    /// 触发器组件
    /// </summary>
    private TriggerComponent trigger;
    /// <summary>
    /// 动作停止Buff
    /// </summary>
    private Buff SotpBuff;

    
    /// <summary>
    /// 初始化战斗触发器
    /// </summary>
    void Awake()
    {
        SotpBuff = new Buff
        {
            name = "ActionSotp"
        };

        //动画停止Buff
        SotpBuff.OnAdd += delegate (Role role)
        {
            role.PlaySpeed = 0;
        };

        SotpBuff.OnUpdate += delegate (Role role)
        {

        };
        SotpBuff.OnRemove += delegate (Role role)
        {
            role.PlaySpeed = 1;
        };
        trigger = GetComponent<TriggerComponent>();



        //初始化战斗触发器事件
        trigger.OnTriggerEnterEvent += delegate (Collider2D other)
        {
            Hit hitObject = null;
            //获得被击组件
            if (hitObject = other.GetComponent<Hit>())
            {
                //如果存在击中特效
                if (effects.HitEffects)
                {
                    //击中特效
                    ActionObject Effect = Instantiate(effects.HitEffects);
                    //设置特效位置
                    Effect.transform.position = other.transform.position + effects.EffectsPosition + Vector3.back;
                }
               
                //受击者开始受击逻辑
                hitObject.On(this);
            }
        };

        //尝试获取组件
        actionObject = GetComponent<ActionObject>();
    }
    /// <summary>
    /// 动作创建对象
    /// </summary>
    private ActionObject actionObject;

    /// <summary>
    /// 角色帧停(如果是被制造物的话
    /// </summary>
    public void RoleSotp()
    {
        if (actionObject)
        {
            //SotpBuff.Timer
            SotpBuff.Timer = 0.075f;

            actionObject.Creator.AddBuff(SotpBuff);
        }
    }

    /// <summary>
    /// 显示击中造成的伤害
    /// </summary>
    public void DisplayFightHitText(Vector3 position,int number) {
        WorldTree.worldUI.hitUI.CreateText(position,number+"");
    }
}
