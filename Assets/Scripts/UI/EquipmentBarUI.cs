using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品装备栏UI
/// </summary>
public class EquipmentBarUI : MonoBehaviour
{
    /// <summary>
    /// 要呈现数据的背包
    /// </summary>
    public Knapsack knapsack;
    /// <summary>
    /// 要显示装备道具
    /// </summary>
    public EquipmentItems EquipmentData;

    /// <summary>
    /// 显示的格子
    /// </summary>
    public List<KnapsackItemUI> ItemUIs;
    /// <summary>
    /// 格子自适应UI
    /// </summary>
    private GridAutoUI gridAutoUI;

    /// <summary>
    /// 是否进行数据实时刷新
    /// </summary>
    public bool OpenUpdate = true;

    /// <summary>
    /// 装备栏UI预制体
    /// </summary>
    public KnapsackItemUI knapsackUIPrf;


    private void Start()
    {
        gridAutoUI = GetComponent<GridAutoUI>();
        gridAutoUI.ContentSizeUpdate(10);
        SetData(knapsack.knapsackData.equipmentItems);

    }


    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="items"></param>
    public void SetData(EquipmentItems items)
    {

        EquipmentData = items;

    }
    //窗口大小改变时改变容器大小
    private void OnRectTransformDimensionsChange()
    {
        if (gridAutoUI != null)
        {

            gridAutoUI.ContentSizeUpdate(10);
        }

    }
    private void Update()
    {
        if (OpenUpdate)
        {
            //实时刷新
            EquipmentBarUIUpdate();
        }
    }


    /// <summary>
    /// 装备栏UI刷新
    /// </summary>
    private void EquipmentBarUIUpdate()
    {
        //UI补充
        while (ItemUIs.Count != EquipmentData.container.list.Count)
        {

            ItemUIs.Add(GameObject.Instantiate(knapsackUIPrf, transform).GetComponent<KnapsackItemUI>());

        }

        //存在的物品遍历
        for (int i = 0; i < ItemUIs.Count; i++)
        {
            try
            {
                ItemUIs[i].SetItem(EquipmentData.container.list[i]);
            }
            catch (System.Exception)
            {

                Debug.Log(ItemUIs[i]);
            }

        }

    }

}
