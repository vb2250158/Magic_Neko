using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家按键设置
/// </summary>
[System.Serializable]
public class PlayerKey
{
    public KeyCode UP = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Atk = KeyCode.J;
    public KeyCode Jump = KeyCode.K;
    public KeyCode Specil = KeyCode.L;
    public KeyCode Pickup = KeyCode.P;
    public KeyCode Esc = KeyCode.Escape;

    public List<KeyCode> UserItem = new List<KeyCode>();
}
/// <summary>
/// 按键计时器
/// </summary>
[System.Serializable]
public class KeyTimer
{
    /// <summary>
    /// 按键名
    /// </summary>
    public string name;
    /// <summary>
    /// 当前按键码
    /// </summary>
    public KeyCode key;
    /// <summary>
    /// 按键码存在时间
    /// </summary>
    public float Timer;

    public KeyTimer(KeyCode key, float timer)
    {
        this.key = key;
        name = key.ToString();
        Timer = timer;
    }
}

/// <summary>
/// 按键缓冲
/// </summary>
public class KeyBuff : MonoBehaviour
{


    /// <summary>
    /// 按键计时器列表，操作预处理
    /// </summary>
    public List<KeyTimer> KeyTimerList;

    /// <summary>
    /// 最大保存按键数
    /// </summary>
    public int MaxKey = 4;

    /// <summary>
    /// 玩家按键，该值需要其他控制类初始化
    /// </summary>
    public PlayerKey playerKey;

    /// <summary>
    /// 按键存在时间
    /// </summary>
    public float MaxTime = 0.2f;

    private void Awake()
    {
        WorldTree.keyBuff = this;
    }

    // Update is called once per frame
    void Update()
    {
        KeyUpdate();
        //记录按键缓存
        if (Input.GetKeyDown(playerKey.Atk))
        {
            KeyTimerList.Add(new KeyTimer(playerKey.Atk, MaxTime));
            if (KeyTimerList.Count > MaxKey)
            {
                KeyTimerList.RemoveAt(0);
            }
        }

        if (Input.GetKeyDown(playerKey.Jump))
        {
            KeyTimerList.Add(new KeyTimer(playerKey.Jump, MaxTime));
            if (KeyTimerList.Count > MaxKey)
            {
                KeyTimerList.RemoveAt(0);
            }
        }
        if (Input.GetKeyDown(playerKey.Specil))
        {
            KeyTimerList.Add(new KeyTimer(playerKey.Specil, MaxTime));
            if (KeyTimerList.Count > MaxKey)
            {
                KeyTimerList.RemoveAt(0);
            }
        }
        if (Input.GetKeyDown(playerKey.Pickup))
        {
            KeyTimerList.Add(new KeyTimer(playerKey.Pickup, MaxTime));
            if (KeyTimerList.Count > MaxKey)
            {
                KeyTimerList.RemoveAt(0);
            }
        }

    }
    /// <summary>
    /// 按键列表更新
    /// </summary>
    void KeyUpdate()
    {

        foreach (var item in KeyTimerList)
        {
            item.Timer -= Time.deltaTime;
            if (item.Timer <= 0)
            {
                KeyTimerList.Remove(item);
                break;
            }
        }


    }
}
