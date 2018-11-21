using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class ObjectUtiles : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 创建一个物体
    /// </summary>
    /// <param name="unityArmature"></param>
    /// <param name="CreateObject"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static ActionObject ActionObjectCreate(ActionObject CreateObject, Vector3 position)
    {

        ActionObject actionObject = null;
        actionObject = Instantiate(CreateObject);
        actionObject.transform.position = position;
        return actionObject;
    }
}
