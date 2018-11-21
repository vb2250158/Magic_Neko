using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 物品丢弃范围UI
/// </summary>
public class DiscardUI : MonoBehaviour, IDropHandler
{


    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void DragEnd()
    {
        ItemUILogic.IsDragEnd = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragEnd();
    }
}
