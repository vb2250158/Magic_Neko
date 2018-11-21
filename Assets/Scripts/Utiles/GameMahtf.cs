using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMahtf
{

    /// <summary>
    /// 接近目标值()
    /// </summary>
    /// <param name="value"></param>
    /// <param name="TargetValue"></param>
    /// <returns></returns>
    public static float ToTargetValue(float value, float TargetValue, float delta)
    {

        if (value > TargetValue)
        {

            return Mathf.Clamp(value - delta, TargetValue, value);
        }
        else
        {
            return Mathf.Clamp(value + delta, value, TargetValue);
        }

    }
    /// <summary>
    /// 接近目标值()
    /// </summary>
    /// <param name="value"></param>
    /// <param name="TargetValue"></param>
    /// <returns></returns>
    public static Vector3 ToTargetValue(Vector3 value, Vector3 TargetValue, Vector3 delta)
    {


        Vector3 vector = new Vector3();
        vector.x = ToTargetValue(value.x, TargetValue.x, delta.x);
        vector.y = ToTargetValue(value.y, TargetValue.y, delta.y);
        vector.z = ToTargetValue(value.z, TargetValue.z, delta.z);
        return vector;
    }
}
