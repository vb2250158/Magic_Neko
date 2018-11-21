using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// 触发器组件
/// </summary>
public class TriggerComponent : MonoBehaviour {



    /// <summary>
    /// 目标标签
    /// </summary>
    public List<string> TargetsTag;
    /// <summary>
    /// 碰撞器
    /// </summary>
    private Collider2D c2d;
    void Awake() {
        c2d = GetComponent<Collider2D>();
        c2d.isTrigger = true;
    }


    /// <summary>
    /// 当被进入触发器时
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other) {
        
        //标签查找
        if (TargetsTag.Find(delegate (string tag) {return other.transform.tag == tag; })!=null)
        {

            //触发容器内所有动作
            OnTriggerEnterEvent(other);

        }

    }
    /// <summary>
    /// 退出触发器时
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {

        //标签查找
        if (TargetsTag.Find(delegate (string tag) { return other.transform.tag == tag; }) != null)
        {
            //触发容器内所有动作
            try
            {

                OnTriggerExitEvent(other);
            }
            catch (System.Exception)
            {


            }
           
        }
    }
    /// <summary>
    /// 触发事件容器
    /// </summary>
    public event OnTrigger OnTriggerEnterEvent;

    public event OnTrigger OnTriggerExitEvent;

}

public delegate void OnTrigger(Collider2D other);

