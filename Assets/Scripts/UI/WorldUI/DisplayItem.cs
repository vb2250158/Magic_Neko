using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 显示的物品的文字设置
/// </summary>
public class DisplayItem : MonoBehaviour
{

    public Text text;
    /// <summary>
    /// 物品
    /// </summary>
    public Item content;
    void Awake()
    {

    }

    private void OnGUI()
    {
        //UI位置校正
        if (transform.position != content.transform.position + Vector3.back + (Vector3.up *2))
        {
            transform.position = content.transform.position + Vector3.back + (Vector3.up * 2);
        }

    }

    /// <summary>
    /// 显示信息初始化
    /// </summary>
    /// <param name="content"></param>
    public void DisplayItemAwake(Item content)
    {
        this.content = content;

        text.text = content.knapsackItemData.itemData.name;
    }

}
