using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 存放物品UI控制
/// </summary>
public class ContainerUI : MonoBehaviour
{

    /// <summary>
    /// 背包库存数据
    /// </summary>
    public BackpackContainer backpackContainer;

    /// <summary>
    /// 显示的格子
    /// </summary>
    public List<KnapsackItemUI> ItemUIs;
    /// <summary>
    /// UI预制体
    /// </summary>
    public KnapsackItemUI itemUIPreform;

    /// <summary>
    /// 显示主体的UI
    /// </summary>
    public GridAutoUI backpackContainerUI;

    /// <summary>
    /// GUI实时刷新是否开启
    /// </summary>
    public bool GUIUpdate = true;

    /// <summary>
    /// 有很大的优化空间
    /// </summary>
    private void OnGUI()
    {
        if (GUIUpdate)
        {
            ContainerUpdate();
        }

    }

    public void ContainerUpdate() {

     //   Debug.Log(backpackContainer.list.Count);
        if (backpackContainer.list.Count!= ItemUIs.Count)
        {
            DisplayUpdate(backpackContainer.list.Count);
        }
        //存在的物品进行遍历显示
        for (int i = 0; i < ItemUIs.Count; i++)
        {
            ItemUIs[i].SetItem(backpackContainer.list[i]);
            ItemUIs[i].Index = i;
        }
    }

    /// <summary>
    /// 物品栏全刷新
    /// </summary>
    public void DisplayUpdate(int cellNumber)
    {
        //所需生成格子刷新
        while (cellNumber != ItemUIs.Count)
        {
            //格子数不足
            if (cellNumber > ItemUIs.Count)
            {
                ItemUIs.Add(GameObject.Instantiate(itemUIPreform, backpackContainerUI.transform).GetComponent<KnapsackItemUI>());
              //  ItemUIs.Add(GameObject.Instantiate(knapsackUIPrf, transform).GetComponent<>());
            }
            else
            {

                //移除多余的格子
                GameObject itemObj = ItemUIs[0].gameObject;
                ItemUIs.Remove(ItemUIs[0]);
                GameObject.Destroy(itemObj);
            }
        }
        //容器更新
        backpackContainerUI.ContentSizeUpdate(cellNumber);


    }

}
