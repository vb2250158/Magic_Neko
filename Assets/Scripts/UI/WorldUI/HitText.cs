using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 受击字体
/// </summary>
public class HitText : MonoBehaviour
{

    private Text text;
    /// <summary>
    /// 文字上升速度
    /// </summary>
    public float upSpeed;
    private string content = "";
    void Awake()
    {
        text = GetComponent<Text>();
        StartCoroutine(TextFade());
        text.text = content;
    }

    /// <summary>
    /// 初始化文本
    /// </summary>
    /// <param name="content"></param>
    public void TextAwawk(string content)
    {

        text.text = this.content = content;
    }

    /// <summary>
    /// 文字渐隐
    /// </summary>
    /// <returns></returns>
    IEnumerator TextFade()
    {
        float a = 1;
        while (a >= 0)
        {
            a -= Time.deltaTime;
            transform.position += Vector3.up * upSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);

            yield return null;
        }
        Destroy(gameObject);
        // text.color = new Color(text.color.r, text.color.b,,);
    }
}
