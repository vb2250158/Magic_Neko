using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatUnitInfoUI : MonoBehaviour
{

    public HPUI HP;

    public Text roleName;

    public CombatUnit combatUnit;

    private void Start()
    {
        InfoUpdate();
    }


    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="player"></param>
    public void InfoUpdate()
    {
        //获取HP信息
        HP.HP = combatUnit.HP;
        //设置角色名字
        roleName.text = combatUnit.TheRole.roleData.name;
    }

}
