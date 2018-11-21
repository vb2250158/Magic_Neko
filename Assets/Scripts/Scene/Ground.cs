using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面组件
/// </summary>
public class Ground : MonoBehaviour {

    /// <summary>
    /// 碰撞器
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other) {
        Jump jump = null;
        if (jump=other.collider.GetComponent<Jump>())
        {
            jump.OnGoundEnter();
        }
    }

}
