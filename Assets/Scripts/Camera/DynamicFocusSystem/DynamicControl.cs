using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicControl : MonoBehaviour {

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




    }

    /// <summary>
    /// 镜头缩放||扩展
    /// </summary>
    private ChangePattern changePattern;
}
