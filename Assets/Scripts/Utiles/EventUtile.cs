using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventUtile  {

    /// <summary>
    /// 给一个对象添加EventTrigger事件
    /// </summary>
    /// <param name="TargetObject"></param>
    /// <param name="eventType">事件类型</param>
    /// <param name="function">事件列表</param>
    public void AddEventTrigger(Transform TargetObject, EventTriggerType eventType, UnityAction<BaseEventData> function)
    {
        //获得事件组件
        EventTrigger eventTrigger = TargetObject.GetComponent<EventTrigger>();
        if (eventTrigger!=null)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry()
            {
                eventID = eventType
            };

            entry.callback.AddListener(function);
            eventTrigger.triggers.Add(entry);
        }
        else
        {
            Debug.Log("找不到事件组件");
        }

     
    }
}
