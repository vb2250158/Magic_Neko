using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMap  {

    public Dictionary<string, Buff> AllBuff = new Dictionary<string, Buff>();
    /// <summary>
    /// 记录一个Buff
    /// </summary>
    /// <param name="buff"></param>
    public void AddBuff(Buff buff) {
        //查看是否有记录了该方法        
        if (!AllBuff.ContainsKey(buff.name))
        {
            Debug.Log("添加Buff");
            AllBuff.Add(buff.name, buff);
        }
        
    }
}
 