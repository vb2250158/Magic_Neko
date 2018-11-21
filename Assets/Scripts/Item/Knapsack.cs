using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包的数据
/// </summary>
[System.Serializable]
public class KnapsackData
{

    /// <summary>
    /// 存放中物品
    /// </summary>
    public BackpackContainer container;
    /// <summary>
    /// 装备中物品
    /// </summary>
    public EquipmentItems equipmentItems;

    /// <summary>
    /// 背包的最大负重
    /// </summary>
    public LimitFloat weight;



}




/// <summary>
/// 装备的道具
/// </summary>
[System.Serializable]
public class EquipmentItems
{
    /// <summary>
    /// 容器
    /// </summary>
    public BackpackContainer container;
    /// <summary>
    /// 可用的最大格子数
    /// </summary>
    public int Maximum = 10;





    /// <summary>
    /// 格子数量更新
    /// </summary>
    public void NumberUpdate()
    {
        container.SetSize(Maximum);
    }

    /// <summary>
    /// 添加一个空格子
    /// </summary>
    public void AddNullKnapsackItem()
    {
        container.AddKnapsackItem(null);
    }



    /// <summary>
    /// 得到有东西格子的个数
    /// </summary>
    public int Count
    {
        get
        {
            return container.Count;
        }
    }

    /// <summary>
    /// 根据索引使用道具
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool UserItem(int index, Role role)
    {
        if (index < Maximum)
        {
            KnapsackItem knapsackItem = container.list[index];
            //道具栏为空也使用失败
            if (knapsackItem.itemData.name == "")
            {
                return false;
            }
            else
            {
                UserItem(knapsackItem, role);
                //判断道具类型
                switch (container.list[index].itemData.type)
                {
                    case ItemType.stuff:
                        break;
                    case ItemType.expendable:
                        //消耗品使用次数减少
                        knapsackItem.Number--;
                        if (knapsackItem.Number <= 0)
                        {
                            container.list[index] = new KnapsackItem();
                        }
                        break;
                    case ItemType.functional:
                        break;
                    case ItemType.currency:
                        break;
                    default:
                        break;
                }
                return true;
            }




        }
        else
        {
            return false;
        }

    }


    /// <summary>
    /// 使用一个道具
    /// </summary>
    /// <returns></returns>
    public bool UserItem(KnapsackItem knapsackItem, Role role)
    {
        //判断冷却时间
        if (knapsackItem.CDTimer <= 0)
        {
            //CD进入冷却
            knapsackItem.CDTimer = knapsackItem.itemData.CD;




            //道具功能创建物遍历
            //遍历创建物
            foreach (var item in knapsackItem.itemData.functionSetting.CreateObject)
            {
                ActionObject actionObject = null;
                //判断是否需要旋转
                if (!role.UnityArmature.armature.flipX)
                {
                    actionObject = GameObject.Instantiate(item.actionObject, item.vector3 + role.transform.position, Quaternion.identity, role.transform);

                }
                else
                {

                    actionObject = GameObject.Instantiate(item.actionObject, new Vector3(-item.vector3.x, item.vector3.y) + role.transform.position, Quaternion.identity, role.transform);
                    actionObject.transform.localScale = new Vector3(-actionObject.transform.localScale.x, actionObject.transform.localScale.y, actionObject.transform.localScale.z);
                    //x速度取反
                    actionObject.Speed.x = -actionObject.Speed.x;
                }
                actionObject.Creator = role;
            }



            return true;
        }
        else
        {
            return false;
        }
    }

}
/// <summary>
/// 物品容器,一排格子
/// </summary>
[System.Serializable]
public class BackpackContainer
{

    /// <summary>
    /// 物品格子列表
    /// </summary>
    public List<KnapsackItem> list;



    /// <summary>
    /// 添加一组道具
    /// </summary>
    /// <param name="_itmeData"></param>
    /// <returns></returns>
    public bool AddKnapsackItem(KnapsackItem addKnapsackItem = null)
    {


        //如果是需要添加一个空格子
        if (addKnapsackItem == null)
        {

            //添加格子
            list.Add(new KnapsackItem());
            return true;
        }

        //初始化格子
        KnapsackItem item = null;
        //初始化查找索引
        int index;
        //查看格子内是否有同样的非功能物品
        if ((item = list.Find(delegate (KnapsackItem knapsackItem) { return knapsackItem.itemData.name == addKnapsackItem.itemData.name && knapsackItem.itemData.type != ItemType.functional; })) != null)
        {
            //物品叠加
            item.Number += addKnapsackItem.Number;
            return true;
        }
        //添加将物品添加到空白格子
        else if ((index = list.FindIndex(delegate (KnapsackItem knapsackItem) { return knapsackItem.itemData.name == ""; })) != -1)
        {


            list[index] = addKnapsackItem;


            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 丢弃一个一组
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool DiscardItem(int index, Knapsack knapsack, Vector3 Speed)
    {
        //判断是否为空
        if (index < Size && list[index].itemData.name != "")
        {
          
            //创建物品
            GameObject item = GameObject.Instantiate(Resources.Load<GameObject>(list[index].PreformPath));
            item.transform.position = knapsack.transform.position + Vector3.back;

            //施加一个初速度
            item.GetComponent<Rigidbody2D>().AddForce(Speed);

            //成为空格子
            list[index] = new KnapsackItem();

            return true;
        }
        else
        {
            return false;
        }


    }


    /// <summary>
    /// 一排格子的大小(此值慎重赋值)
    /// </summary>
    public int Size;

    /// <summary>
    /// 设置容器大小
    /// </summary>
    public void SetSize(int size)
    {
        Size = size;
        while (size != list.Count)
        {
            if (size > list.Count)
            {
                AddKnapsackItem(null);
            }
            else
            {
                KnapsackItem Ki = list.Find((KnapsackItem kn) => { return kn.itemData.name == ""; });
                if (Ki != null)
                {
                    list.Remove(Ki);
                }
                else
                {
                    Debug.LogError("移除错误,有物品");
                    return;
                }

            }
        }

    }


    /// <summary>
    /// 有物品的格子数
    /// </summary>
    public int Count
    {
        get
        {
            int Number = 0;
            foreach (var item in list)
            {
                if (item.itemData != null && item.itemData.name != "")
                {
                    Number++;
                }
            }
            return Number;
        }
    }



}



/// <summary>
/// 背包物品格子
/// </summary>
[System.Serializable]
public class KnapsackItem
{
    /// <summary>
    /// 物品数据
    /// </summary>
    public ItemData itemData;
    /// <summary>
    /// 物品数量
    /// </summary>
    public int Number = 1;
    /// <summary>
    /// 剩余冷却时间
    /// </summary>
    public float CDTimer;

    public KnapsackItem()
    {
        itemData = new ItemData();
    }
    /// <summary>
    /// 物品的预制体路径
    /// </summary>
    public string PreformPath;

}


/// <summary>
/// 背包组件
/// </summary>
public class Knapsack : MonoBehaviour
{

    /// <summary>
    /// 背包数据
    /// </summary>
    public KnapsackData knapsackData;
    /// <summary>
    /// 附近物品
    /// </summary>
    public List<Item> EnclosureItem;



    /// <summary>
    /// 触发器
    /// </summary>
    public TriggerComponent triggerComponent;


    private void Start()
    {

        knapsackData.equipmentItems.NumberUpdate();


        //附近有物品时的核心逻辑
        triggerComponent.OnTriggerEnterEvent += delegate (Collider2D collider2D)
        {



            Item item = collider2D.GetComponent<Item>();
            //添加到附近列表
            EnclosureItem.Add(item);
            //创建UI显示
            WorldTree.worldUI.enclosureItemUI.DisplayEnclosureItem(item);






        };
        triggerComponent.OnTriggerExitEvent += delegate (Collider2D collider2D)
        {

            Item item = collider2D.GetComponent<Item>();
            //从附近物品列表里去除
            EnclosureItem.Remove(item);
            //删除
            WorldTree.worldUI.enclosureItemUI.DeleteEnclosureItem(item);
        };

    }

    private void Update()
    {
  
       
    }



    /// <summary>
    /// 拾取一个附件物品
    /// </summary>
    /// <returns></returns>
    public bool EnclosureItemPickUp()
    {
        if (EnclosureItem.Count != 0)
        {

            

            //捡起物品
            if (PickUp(EnclosureItem[0].knapsackItemData))
            {
                //获得物品的游戏对象
                GameObject item = EnclosureItem[0].gameObject;

         

                //从附近物品列表里去除
                EnclosureItem.Remove(EnclosureItem[0]);

                //删除游戏对象
                GameObject.Destroy(item);
                return false;
            }



            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 拾取物品
    /// </summary>
    /// <param name="addKnapsackItemData"></param>
    /// <returns></returns>
    public bool PickUp(KnapsackItem addKnapsackItemData)
    {
        float newWeight = knapsackData.weight.The + addKnapsackItemData.itemData.weight;

        //判断是否能有足够的负重拾取起物品
        if (newWeight <= knapsackData.weight.Max)
        {
   

            //负重增加
            knapsackData.weight.SetThe(newWeight);

            //尝试放到道具装备栏
            if (knapsackData.equipmentItems.Count < knapsackData.equipmentItems.Maximum)
            {
                knapsackData.equipmentItems.container.AddKnapsackItem(addKnapsackItemData);

               

            }
            else
            {

                //需要生成的格子数
                int cellNumber = ((knapsackData.container.Count / 6) + 2) * 6;
                //设置背包格子个数
                knapsackData.container.SetSize(cellNumber);
                //失败则放到存放栏
                knapsackData.container.AddKnapsackItem(addKnapsackItemData);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="index"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool UseItem(int index, Role role)
    {

        return knapsackData.equipmentItems.UserItem(index, role);

    }

    public Vector3 DiscardItemSpeed = Vector3.up * 100;

    /// <summary>
    /// 丢弃物品
    /// </summary>
    /// <param name="index"></param>
    /// <param name="backpackContainer"></param>
    /// <returns></returns>
    public bool DiscardItem(int index, BackpackContainer backpackContainer)
    {
        return backpackContainer.DiscardItem(index, this, DiscardItemSpeed);
    }


}
