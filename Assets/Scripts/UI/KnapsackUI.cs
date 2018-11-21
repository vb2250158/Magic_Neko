using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




/// <summary>
/// 背包UI
/// </summary>
public class KnapsackUI : MonoBehaviour
{
    

    /// <summary>
    /// 要呈现数据的背包
    /// </summary>
    public Knapsack knapsack;
    /// <summary>
    /// 装备物品UI
    /// </summary>
    public EquipmentBarUI equipment;
    /// <summary>
    /// 存放物品UI
    /// </summary>
    public ContainerUI container;
    /// <summary>
    /// 装备物品整理UI
    /// </summary>
    public EquipmentArrangementUI equipmentArrangementUI;

    void Start()
    {
        KnapsackUIUpdate();
        container.backpackContainer = knapsack.knapsackData.container;
    }

    /// <summary>
    /// 是否开启GUI实时刷新
    /// </summary>
    /// <param name="on"></param>
    public void SetGUIUpdate(bool on)
    {
        equipment.OpenUpdate = on;
        container.GUIUpdate = on;
    }

    /// <summary>
    /// 更新装备物品的UI绑定
    /// </summary>
    void KnapsackUIUpdate()
    {

        equipment.SetData(knapsack.knapsackData.equipmentItems);
        equipmentArrangementUI.EquipmentBar.SetData(knapsack.knapsackData.equipmentItems);
    }

   
}
