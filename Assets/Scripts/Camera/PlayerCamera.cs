using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 大小限制值数据
/// </summary>
[System.Serializable]
public class ClampFloat
{
    public float Min;
    public float Max;
}

/// <summary>
/// 摄像机变化设置
/// </summary>
[System.Serializable]
public class CameraChangeSetting
{
    /// <summary>
    /// 摄像机移动目标位置，只有x，y轴有效
    /// </summary>
    public Vector3 focusPosition;
    /// <summary>
    /// 移动到目标视野大小
    /// </summary>
    public float focusFieldSize;

    /// <summary>
    /// 焦点便宜位置
    /// </summary>
    public Vector3 deviationPosition;

    /// <summary>
    /// 摄像机移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 摄像机的视野变化速度
    /// </summary>
    public float FieldSpeed;

}

/// <summary>
/// 玩家摄像机
/// </summary>
public class PlayerCamera : MonoBehaviour
{


    /// <summary>
    /// 焦点单位
    /// </summary>
    public GameObject[] FocalUnit;
    /// <summary>
    /// 视角限制设置
    /// </summary>
    public ClampFloat fieldSize;
    /// <summary>
    /// 动态效果摄像机设置
    /// </summary>
    public CameraChangeSetting changeSetting;

    /// <summary>
    /// 玩家
    /// </summary>
    public Player MainPlayer { get; set; }
    /// <summary>
    /// 主摄像机
    /// </summary>
    public Camera MainCamera { get; set; }
    /// <summary>
    /// 战斗状态
    /// </summary>
    public bool Battle;
    // Use this for initialization
    void Start()
    {
        MainCamera = GetComponent<Camera>();
        FindPlayer();
        StartCoroutine(DynamicFocusSystem());
      
    }


    void Update()
    {
  
        //视角大小改变
        if (changeSetting.focusFieldSize != MainCamera.orthographicSize)
        {

            MainCamera.orthographicSize = Mathf.Clamp(
                GameMahtf.ToTargetValue(MainCamera.orthographicSize,changeSetting.focusFieldSize,Time.deltaTime*10 ),
                fieldSize.Min,
                fieldSize.Max
                );

        }

        //焦点位置改变
        if (Mathf.Abs(changeSetting.focusPosition.x - transform.position.x)> 0 ||
            Mathf.Abs(changeSetting.focusPosition.y - transform.position.y) > 0
            )
        {
            //得出摄像机需要位移的方向          
            Vector3 velocity =((changeSetting.focusPosition - transform.position)+ changeSetting.deviationPosition).normalized * changeSetting.MoveSpeed;

         
            transform.position += new Vector3(velocity.x, velocity.y, 0)*Time.deltaTime;
        }

       
    }



    /// <summary>
    /// 动态焦点系统，可以每秒刷新一下焦点位置
    /// 玩家角色围绕焦点群的模式
    /// </summary>
    /// <returns></returns>
    IEnumerator DynamicFocusSystem()
    {

        while (true)
        {
            //逻辑思路
            //1、获取所有焦点单位
            //2、计算出焦点单位的位置折中值


            //第一步
            FindFocalUnit();

            //第二步
            Vector3 position = new Vector3();
            foreach (var item in FocalUnit)
            {
                position += item.transform.position;
            }
            //得出平均值,赋值给当前要移动到的位置
            changeSetting.focusPosition = position / FocalUnit.Length;
            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// 寻找焦点单位
    /// </summary>
    public void FindFocalUnit()
    {
        //寻找怪物
        FocalUnit = GameObject.FindGameObjectsWithTag("Mosnter");

    }

    /// <summary>
    /// 寻找玩家
    /// </summary>
    public void FindPlayer()
    {

        MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
