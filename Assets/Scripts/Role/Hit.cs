using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public delegate void OnHit(FightTrigger gameObject);


/// <summary>
/// 受击组件，包含了给受击者提供了事件绑定的接口
/// </summary>
public class Hit : MonoBehaviour
{
    /// <summary>
    /// 受击僵直阈值
    /// </summary>
    public const int THRESHOLD = 100;
    /// <summary>
    /// 是否进行击中判定
    /// </summary>
    public bool IsOn = true;
    /// <summary>
    /// 受击buff定义
    /// </summary>
    public Buff HitBuff { get; set; }
    /// <summary>
    /// 生命值
    /// </summary>
    public LimitInt HP { get; set; }

    /// <summary>
    /// 击中前
    /// </summary>
    public event OnHit Start;//=new OnHitStart(s);
    /// <summary>
    /// 击中时
    /// </summary>
    public event OnHit Stay;// = new OnHit(s);
    /// <summary>
    /// 击中后
    /// </summary>
    public event OnHit End;//= new OnHitEnd(s);
    

    void Awake()
    {
        //定义受击buff名字
        HitBuff = new Buff
        {
            name = "hit"
        };

        //初始化受击BUFF逻辑,该BUFF是给战斗
        HitBuff.OnAdd += delegate (Role role)
        {
            role.PlaySpeed = 1;

        };
        HitBuff.OnUpdate += delegate (Role role)
        {
            //受击动画减缓
            if (role.UnityArmature.animation.lastAnimationState.currentTime / role.UnityArmature.animation.lastAnimationState.totalTime > (1f / 2))
            {
                role.PlaySpeed = 0.05f;
            }
        };
        HitBuff.OnRemove += delegate (Role role)
        {
            role.PlaySpeed = 1;
        };
    }

    /// <summary>
    /// 受击逻辑顺序开始
    /// </summary>
    /// <param name="triggerComponent"></param>
    public void On(FightTrigger triggerComponent)
    {

        this.Start(triggerComponent);


        if (IsOn)
        {
            this.Stay(triggerComponent);
        }
        this.End(triggerComponent);


    }

}
