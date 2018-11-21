using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

/// <summary>
/// 角色移动、待机demo
/// @秋雨之忆
/// </summary>
public class RoleDemo : MonoBehaviour {

    /// <summary>
    /// 移动动画名字
    /// </summary>
    public string move;
    /// <summary>
    /// 待机动画名字
    /// </summary>
    public string steady;
    /// <summary>
    /// 角色移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 龙骨动画组件
    /// </summary>
    private UnityArmatureComponent armatureComponent;

 
    // 组件初始化
    void Start () {
        armatureComponent = GetComponent<UnityArmatureComponent>();

    }
	
	// 每帧更新逻辑
	void Update () {
        //基于数据来驱动逻辑
        //只要速度不为0就位移
        if (moveSpeed != 0)
        {
            //判断当前是否不在播放移动动画
            if (armatureComponent.animation.lastAnimationName != move)
            {
                //没有播放移动动画就进行播放
                armatureComponent.animation.Play(move);
            }
            //位移,transform组件是默认有的，可以不通过GetComponent<Transform>();就可以使用
            transform.position += Vector3.right * moveSpeed*0.1f;

            //flipX==true是翻转状态，flipX==false是不翻转状态

            //判断是否需要转身,条件是角色速度大于0并且角色翻转过了
            if (moveSpeed>0&&armatureComponent.armature.flipX)
            {
                armatureComponent.armature.flipX = false;
            }
            //因为是向左走，没翻转则进行翻转
            else if (moveSpeed < 0&&!armatureComponent.armature.flipX)
            {
                armatureComponent.armature.flipX = true;
            }

        }
        else
        {
            //检测是否在播放待机动画
            if (armatureComponent.animation.lastAnimationName != steady)
            {
                armatureComponent.animation.Play(steady);
            }
        }
    }
}
