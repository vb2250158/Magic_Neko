using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    /// <summary>
    /// 材料
    /// </summary>
    stuff,
    /// <summary>
    /// 消耗品
    /// </summary>
    expendable,
    /// <summary>
    /// 功能物品
    /// </summary>
    functional,
    /// <summary>
    /// 货币
    /// </summary>
    currency
}

/// <summary>
/// 物品数据
/// </summary>
[System.Serializable]
public class ItemData
{
    /// <summary>
    /// 物品的名字
    /// </summary>
    public string name = "";
    /// <summary>
    /// 物品的ID
    /// </summary>
    public int id;
    /// <summary>
    /// 物品的类型
    /// </summary>
    public ItemType type;
    /// <summary>
    /// 物品的说明
    /// </summary>
    [Multiline]
    public string explain;
    /// <summary>
    /// 物品的功能设置
    /// </summary>
    public ItemFunction functionSetting;
    /// <summary>
    ///  物品的图片
    /// </summary>
    public Sprite picture;
    /// <summary>
    /// 占用负重
    /// </summary>
    public float weight;
    /// <summary>
    /// 物品使用冷却时间
    /// </summary>
    public float CD;


}

/// <summary>
/// 物品功能设置
/// </summary>
[System.Serializable]
public class ItemFunction
{

    public List<CreateObjectSetting> CreateObject;
}
/// <summary>
/// 物品使用时创建物列表
/// </summary>
[System.Serializable]
public class CreateObjectSetting{
    public ActionObject actionObject;
    public Vector3 vector3;
}

/// <summary>
/// 道具物品组件
/// </summary>
[System.Serializable]
public class Item : MonoBehaviour
{

    public KnapsackItem knapsackItemData;
    




}
