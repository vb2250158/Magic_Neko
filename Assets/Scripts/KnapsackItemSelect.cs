using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品拖拽事件触发
/// </summary>
public class KnapsackItemSelect : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    KnapsackItemUI itemUI;



    public void OnPointerDown(PointerEventData eventData)
    {
        DragStart();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragToTarget();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        ExitTarget();
    }


    // Use this for initialization
    void Start()
    {
        itemUI = GetComponent<KnapsackItemUI>();

    }
    /// <summary>
    /// 开始初始化拖拽
    /// </summary>
    public void DragStart()
    {
        ItemUILogic.IsDragEnd = false;
        ItemUILogic.SelectedUI = itemUI;

    }
    /// <summary>
    /// 拖拽到目标设置
    /// </summary>
    public void DragToTarget()
    {

        ItemUILogic.Target = itemUI;
    }

    /// <summary>
    /// 退出目标
    /// </summary>
    public void ExitTarget()
    {

        ItemUILogic.Target = null;
    }


    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void DragEnd()
    {
        ItemUILogic.IsDragEnd = true;
    }

}
