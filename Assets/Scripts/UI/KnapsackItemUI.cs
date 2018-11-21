using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
/// <summary>
/// 物品显示样式
/// </summary>
[System.Serializable]
public class ItemStyle
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType itemType;
    /// <summary>
    /// 该类型物品的蒙版背景色
    /// </summary>
    public Sprite sprite;
}





//物品格子UI显示
public class KnapsackItemUI : MonoBehaviour
{
    /// <summary>
    /// 格子的索引
    /// </summary>
    public int Index;

    /// <summary>
    /// 物品图片
    /// </summary>
    public Image image;
    /// <summary>
    /// 物品数量
    /// </summary>
    public Text Number;
    /// <summary>
    /// 格子物品数据
    /// </summary>
    public KnapsackItem KnapsackItemData;
    /// <summary>
    /// 物品类型区别的图片
    /// </summary>
    public Image ItemTypeImage;
    /// <summary>
    /// 样式列表
    /// </summary>
    public List<ItemStyle> itemStyles;

    /// <summary>
    /// 有很大的优化空间
    /// </summary>
    private void OnGUI()
    {


        GUIItemUpdate();


    }
    private void Awake()
    {
        ItemGroup = GetComponent<CanvasGroup>();

    }
    /// <summary>
    /// 事件是否有效的控制
    /// </summary>
    public CanvasGroup ItemGroup { get; set; }


    /// <summary>
    /// 物品显示根据数据更新
    /// </summary>
    public void GUIItemUpdate()
    {


        //无物品
        if (KnapsackItemData.itemData.name == "" && image.sprite != null)
        {

            Number.text = "";
            image.sprite = null;
            ItemTypeImage.sprite = null;

        }
        //如果有物品
        else if (KnapsackItemData.itemData.name != "")
        {
 

            //功能物品
            if (KnapsackItemData.itemData.type == ItemType.functional)
            {
                if (Number.text != "")
                {
                    Number.text = "";
                }


            }
            //其他
            else
            {
                if (Number.text != KnapsackItemData.Number + "")
                {
                    Number.text = KnapsackItemData.Number + "";
                }

            }
            //图片校正
            if (image.sprite != KnapsackItemData.itemData.picture)
            {
                //显示图片校正
                image.sprite = KnapsackItemData.itemData.picture;
                //背景遍历样式表
                ItemStyle style = itemStyles.Find((ItemStyle itemtype) =>
                {

                    return KnapsackItemData.itemData.type == itemtype.itemType;
                });
                //如果样式存在
                if (style != null)
                {
                    //样式赋值
                    ItemTypeImage.sprite = style.sprite;
                }
            }
        }
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="knapsackItem"></param>
    public void SetItem(KnapsackItem knapsackItem)
    {
        //  Debug.Log(knapsackItem);
        this.KnapsackItemData = knapsackItem;

    }

   
}
