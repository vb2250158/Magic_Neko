using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进度条UI
/// </summary>
public class ProgressBarUI : MonoBehaviour
{
    [Range(0, 1)]
    public float value = 1;
    /// <summary>
    /// 棒状条
    /// </summary>
    public RectTransform barLike;
    /// <summary>
    /// 条进度条容器大小
    /// </summary>
    private float vessel;
    /// <summary>
    /// 该组件本身的Transform
    /// </summary>
    private RectTransform rectTransform;
    // Use this for initialization
    void Start()
    {
        //初始化组件
        barLike.anchorMin = Vector2.zero;
        barLike.anchorMax = new Vector2(1, 1);
        barLike.offsetMax = Vector2.zero;
        barLike.offsetMin = Vector2.zero;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //算法进度条的当前max.x=该组件的宽度*n
        //n=max.x/该组件的宽度
        if ((barLike.offsetMax.x / rectTransform.rect.width)+1!= value)
        {
            barLike.offsetMax = new Vector2((rectTransform.rect.width * value) - rectTransform.rect.width, barLike.offsetMax.y);
        }

    }
}
