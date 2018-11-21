using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// 向量与浮点数
/// </summary>
[System.Serializable]
public class VectorAndFlaot {
    public Vector3 vector;
    public float value;
}


/// <summary>
/// 动作创建物,带有自动销毁机制
/// </summary>
public class ActionObject : MonoBehaviour {
    /// <summary>
    /// 对象起始速度
    /// </summary>
    public Vector3 Speed;
    /// <summary>
    /// 向量根据时间变化
    /// </summary>
    public List<VectorAndFlaot> vectorByTime;
    /// <summary>
    /// 存在时间
    /// </summary>
    public float LifeTime=1;
    /// <summary>
    /// 是否开启计时器
    /// </summary>
    public bool OpenTimer=true;
    /// <summary>
    /// 近战判断击中时创作者被控时间
    /// </summary>
    public float CreatorControl;
    /// <summary>
    /// 创作者
    /// </summary>
    public Role Creator { get; set; }

    /// <summary>
    /// 是否从父级脱离
    /// </summary>
    public bool BreakAway;


    private void Awake()
    {
        //从父级脱离
        if (BreakAway)
        {
            transform.SetParent(transform.parent.parent);
        }
    }

    void FixedUpdate() {

        transform.position += Speed*Time.deltaTime;
        //循环遍历变速计时
        foreach (var item in vectorByTime)
        {
            item.value -= Time.deltaTime;
            if (item.value<=0)
            {
                vectorByTime.Remove(item);
                Speed = item.vector;
                break;
            }
        }

        //消亡计时
        if (OpenTimer)
        {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
     
    }
}
