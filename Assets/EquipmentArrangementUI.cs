using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备道具整理UI
/// </summary>
[RequireComponent(typeof(EquipmentBarUI))]
public class EquipmentArrangementUI : MonoBehaviour {
    /// <summary>
    /// 自适应组件
    /// </summary>
    private GridAutoUI gridAutoUI;
    /// <summary>
    /// 自身装备栏UI
    /// </summary>
    public EquipmentBarUI EquipmentBar { get; set; }

    private void Awake()
    {
        gridAutoUI = GetComponent<GridAutoUI>();
        EquipmentBar = GetComponent<EquipmentBarUI>();
    }
    // Use this for initialization
    void Start () {
      
        gridAutoUI.ContentSizeUpdate(10);
    }

    //窗口大小改变时改变容器大小
    private void OnRectTransformDimensionsChange()
    {
        if (gridAutoUI != null)
        {

            gridAutoUI.ContentSizeUpdate(10);
        }

    }
}
