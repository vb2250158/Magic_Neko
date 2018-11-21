using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物类
/// </summary>
[RequireComponent(typeof(CombatUnit))]
public class Mosnter : MonoBehaviour
{

    private CombatUnit combatUnit;


    



    void Start()
    {
        combatUnit = GetComponent<CombatUnit>();

        //死亡更新事件
        combatUnit.DieUpdate += delegate ()
        {

            //尝试播放死亡动画
            if (combatUnit.TheRole.UnityArmature.animation.lastAnimationName != "die")
            {
                combatUnit.TheRole.AddAction("die",1);
            }
            //combatUnit.TheRole.roleData.PlaySpeed

        };

        //当角色走向死亡时
        combatUnit.DieStart += delegate ()
        {
            combatUnit.TheRole.BeasAni=false;
        };

    }

    // Update is called once per frame
    void Update()
    {

    }
}
