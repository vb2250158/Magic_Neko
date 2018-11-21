using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选中项数据
/// </summary>
[System.Serializable]
public class SelectItemIndex
{
    /// <summary>
    /// 选中的组索引
    /// </summary>
    public int SelectListIndex;
    /// <summary>
    /// 选中的项索引
    /// </summary>
    public int ItemIndex;
}


/// <summary>
/// 控制物品UI
/// 物品的移动、丢弃
/// </summary>
public class ItemUILogic : MonoBehaviour
{
    /// <summary>
    /// 背包容器
    /// </summary>
    public List<BackpackContainer> listBackpackContainer;
    /// <summary>
    /// 拖拽物
    /// </summary>
    public DragObject dragObject;
    /// <summary>
    /// 拖拽物的变幻组件
    /// </summary>
    private RectTransform DragTransform;
    /// <summary>
    /// 是否是移动物体状态
    /// </summary>
    public bool IsMoveMode { get; set; }
    /// <summary>
    /// 拖动窗口
    /// </summary>
    public ScrollRect scrollRect;
    /// <summary>
    /// 是否以及拖拽结束
    /// </summary>
    public static bool IsDragEnd = true;
    /// <summary>
    /// 选中的UI
    /// </summary>
    public static KnapsackItemUI SelectedUI;

    /// <summary>
    /// 目标的UI
    /// </summary>
    public static KnapsackItemUI Target;




    /// <summary>
    /// 背包UI控件
    /// </summary>
    public KnapsackUI TheKnapsackUI { get; set; }

    /// <summary>
    /// 选中的项数据索引
    /// </summary>
    public SelectItemIndex selectItemIndex;


    private void Awake()
    {
        TheKnapsackUI = GetComponent<KnapsackUI>();
        DragTransform = dragObject.GetComponent<RectTransform>();
        listBackpackContainer.Add(TheKnapsackUI.knapsack.knapsackData.container);
        listBackpackContainer.Add(TheKnapsackUI.knapsack.knapsackData.equipmentItems.container);
    }

    ///// <summary>
    ///// 原数据位置索引
    ///// </summary>
    //private int index;

    ///// <summary>
    ///// 拖拽列表的ID
    ///// </summary>
    //public static int SelectedListIndex=0;
    ///// <summary>
    ///// 目标列表的ID
    ///// </summary>
    //public static int TargetListIndex=0;

    /// <summary>
    /// 物品位置替换
    /// </summary>
    /// <returns></returns>
    public bool ItemPositionReplacement(BackpackContainer A, BackpackContainer B, int IndexA, int IndexB)
    {

        if (IndexA < A.list.Count && IndexB < B.list.Count)
        {
            KnapsackItem knapsackItem = A.list[IndexA];
            A.list[IndexA] = B.list[IndexB];
            B.list[IndexB] = knapsackItem;
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 物品位置替换
    /// </summary>
    /// <returns></returns>
    public bool ItemPositionReplacement(int A, int B, int IndexA, int IndexB)
    {

        return ItemPositionReplacement(listBackpackContainer[A],listBackpackContainer[B], IndexA, IndexB);

    }

    /// <summary>
    /// 物品位置替换
    /// </summary>
    /// <returns></returns>
    public bool ItemPositionReplacement(SelectItemIndex A, SelectItemIndex B)
    {

        return ItemPositionReplacement(A.SelectListIndex, B.SelectListIndex, A.ItemIndex, B.ItemIndex);

    }


    /// <summary>
    /// 开始一个拖拽的UI
    /// </summary>
    public bool DragObjectOpen(BackpackContainer container, int index)
    {
        if (container.list.Count > index)
        {
            dragObject.gameObject.SetActive(true);
            dragObject.SetItem(container.list[index]);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获得选择的格子索引
    /// </summary>
    /// <param name="list"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public SelectItemIndex GetSelectedIndex(List<BackpackContainer> list, KnapsackItem data)
    {
        SelectItemIndex select = new SelectItemIndex();
        //遍历所有背包容器，查找数据在哪一个容器
        foreach (var item in list)
        {
            selectItemIndex.ItemIndex = item.list.IndexOf(data);
            //不为-1就是找到了该数据
            if (selectItemIndex.ItemIndex != -1)
            {

                break;
            }
            selectItemIndex.SelectListIndex++;
        }
        if (select.SelectListIndex != -1)
        {
            return select;
        }
        else
        {
            return null;
        }

    }
    /// <summary>
    /// 获得选择的格子索引
    /// </summary>
    /// <param name="list"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public SelectItemIndex GetSelectedIndex(KnapsackItem data)
    {
        SelectItemIndex select = new SelectItemIndex();
        //遍历所有背包容器，查找数据在哪一个容器
        foreach (var item in listBackpackContainer)
        {
            select.ItemIndex = item.list.IndexOf(data);
            //不为-1就是找到了该数据
            if (select.ItemIndex != -1)
            {

                break;
            }
            select.SelectListIndex++;
        }
        if (select.SelectListIndex != -1)
        {
            return select;
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// 根据索引获取数据
    /// </summary>
    /// <param name="selectItemIndex"></param>
    /// <returns></returns>
    public KnapsackItem GetDataBySelectItemIndex(SelectItemIndex selectItemIndex)
    {
        return listBackpackContainer[selectItemIndex.SelectListIndex].list[selectItemIndex.ItemIndex];
    }

    /// <summary>
    /// 开始拖拽逻辑
    /// </summary>
    public bool DragStart()
    {
        //获得选中数据
        KnapsackItem SelectedData = SelectedUI.KnapsackItemData;
        //查找选中数据索引
        selectItemIndex.SelectListIndex = 0;
        foreach (var item in listBackpackContainer)
        {
            selectItemIndex.ItemIndex = item.list.IndexOf(SelectedData);
            //不为-1就是找到了该数据
            if (selectItemIndex.ItemIndex != -1)
            {

                break;
            }
            selectItemIndex.SelectListIndex++;
        }


        //找到索引并且不是点击空白栏地方时
        if (selectItemIndex.ItemIndex != -1 && SelectedData.itemData.name != "")
        {

            scrollRect.vertical = false;

            //激活拖拽物
            DragObjectOpen(listBackpackContainer[selectItemIndex.SelectListIndex], selectItemIndex.ItemIndex);




            //拖拽物大小校正
            DragTransform.sizeDelta = SelectedUI.GetComponent<RectTransform>().sizeDelta;


            return true;

        }
        //点击空白栏时
        else
        {
            //  Selected.ItemGroup.blocksRaycasts = false;
            return false;
        }

    }



    /// <summary>
    /// 拖拽结束
    /// </summary>
    public void DragEnd()
    {
        //   关闭拖动
        scrollRect.vertical = true;

        //   查看目标是否存在
        if (Target)
        {
            Debug.Log("(" + selectItemIndex.SelectListIndex + selectItemIndex.ItemIndex + ")" );

            SelectItemIndex TargetIndex = GetSelectedIndex(Target.KnapsackItemData);

            ItemPositionReplacement(selectItemIndex,TargetIndex);

            Debug.Log("("+ selectItemIndex.SelectListIndex +","+ selectItemIndex.ItemIndex +")\t("+ TargetIndex.SelectListIndex + ","+ TargetIndex.ItemIndex + ")");
        }
        else
        {


            //创建物品
            GameObject item = GameObject.Instantiate(Resources.Load<GameObject>(GetDataBySelectItemIndex(selectItemIndex).PreformPath));
            item.transform.position = TheKnapsackUI.knapsack.transform.position + Vector3.back;

            //施加一个初速度
            item.GetComponent<Rigidbody2D>().AddForce(TheKnapsackUI.knapsack.DiscardItemSpeed);


            //原格子变成空白格子
            listBackpackContainer[selectItemIndex.SelectListIndex].list[selectItemIndex.ItemIndex] = new KnapsackItem();

        }

        dragObject.gameObject.SetActive(false);
    }



    private void OnGUI()
    {


        //查看是否选中UI
        if (!IsDragEnd)
        {
            if (!IsMoveMode)
            {
                //尝试开始拖拽
                IsMoveMode = DragStart();

            }

        }

        //如果是拖拽状态中
        if (IsMoveMode)
        {
            UpdatePositionByMousePosition();
        }


        if (IsDragEnd)
        {


            if (IsMoveMode)
            {
                IsMoveMode = false;

                //结束拖拽逻辑
                DragEnd();



            }

        }

    }



    /// <summary>
    /// 根据鼠标点更新位置
    /// </summary>
    public void UpdatePositionByMousePosition()
    {
        //Debug.Log(Input.mousePosition);
        //Debug.Log("拖拽物位置:" + rectTransform.position + ",鼠标位置:" + Input.mousePosition);
        DragTransform.position = Input.mousePosition;
    }

    //// Use this for initialization
    //void Start()
    //{

    //}
    public DisplayItemWindowUI displayItemUI;

    // Update is called once per frame
    void Update()
    {
        //判断鼠标松开
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            IsDragEnd = true;
        }

        //显示一个物件信息的逻辑
        if (Target!=null&& Target.KnapsackItemData.itemData.name!="")
        {
            //信息校正
            displayItemUI.gameObject.SetActive(true);
            displayItemUI.SetDisplayItem(Target.KnapsackItemData);

            //位置校正
            RectTransform displayItemRect=displayItemUI.GetComponent<RectTransform>();

            if (Input.mousePosition.x<Screen.width/1.5)
            {
                displayItemRect.position = Input.mousePosition + Vector3.right * displayItemRect.rect.width;
            }
            else
            {
                displayItemRect.position = Input.mousePosition + Vector3.left * displayItemRect.rect.width;
            }


            if (Input.mousePosition.y < Screen.height / 2)
            {
                displayItemRect.position += Vector3.up * displayItemRect.rect.height/4;
            }
            else
            {
                displayItemRect.position += Vector3.down * displayItemRect.rect.height/4;
            }


        }
        else
        {
            displayItemUI.gameObject.SetActive(false);
        }
    }
}
