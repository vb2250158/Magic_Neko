using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemColorSettings
{
    public ItemType itemType;
    public Color color;
}


/// <summary>
/// 显示物品信息窗口
/// </summary>
public class DisplayItemWindowUI : MonoBehaviour
{

    public KnapsackItem DisplayItem { get; set; }

    /// <summary>
    /// 名字显示控件
    /// </summary>
    public Text ItemName;
    /// <summary>
    /// 类型显示控件
    /// </summary>
    public Text ItemType;
    /// <summary>
    /// 物品说明控件
    /// </summary>
    public Text ItemExplain;

    /// <summary>
    /// 颜色设置
    /// </summary>
    public List<ItemColorSettings> colorSettings;



    //待优化
    private void Update()
    {
        if (DisplayItem != null)
        {
            ItemName.text = DisplayItem.itemData.name;

            switch (DisplayItem.itemData.type)
            {
                case global::ItemType.stuff:
                    ItemType.text = "材料";
                    break;
                case global::ItemType.expendable:
                    ItemType.text = "消耗品";
                    break;
                case global::ItemType.functional:
                    ItemType.text = "功能物品";
                    break;
                case global::ItemType.currency:
                    ItemType.text = "货币";
                    break;

                default:
                    break;
            }

            //字体颜色变化
            foreach (var item in colorSettings)
            {
                if (item.itemType == DisplayItem.itemData.type)
                {
                    ItemType.color = item.color;

                    break;
                }
            }

            ItemExplain.text = DisplayItem.itemData.explain;


        }

    }
    public void SetDisplayItem(KnapsackItem knapsackItem)
    {

        DisplayItem = knapsackItem;
    }
}
