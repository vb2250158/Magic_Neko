using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色管理
/// </summary>
[System.Serializable]
public class RoleManagement
{
    /// <summary>
    /// 整个地图的所有角色
    /// </summary>
    private List<Role> WorldRole = new List<Role>();

    public bool AddRole(Role role) {
        WorldRole.Add(role);
        return true;
    }



    public bool DeleteRole(Role role) {
        WorldRole.Remove(role);
        return true;
    }

}
