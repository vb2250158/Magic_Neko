using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 改变模式,用于摄像头
/// </summary>
public enum ChangePattern
{
    Shrink, Expand

}

/// <summary>
/// 摄像机控制
/// </summary>
public class CameraControl : MonoBehaviour
{


    public PlayerCamera playerCamera;

    /// <summary>
    /// 视角改变模式，进入时扩展、或退出时收缩
    /// </summary>
    public ChangePattern changeMode;

    // Use this for initialization
    void Start()
    {

    }
    public bool Control = true;
    // Update is called once per frame
    void Update()
    {
        if (Control)
        {


            //碰撞框大小效正
            if (transform.localScale.x != playerCamera.MainCamera.orthographicSize)
            {
                transform.localScale = new Vector3(playerCamera.MainCamera.orthographicSize, playerCamera.MainCamera.orthographicSize, 1);
            }

            //缩放逻辑控制
            switch (changeMode)
            {
                case ChangePattern.Shrink:
                    //里面时收缩
                    if (inside)
                    {
                        Debug.Log("收缩");
                        //playerCamera.changeSetting.focusFieldSize -= Time.deltaTime * 5;
                        playerCamera.changeSetting.focusFieldSize = Mathf.Clamp(playerCamera.changeSetting.focusFieldSize - Time.deltaTime * 20, playerCamera.fieldSize.Min, playerCamera.fieldSize.Max);

                    }
                    break;
                case ChangePattern.Expand:
                    //不在里面时扩展
                    if (!inside)
                    {
                        Debug.Log("扩展");
                        playerCamera.changeSetting.focusFieldSize = Mathf.Clamp(playerCamera.changeSetting.focusFieldSize+ Time.deltaTime *20, playerCamera.fieldSize.Min,playerCamera.fieldSize.Max);

                    }
                   
                    break;
                default:
                    break;
            }
        }
    }
    private bool inside = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("e");
            inside = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("o");
            inside = false;
        }

    }
}
