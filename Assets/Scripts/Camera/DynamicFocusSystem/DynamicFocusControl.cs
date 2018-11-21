using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFocusControl : MonoBehaviour
{
    /// <summary>
    /// 战斗状态
    /// </summary>
    public bool Battle;

    //内外临界值
    public float within = 0.8f;
    public float abroad = 0.9f;
    /// <summary>
    /// 缩放速度倍率
    /// </summary>
    public float ScalingRatio = 1;
    public DynamicFocusSystem FocusSystem { get; set; }
    // Use this for initialization
    void Start()
    {
        FocusSystem = GetComponent<DynamicFocusSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(MainCamera.worldto(player.transform.position));
        CombatFocusUpdate();
    }

    /// <summary>
    /// 战斗焦点位置更新
    /// </summary>
    public void CombatFocusUpdate()
    {
        //目标位置校正
        Vector3 position = new Vector3();
        //求出焦点平均位置
        foreach (var item in FocusSystem.focalList)
        {

            position += item.transform.position;
        }
        //得出平均值,赋值给当前要移动到的位置
        FocusSystem.changeSetting.focusPosition = position / FocusSystem.focalList.Count;




        //算法：获得离摄像机最远的单位
        float MaxViewport = 0;
        //遍历所有焦点单位
        foreach (var item in FocusSystem.focalList)
        {

            if (MaxViewport < Mathf.Abs(FocusSystem.MainCamera.WorldToViewportPoint(item.transform.position).x))
            {
                MaxViewport = Mathf.Abs(FocusSystem.MainCamera.WorldToViewportPoint(item.transform.position).x);
            }
        }



        //判断是否需要开启摄像头的平滑移动轴
        if (MaxViewport < within)
        {
            SmoothFocalTimer = 1;
            changePattern = ChangePattern.Shrink;

        }
        else if (MaxViewport > abroad)
        {
            SmoothFocalTimer = 1;
            changePattern = ChangePattern.Expand;
        }



        //平滑缩放焦点
        //收缩
        if (SmoothFocalTimer > 0 && ChangePattern.Shrink == changePattern)
        {
            FocusSystem.changeSetting.focusFieldSize = Mathf.Clamp(
                FocusSystem.changeSetting.focusFieldSize - (SmoothFocalTimer *//时间平滑
                    Mathf.Abs((within - MaxViewport))//距离平滑，绝对值(临界值大小-最外层对象轴大小)
                ),
               FocusSystem.fieldSize.Min,
               FocusSystem.fieldSize.Max);
        }
        //扩展
        else if (SmoothFocalTimer > 0 && ChangePattern.Expand == changePattern)
        {
            FocusSystem.changeSetting.focusFieldSize = Mathf.Clamp(
                FocusSystem.changeSetting.focusFieldSize + (SmoothFocalTimer *//时间平滑
                    Mathf.Abs((abroad - MaxViewport) )//距离平滑，绝对值(临界值大小-最外层对象轴大小)
                ),
            FocusSystem.fieldSize.Min,
            FocusSystem.fieldSize.Max);
        }

        SmoothFocalTimer -= Time.deltaTime;
    }
    /// <summary>
    /// 平滑缩放计时器
    /// </summary>
    private float SmoothFocalTimer = 0;
    /// <summary>
    /// 镜头缩放||扩展
    /// </summary>
    private ChangePattern changePattern;
}
