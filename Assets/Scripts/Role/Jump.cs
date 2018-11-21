using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跳跃数据
/// </summary>
[System.Serializable]
public class JumpData
{

}

/// <summary>
/// 跳跃状态
/// </summary>
public enum HoppingState
{
    /// <summary>
    /// 在地面
    /// </summary>
    OnTheGround,
    /// <summary>
    /// 上升
    /// </summary>
    Rise,
    /// <summary>
    /// 下降
    /// </summary>
    Decline
}



/// <summary>
/// 跳跃组件，该组件基于角色组件,
/// 使用该组件后会受世界重力、环境重力影响
/// </summary>
[RequireComponent(typeof(Role))]
public class Jump : MonoBehaviour
{

    /// <summary>
    /// 跳跃数据
    /// </summary>
    public JumpData jumpData;
    /// <summary>
    /// 跳跃组件状态
    /// </summary>
    public HoppingState hopping;
    internal void OnGoundEnter()
    {
        //状态切换为在地面
        hopping = HoppingState.OnTheGround;
        //恢复跳跃次数
        frequencyRemain = frequencyMax;
        //停止播放跳跃动画
        TheRole.UnityArmature.animation.Stop(JumpName);
    }

    /// <summary>
    /// 跳跃力
    /// </summary>
    public float force = 100;

    /// <summary>
    /// 最大跳跃次数
    /// </summary>
    public int frequencyMax = 1;

    /// <summary>
    /// 跳跃动画名字
    /// </summary>
    public string JumpName;


    /// <summary>   
    /// 当前剩余跳跃次数
    /// </summary>
    public int frequencyRemain { get; set; }

    /// <summary>
    /// 下落速度倍率
    /// </summary>
    public float FallingSpeed { get; set; }
    /// <summary>
    /// 下落环境影响率
    /// </summary>
    public float FallingEnvironmetal { get; set; }
    /// <summary>
    /// 当前的物理组件
    /// </summary>
    private Rigidbody2D R2d;
    /// <summary>
    /// 当前角色
    /// </summary>
    public Role TheRole { get; set; }


    void Start()
    {
        TheRole = GetComponent<Role>();
        R2d = GetComponent<Rigidbody2D>();
        //下落倍率，环境影响重力默认为1
        FallingEnvironmetal = FallingSpeed = 1f;
        //跳跃次数初始化
        frequencyRemain = frequencyMax;
    }

    void Update()
    {

        //重力下落速度倍率效正
        if (FallingSpeed * FallingEnvironmetal * WorldTree.parameters.gravity != R2d.gravityScale)
        {
            R2d.gravityScale = FallingSpeed * FallingEnvironmetal * WorldTree.parameters.gravity;
        }



        //检查速度是否为在空中下落
        if (hopping==HoppingState.Rise && R2d.velocity.y < 0)
        {
            
            hopping = HoppingState.Decline;
        }



        //空中动画更新
        if (hopping == HoppingState.Rise&&TheRole.GetActionTime()>1f/2)
        {
            TheRole.UnityArmature.animation.GotoAndPlayByProgress(JumpName, 1f / 4, 0);
        }
        else if (hopping == HoppingState.Decline && TheRole.GetActionTime() > 7f / 8)
        {
            TheRole.UnityArmature.animation.GotoAndPlayByProgress(JumpName, 1f / 2, 0);
        }

        
      



        if (Input.GetKeyDown(KeyCode.K))
        {
            JumpAction();
        }
    }



    /// <summary>
    /// 跳跃动作，会减少跳跃次数，无次数时跳跃失败
    /// </summary>
    /// <returns></returns>
    public bool JumpAction()
    {
        if (frequencyRemain <= 0)
        {
            return false;
        }
        else if(!TheRole.JudgeBuff("attack"))
        {
            JumpUp();

            frequencyRemain--;
            return true;
        }
        else
        {

            return false;
        }

    }

    /// <summary>
    /// 跳起来
    /// </summary>
    public void JumpUp()
    {
        //切换为上升状态
        hopping = HoppingState.Rise;
        //播放动画
        TheRole.AddAction(JumpName, 1);

        //施加一个力
        R2d.AddRelativeForce(force * Vector2.up * 10);
        //重置向上速度
        R2d.velocity= new Vector2(R2d.velocity.x, 0);


    }

}
