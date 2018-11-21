using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatUnitUI : MonoBehaviour {

    public HPUI playerHPUI;

    public Text roleName;

    public Player player;
    // Use this for initialization
    void Start()
    {
        SetPlayer(player);
    }



    /// <summary>
    /// 设置观察的玩家
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(Player player)
    {
        //获取HP信息
        playerHPUI.HP = player.PlayerCombatUnit.combatUnitData.HP;
        //设置角色名字
        roleName.text = player.PlayerRole.roleData.name;
    }
}
