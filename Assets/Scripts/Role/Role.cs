using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

/// <summary>
/// 动作数据
/// </summary>
[System.Serializable]
public class ActionEvent
{
    /// <summary>
    /// 创建物体事件设置
    /// </summary>
    public List<ActionEventCreateObject> CreateList;
    /// <summary>
    /// 播放声音设置
    /// </summary>
    public List<ActionPlaySound> PlaySounds;
    /// <summary>
    /// 连击设置
    /// </summary>
    public List<ActionCombo> CombosList;
}

/// <summary>
/// 连击类，可以设置连击的按键
/// </summary>
[System.Serializable]
public class ActionCombo
{
    /// <summary>
    /// 连击名
    /// </summary>
    public string name;
    /// <summary>
    /// 连击要求按键                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    /// </summary>
    public List<KeyCode> keys;
    /// <summary>
    /// 连击动画名
    /// </summary>
    public string AnimationName;
}



/// <summary>
/// 动作创建对象
/// </summary>
[System.Serializable]
public class ActionEventCreateObject
{
    /// <summary>
    /// 动作名字
    /// </summary>
    public string name;

    /// <summary>
    /// 创建设置
    /// </summary>
    public CreateSettings Settings;

    //public 
}
/// <summary>
/// 动作音效
/// </summary>
[System.Serializable]
public class ActionPlaySound
{
    /// <summary>
    /// 音效名
    /// </summary>
    public string name;
    /// <summary>
    /// 音效的设置
    /// </summary>
    public AudioSettings Settings;
}
/// <summary>
/// 播放声音设置
/// </summary>
[System.Serializable]
public class AudioSettings
{
    public AudioClip sound;
    public bool loop;
}

/// <summary>
/// 物品创建设置
/// </summary>
[System.Serializable]
public class CreateSettings
{
    /// <summary>
    /// 创建物
    /// </summary>
    public ActionObject CreateObject;
    /// <summary>
    /// 创建点
    /// </summary>
    public Vector3 position;
}

/// <summary>
/// 角色数据类，该数据为角色基本数据，不计算加成值
/// </summary>
[System.Serializable]
public class RoleData
{
    /// <summary>
    /// 角色名字
    /// </summary>
    public string name;
    /// <summary>
    /// 角色动画播放速度
    /// </summary>
    public float PlaySpeed = 1;
}

/// <summary>
/// 角色属性类
/// </summary>
[System.Serializable]
public class RoleAttribute
{

}



/// <summary>
/// 角色组件，包含了角色的基础功能
/// 功能：移动，待机，受击
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Role : MonoBehaviour
{


    /// <summary>
    /// 角色数据
    /// </summary>
    public RoleData roleData;
    /// <summary>
    /// 龙骨动画组件
    /// </summary>
    public UnityArmatureComponent UnityArmature { get; set; }
    /// <summary>
    /// 声音控制组件
    /// </summary>
    private AudioSource audioSource;
    /// <summary>
    /// 角色buff类
    /// </summary>
    public List<Buff> buffs;

    /// <summary>
    /// 角色动作列表
    /// </summary>
    public ActionEvent ActionList;
    /// <summary>
    /// 角色的移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 受击组件组件
    /// </summary>
    public Hit RoleHit { get; set; }




    /// <summary>
    /// 角色是否可控制
    /// </summary>
    public bool IsControl;
    /// <summary>
    /// 增加一个buff
    /// </summary>k
    /// <param name="buff"></param>
    public void AddBuff(Buff buff)
    {
        //触发添加buff事件
        buff.RoleAddBuff(this);
        foreach (var item in buffs)
        {
            //查看是否以存在此buff
            if (item.name == buff.name)
            {
                //如果新同名buff
                if (item.Timer < buff.Timer)
                {
                    item.Timer = buff.Timer;
                }
                return;
            }
        }
        //添加buff
        buffs.Add(buff);

    }

    void Awake()
    {
        //数据初始化
        IsControl = true;
        //初始化角色必须组件
        WorldTree.roleManagement.AddRole(this);
        audioSource = GetComponent<AudioSource>();
        UnityArmature = GetComponent<UnityArmatureComponent>();
        //事件与动作数据融合
        //龙骨动画事件
        UnityArmature.AddDBEventListener(EventObject.FRAME_EVENT, OnEventHandler);

        //尝试初始化扩展组件,初始化获取失败将不会生效，但是可以通过其他组件给其初始化
        try
        {
            KeyTimer = GetComponent<KeyBuff>().KeyTimerList;
        }
        catch (System.Exception)
        {

            Debug.Log("该对象无按键计时器:" + gameObject.name);
        }
        try
        {
            RoleCombatUnit = GetComponent<CombatUnit>();
        }
        catch (System.Exception)
        {

            Debug.Log("该对象无战斗单位组件:" + gameObject.name);
        }
    }
    /// <summary>
    /// 战斗单位组件
    /// </summary>
    public CombatUnit RoleCombatUnit { get; set; }
    /// <summary>
    /// 按键计时器
    /// </summary>
    public List<KeyTimer> KeyTimer { get; set; }
    /// <summary>
    /// 注册特效、触发器信息
    /// </summary>
    /// <param name="type">事件的类型</param>
    /// <param name="eventObject">事件对象</param>
    void OnEventHandler(string type, EventObject eventObject)
    {

        //事件遍历
        switch (eventObject.name)
        {
            //创建物体事件
            case "Create":
                //循环遍历创建物列表
                foreach (var item in ActionList.CreateList)
                {

                    //当前动画的字符串参数与动作名相同时
                    if (item.name == eventObject.data.GetString())
                    {
                        ActionObject actionObject = null;
                        //判断是否需要旋转
                        if (!UnityArmature.armature.flipX)
                        {
                            actionObject = Instantiate(item.Settings.CreateObject, item.Settings.position + transform.position, Quaternion.identity, transform);

                        }
                        else
                        {

                            actionObject = Instantiate(item.Settings.CreateObject, new Vector3(-item.Settings.position.x, item.Settings.position.y) + transform.position, Quaternion.identity, transform);
                            actionObject.transform.localScale = new Vector3(-actionObject.transform.localScale.x, actionObject.transform.localScale.y, actionObject.transform.localScale.z);
                            //x速度取反
                            actionObject.Speed.x = -actionObject.Speed.x;
                        }
                        actionObject.Creator = this;
                        return;
                    }

                }
                Debug.Log("无法识别创建动作参数:" + eventObject.data.GetString());

                break;
            //播放声音事件
            case "Sound":
                foreach (var item in ActionList.PlaySounds)
                {
                    //当前要
                    if (item.name == eventObject.data.GetString())
                    {
                        //不是循环播放并且不是
                        if (!(item.Settings.loop && audioSource.clip == item.Settings.sound))
                        {
                            //切换音频文件
                            audioSource.clip = item.Settings.sound;
                            //修改循环设置
                            audioSource.loop = item.Settings.loop;
                            //播放
                            audioSource.Play();
                        }

                    }

                }
                break;
            case "Combo":
                if (KeyTimer == null)
                {
                    break;
                }
                foreach (var item in ActionList.CombosList)
                {
                    //检索动作名
                    if (item.name == eventObject.data.GetString())
                    {
                        //获得按键计时器的按键数量
                        int KeyIndex = KeyTimer.Count;
                        bool ComboOK = true;
                        //判断
                        if (item.keys.Capacity <= KeyIndex)
                        {
                            //遍历按键条件
                            foreach (var key in item.keys)
                            {
                                KeyIndex--;
                                //判断键盘码
                                if (key != KeyTimer[KeyIndex].key)
                                {

                                    ComboOK = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ComboOK = false;
                        }
                        //动作条件准备就绪
                        if (ComboOK)
                        {
                            //开始动作
                            AddAction(item.AnimationName, 1);
                            if (RoleCombatUnit)
                            {
                                //添加攻击中buff
                                AddBuff(RoleCombatUnit.AttackBuff);
                            }
                        }

                    }
                }
                break;
            default:
                Debug.Log("无法识别事件:" + eventObject.name);
                break;
        }
    }


    /// <summary>
    /// 角色是否处于移动状态，非本组件时此变量请慎重赋值
    /// </summary>
    public bool Moveing { get; set; }

    /// <summary>
    /// 暂停播放时间>0就暂停播放动画
    /// </summary>
    public float StopTime;

    /// <summary>
    /// 当前动画播放速度
    /// </summary>
    public float PlaySpeed = 1;

    /// <summary>
    /// 是否打开基础动画切换更新
    /// </summary>
    public bool BeasAni = true;

    void Update()
    {
        //判定是否需要暂停动作的逻辑,动画修正
        if (StopTime > 0)
        {
            StopTime -= Time.deltaTime;
            if (UnityArmature.animation.timeScale != 0)
            {
                UnityArmature.animation.timeScale = 0;
            }

        }
        else if (PlaySpeed != UnityArmature.animation.timeScale)
        {
            //动画播放速度效正
            UnityArmature.animation.timeScale = PlaySpeed;
        }


        //角色buff更新
        foreach (var item in buffs)
        {
            item.Timer -= Time.deltaTime;
            item.RoleUpdate(this);
            if (item.Timer <= 0)
            {
                item.RoleRemoveBuff(this);
                buffs.Remove(item);
                break;
            }
        }

        //是否打开动画更新

        if (MoveSpeed != 0)
        {
            //判断角是否处于移动状态，决定是否播放移动动画
            if (!Moveing&& BeasAni)
            {

                //切换回移动动画
                StopAction(UnityArmature.animation.lastAnimationName);
                //播放移动动画
                AddAction("walk");

                Moveing = true;
            }
            //判断角色是否需要镜像翻转
            if (MoveSpeed > 0 && UnityArmature.armature.flipX)
            {
                UnityArmature.armature.flipX = false;
            }
            else if (MoveSpeed < 0 && !UnityArmature.armature.flipX)
            {
                UnityArmature.armature.flipX = true;
            }

            //位移
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }
        //如果速度为0并且是移动动画则播放待机动画
        else if (UnityArmature.animation.lastAnimationName == "walk"&& BeasAni)
        {
            Moveing = false;
            AddAction("steady");
        }




        //当前如果没有动画，则默认播放待机动画
        if (!UnityArmature.animation.isPlaying&& BeasAni)
        {

            Moveing = false;
            AddAction("steady");
        }


    }




    /// <summary>
    /// 添加一个动作
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(string aniName, int playTimes = -1, List<string> mask = null, string group = null)
    {

        //播放龙骨动画
        DragonBones.AnimationState animationState = UnityArmature.animation.FadeIn(aniName, -1, playTimes, 0, group);
        if (mask != null)
        {
            //循环添加遮罩
            foreach (var item in mask)
            {
                animationState.AddBoneMask(item);
            }
        }
    }
    /// <summary>
    /// 获取动作的播放进度
    /// </summary>
    /// <returns></returns>
    public float GetActionTime()
    {

        return UnityArmature.animation.lastAnimationState.currentTime / UnityArmature.animation.lastAnimationState.totalTime;
    }


    /// <summary>
    /// 停止动作
    /// </summary>
    public void StopAction(string animationName = null)
    {

        UnityArmature.animation.Stop(animationName);
    }

    /// <summary>
    /// 判断一个buff是否存在
    /// </summary>
    public bool JudgeBuff(string buffName)
    {
        //查看是否有Buff
        if (buffs.Find(delegate (Buff buff) { return buff.name == buffName; }) != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
