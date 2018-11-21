using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 受击字体设置
/// </summary>
[System.Serializable]
public class HitTextSettings {
    /// <summary>
    /// 攻击类型
    /// </summary>
    public HitType hitType;
    /// <summary>
    /// 该类型的字体设置
    /// </summary>
    public HitText hitTextPreform;
}



/// <summary>
/// 受击UI，显示伤害、恢复等战斗中受击量
/// </summary>
public class HitUI : MonoBehaviour {

  //  HitTextSettings

    public List<HitTextSettings> hitTextsList;

    // public Transform The;
    void Awake() {
        WorldTree.worldUI.hitUI = this;
    }
    



    /// <summary>
    /// 创建一个受击文字
    /// </summary>
    public void CreateText(Vector3 position,string content,HitType hitType=HitType.ordinary) {
        HitText hitText = null;

        //遍历配置信息
        foreach (var item in hitTextsList)
        {
            if (item.hitType==hitType)
            {
                //创建文字
                hitText = Instantiate(item.hitTextPreform, transform);
            }
            
        }
        //设置起始位置
        hitText.transform.position = position;

        hitText.TextAwawk(content);
    }



}
