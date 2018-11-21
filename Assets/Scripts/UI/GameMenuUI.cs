using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GameMenuUI : MonoBehaviour
{
    /// <summary>
    /// 打开菜单事件集
    /// </summary>
    public UnityEvent OpenEvent;
    /// <summary>
    /// 关闭菜单事件集
    /// </summary>
    public UnityEvent CloseEvent;

    private void Start()
    {
       
    }



    /// <summary>
    /// 打开菜单
    /// </summary>
    public void OpenMenu()
    {

        gameObject.SetActive(true);
        OpenEvent.Invoke();
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void CloseMenu()
    {
        gameObject.SetActive(false);
        CloseEvent.Invoke();
    }
}
