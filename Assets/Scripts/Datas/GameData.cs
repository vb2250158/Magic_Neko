using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// 魔杖数据
/// </summary>
[System.Serializable]
public class MagicWandData
{
    /// <summary>
    /// 魔杖名字
    /// </summary>
    public string name;
    /// <summary>
    /// 魔杖物理强度
    /// </summary>
    public int PhysicalTrength;
    /// <summary>
    /// 魔法力
    /// </summary>
    public int MagicPower;
    /// <summary>
    /// 魔杖说明
    /// </summary>
    public string Explain;
    /// <summary>
    /// 魔杖等级
    /// </summary>
    public Level Level;
    /// <summary>
    /// 魔杖阶级
    /// </summary>
    public int Rank;
}

/// <summary>
/// 等级类
/// </summary>
[System.Serializable]
public class Level
{
    /// <summary>
    /// 当前经验值
    /// </summary>
    public LimitInt Exp;

    public void ExpUp(int upExp) {
        Exp.The += upExp;
        if (Exp.Max == Exp.The)
        {
            level++;
            //重置经验值
            Exp.The = 0;

        }
    }
    /// <summary>
    /// 当前等级
    /// </summary>
    public int level;


}

/// <summary>
/// 极限值
/// </summary>
[System.Serializable]
public class LimitInt
{
    public int Max;

    public int The;
    public int SetThe(int value) {
        if (value > Max)
        {
            The = Max;
        }
        else
        {
            The = value;
        }
        return The;
    }
  

    public LimitInt() {
        this.Max = The = 9;
    }

    //构造函数
    public LimitInt(int Max)
    {
        this.Max = The = Max;
    }
    public LimitInt(int Max, int The)
    {
        this.Max = Max;
        this.The = The;
    }
}

/// <summary>
/// 极限值
/// </summary>
[System.Serializable]
public class LimitFloat
{
    public float Max=1;

    public float The;
    public float SetThe(float value)
    {
        if (value > Max)
        {
            The = Max;
        }
        else
        {
            The = value;
        }
        return The;
    }


    public LimitFloat()
    {
        this.Max = The = 9;
    }

    //构造函数
    public LimitFloat(int Max)
    {
        this.Max = The = Max;
    }
    public LimitFloat(int Max, int The)
    {
        this.Max = Max;
        this.The = The;
    }
}




