using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;



[RequireComponent(typeof(CombatUnit))]
public class Player : MonoBehaviour
{


    /// <summary>
    /// 角色组件
    /// </summary>
    public Role PlayerRole { get; set; }
    /// <summary>
    /// 作战单位组件
    /// </summary>
    public CombatUnit PlayerCombatUnit { get; set; }
    /// <summary>
    /// 按键缓存
    /// </summary>
    private KeyBuff keyBuff;


    /// <summary>
    /// 动态焦点触发器
    /// </summary>
    public TriggerComponent DynamicFocusTrigger;
    /// <summary>
    /// 玩家焦点
    /// </summary>
    public FocalUnit PlayerFocal { get; set; }

    /// <summary>
    /// 背包
    /// </summary>
    public Knapsack knapsack;
    void Awake()
    {
        PlayerRole = GetComponent<Role>();
        PlayerCombatUnit = GetComponent<CombatUnit>();
        keyBuff = GetComponent<KeyBuff>();
        PlayerFocal = GetComponent<FocalUnit>();


    }

    public float AxisX { get; set; }
    public float AxisY { get; set; }

    public float AxisUpSize = 1f;

    /// <summary>
    /// 接近目标值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="TargetValue"></param>
    /// <returns></returns>
    private float ToTargetValue(float value, float TargetValue)
    {

        if (value > TargetValue)
        {
            //     Debug.Log(value+"--"+ TargetValue);
            return Mathf.Clamp(value - AxisUpSize * Time.deltaTime, TargetValue, value);
        }
        else
        {
            return Mathf.Clamp(value + AxisUpSize * Time.deltaTime, value, TargetValue);
        }

    }


    /// <summary>
    /// 玩家控制逻辑，基于keybuff控制玩家数据
    /// </summary>
    public void PlayerControl()
    {

        if (Input.GetKey(keyBuff.playerKey.UP) || Input.GetKey(keyBuff.playerKey.Down))
        {
            if (Input.GetKey(keyBuff.playerKey.UP))
            {
                AxisY = ToTargetValue(AxisY, 1);
            }
            if (Input.GetKey(keyBuff.playerKey.Down))
            {
                AxisY = ToTargetValue(AxisY, -1);

            }
        }
        else
        {
            AxisY = ToTargetValue(AxisY, 0);
        }



        if (Input.GetKey(keyBuff.playerKey.Left) || Input.GetKey(keyBuff.playerKey.Right))
        {

            if (Input.GetKey(keyBuff.playerKey.Left))
            {

                AxisX = -1;
            }
            if (Input.GetKey(keyBuff.playerKey.Right))
            {

                AxisX = 1;
            }
        }
        else
        {
            AxisX = ToTargetValue(AxisX, 0);
        }

    }

    void Update()
    {
        PlayerControl();
        //当角可控时
        if (PlayerRole.IsControl)
        {
            //移动更新
            PlayerRole.MoveSpeed = PlayerCombatUnit.GetMoveSpeed(AxisX);


        }

        //攻击
        if (Input.GetKeyDown(keyBuff.playerKey.Atk) && PlayerCombatUnit.AttackReady())
        {
            PlayerCombatUnit.Attack();
        }
        //拾取
        if (Input.GetKeyDown(keyBuff.playerKey.Pickup))
        {
            Pickup();
        }
        //使用道具
        for (int i = 0; i < keyBuff.playerKey.UserItem.Count; i++)
        {
            var item = keyBuff.playerKey.UserItem[i];
            //遍历按键是否有按下
            if (Input.GetKeyDown(item))
            {
                Debug.Log("使用道具");
                knapsack.UseItem(i, PlayerRole);
            }

        }



        //  Debug.Log();
    }

    /// <summary>
    /// 拾取
    /// </summary>
    public void Pickup()
    {

        knapsack.EnclosureItemPickUp();
    }


}
