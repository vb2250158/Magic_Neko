using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class DisplayItemSettings
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType itemType;
    /// <summary>
    /// 该类型的字体设置
    /// </summary>
    public DisplayItem displayPreform;
}

/// <summary>
/// 附近物品名字显示控制器
/// </summary>
public class EnclosureItemUI : MonoBehaviour {
    /// <summary>
    /// 显示预制体
    /// </summary>
    public List<DisplayItemSettings> ItemDisplayList;
    /// <summary>
    /// 物品与物品显示UI对照字典
    /// </summary>
    public Dictionary<Item, DisplayItem> keyValuePairs = new Dictionary<Item, DisplayItem>();
    private void Awake()
    {
        WorldTree.worldUI.enclosureItemUI = this;
    }


    /// <summary>
    /// 创建一个物品显示
    /// </summary>
    public DisplayItem DisplayEnclosureItem(Item content, ItemType itemType = ItemType.stuff)
    {

        DisplayItem displayItem = null;

        //遍历配置信息
        foreach (var item in ItemDisplayList)
        {
            if (item.itemType == itemType)
            {
                //创建文字
                displayItem = Instantiate(item.displayPreform, transform);
            }

        }
        //初始化UI位置
        displayItem.transform.position = content.transform.position + Vector3.back + (Vector3.up * 2);
        //设置显示信息
        displayItem.DisplayItemAwake(content);
        //添加
        keyValuePairs.Add(content,displayItem);
        return displayItem;
    }
  



    /// <summary>
    /// 删除一个物品显示
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool DeleteEnclosureItem(Item item) {
        if (keyValuePairs.ContainsKey(item))
        {
            GameObject.Destroy(keyValuePairs[item].gameObject);

            keyValuePairs.Remove(item);
            return true;
        }
        else
        {
            return false;
        }



      
    }

}
