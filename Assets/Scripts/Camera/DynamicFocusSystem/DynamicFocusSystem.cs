using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DynamicFocusSystem : MonoBehaviour
{

    /// <summary>
    /// 焦点单位
    /// </summary>
    public List<FocalUnit> focalList;
    /// <summary>
    /// 视角限制设置
    /// </summary>
    public ClampFloat fieldSize;
    /// <summary>
    /// 动态效果摄像机设置
    /// </summary>
    public CameraChangeSetting changeSetting;

    /// <summary>
    /// 玩家
    /// </summary>
    private Player player;
    /// <summary>
    /// 主摄像机
    /// </summary>
    public Camera MainCamera { get; set; }
  
    // Use this for initialization
    void Start()
    {
        MainCamera = GetComponent<Camera>();
        FocalPlayerAwake();
        WorldTree.dynamicFocusSystem = this;
    }


    void Update()
    {

        //视角大小改变
        if (Mathf.Abs(changeSetting.focusFieldSize - MainCamera.orthographicSize) > 1)
        {

            //Debug.Log();
            MainCamera.orthographicSize = Mathf.Clamp(
                GameMahtf.ToTargetValue(
                    MainCamera.orthographicSize, changeSetting.focusFieldSize,
                    changeSetting.FieldSpeed * Mathf.Abs((changeSetting.focusFieldSize - MainCamera.orthographicSize) * (changeSetting.focusFieldSize - MainCamera.orthographicSize) * Time.deltaTime)
                ),
                fieldSize.Min,
                fieldSize.Max
                );

        }

        //焦点位置改变
        if (Mathf.Abs(changeSetting.focusPosition.x - transform.position.x) > 1 ||
            Mathf.Abs(changeSetting.focusPosition.y - transform.position.y) > 1
            )
        {
            //得出摄像机需要位移的方向          
            Vector3 velocity = ((changeSetting.focusPosition - transform.position) + changeSetting.deviationPosition).normalized * changeSetting.MoveSpeed;
            // Debug.Log(new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * GetPlayerSqrMagnitude());
            transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * GetPlayerSqrMagnitude();
        }



      
    }


    /// <summary>
    /// 震荡摄像机
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator Vibrate(float timer)
    {

        //   Vector3 position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            transform.position += Random.onUnitSphere * Time.deltaTime * 10;

            yield return null;
        }

    }



    /// <summary>
    /// 获得玩家与摄像机距离
    /// </summary>
    /// <returns></returns>
    public float GetPlayerSqrMagnitude()
    {

        return (changeSetting.focusPosition - transform.position).sqrMagnitude;
    }


    /// <summary>
    /// 初始化焦点玩家
    /// </summary>
    public void FocalPlayerAwake()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //监听玩家的焦点触发器
        player.DynamicFocusTrigger.OnTriggerEnterEvent += delegate (Collider2D collider2D)
        {
            FocalUnit focalUnit = null;

            if (focalUnit = collider2D.GetComponent<FocalUnit>())
            {
                Debug.Log("增加");
                focalList.Add(focalUnit);
            }

        };
        player.DynamicFocusTrigger.OnTriggerExitEvent += delegate (Collider2D collider2D)
        {
            FocalUnit focalUnit = null;
            if (focalUnit = collider2D.GetComponent<FocalUnit>())
            {
                Debug.Log("减少");
                focalList.Remove(focalUnit);
            }
        };
        focalList.Add(player.GetComponent<FocalUnit>());



    }
}
