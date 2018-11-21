using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通摄像机
/// </summary>
public class CommonCamera : MonoBehaviour
{
    public Transform target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((target.position - transform.position).sqrMagnitude > 0)
        {
            transform.position = GameMahtf.ToTargetValue(transform.position, target.position, new Vector3(Mathf.Abs((target.position - transform.position).x), Mathf.Abs((target.position - transform.position).y)) * Time.deltaTime);
        }


    }


}
