using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUP : MonoBehaviour
{

    /// <summary>
    /// 触发器组件
    /// </summary>
    private TriggerComponent triggerComponent;

    /// <summary>
    /// 恢复生命值大小
    /// </summary>
    public int UpSize;

    private void Awake()
    {
        triggerComponent = GetComponent<TriggerComponent>();

        triggerComponent.OnTriggerEnterEvent += TriggerComponent_OnTriggerEnterEvent;

        triggerComponent.OnTriggerExitEvent += TriggerComponent_OnTriggerExitEvent;

    }




    private void TriggerComponent_OnTriggerExitEvent(Collider2D other)
    {

    }

    private void TriggerComponent_OnTriggerEnterEvent(Collider2D other)
    {
        CombatUnit combatUnit = other.GetComponent<CombatUnit>();


        combatUnit.HP.SetThe(combatUnit.HP.The + UpSize);

        WorldTree.worldUI.hitUI.CreateText(other.transform.position+Vector3.up*5, UpSize + "",HitType.magic);

        Debug.Log("HP+"+ UpSize);
    }
}
