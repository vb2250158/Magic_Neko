using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugUI : MonoBehaviour
{
    private Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = 1 / Time.deltaTime + "fps";
    }

    /// <summary>
    /// 打印
    /// </summary>
    /// <param name="msg"></param>
    public void DebugLog(string msg) {
        Debug.Log (msg);
    }

}
