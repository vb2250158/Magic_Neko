using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 物品掉落数据
/// </summary>
[System.Serializable]
public class DropOutData
{
    public Item item;
}
/// <summary>
/// 掉落速度
/// </summary>
[System.Serializable]
public class DropOutSpeed
{
    public Vector3 vector;
    public float randomX;
}


/// <summary>
/// 物品掉落组件
/// </summary>
public class DropOut : MonoBehaviour
{
    /// <summary>
    /// 受击组件
    /// </summary>
    private Hit hit;

    /// <summary>
    /// 掉落数据
    /// </summary>
    public DropOutData outData;
    /// <summary>
    /// 掉落速度设置
    /// </summary>
    public DropOutSpeed dropOutSpeed;

    private void Start()
    {

        hit = GetComponent<Hit>();
        //被击中时
        hit.Stay += delegate (FightTrigger fightTrigger)
        {

          
            //创建物品
            Item item = GameObject.Instantiate(outData.item);
            item.transform.position = transform.position+Vector3.back;
            
            //施加一个初速度
            item.GetComponent<Rigidbody2D>().AddForce(dropOutSpeed.vector + Vector3.right * Random.Range(-dropOutSpeed.randomX, dropOutSpeed.randomX));
        };


    }

  


}
