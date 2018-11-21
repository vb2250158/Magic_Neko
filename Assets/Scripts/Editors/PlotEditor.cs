using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[System.Serializable]
public class TextSettings
{
    /// <summary>
    /// 文字播放速度
    /// </summary>
    public float FadeSpeed = 10;


    /// <summary>
    /// 说话者文字颜色设定
    /// </summary>
    public List<SpeakerColor> speakers;
    /// <summary>
    /// 默认颜色
    /// </summary>
    public Color DefaultColor=new Color(200,200,200,1);
}


/// <summary>
/// 讲述者颜色设置
/// </summary>
[System.Serializable]
public class SpeakerColor
{
    public string name;
    public Color TextColor;
}


[System.Serializable]
public class PlotEditorBaseSettings
{
    /// <summary>
    /// 内容
    /// </summary>
    public Text TextControl;
    /// <summary>
    /// 名字
    /// </summary>
    public Text NameControl;
    /// <summary>
    /// UI主控件
    /// </summary>
    public GameObject UIRoot;
}





/// <summary>
/// 文本故事编辑类
/// </summary>
[RequireComponent(typeof(ParseCodeLibrary))]
public class PlotEditor : MonoBehaviour
{
    /// <summary>
    /// 必须UI设置
    /// </summary>
    public PlotEditorBaseSettings BaseSettings;
    /// <summary>
    /// 文字设置
    /// </summary>
    public TextSettings TextSetting;
    /// <summary>
    /// 预加载文本
    /// </summary>
    public List<string> PreloadedText { get; set; }
    /// <summary>
    /// 编译代码库
    /// </summary>
    private ParseCodeLibrary CodeLibrary;
    /// <summary>
    /// 当前内容索引
    /// </summary>
    public int ContentIndex;
    /// <summary>
    /// 当前内容
    /// </summary>
    public string TextContent;

    /// <summary>
    /// 开始剧情
    /// </summary>
    /// <param name="path"></param>
    public void PlotStart(string path) {
        BaseSettings.UIRoot.SetActive(true);
        LoadPreloadedText("剧情/Test.txt");
    }

    /// <summary>
    /// 停止剧情
    /// </summary>
    public void PlotEnd() {
        BaseSettings.UIRoot.SetActive(false);
    }


    // Use this for initialization
    void Start()
    {
        CodeLibrary = GetComponent<ParseCodeLibrary>();
        //进入世界树
        WorldTree.plotEditor = this;

    }
    /// <summary>
    /// 是否还有文字
    /// </summary>
    public bool ExistenceText { get; set; }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            KeyDown();

        }
    }

    /// <summary>
    /// 逐渐出现文字
    /// </summary>
    public void FadeText()
    {
        IsPlayText = true;
        StartCoroutine(FadeText_e(TextContent));
    }

    IEnumerator FadeText_e(string text)
    {
  
        //文本框重置
        BaseSettings.TextControl.text = "";
        int textIndex = 0;


        //颜色及显示的文本设置
        if (Speaker != "")
        {
            //显示角色名字
            BaseSettings.NameControl.text = "【" + Speaker + "】";
            System.Predicate<SpeakerColor> findName = delegate (SpeakerColor s)
            {
                return s.name == Speaker;
            };

            try
            {
                //改变文字颜色
                BaseSettings.NameControl.color = TextSetting.speakers.Find(findName).TextColor;
                BaseSettings.TextControl.color = TextSetting.speakers.Find(findName).TextColor;
            }
            catch (System.Exception)
            {
                //当找不到颜色时使用默认色
                BaseSettings.NameControl.color = TextSetting.DefaultColor;
                BaseSettings.TextControl.color = TextSetting.DefaultColor;
            }
        }
        else
        {
            //改变名字颜色
            BaseSettings.NameControl.text = "";
            //改变内容颜色
            BaseSettings.NameControl.color = TextSetting.DefaultColor;
        }

        //    Debug.Log(text.Length + ","+ textIndex);


        //循环出现文字
        while (text.Length > textIndex && IsPlayText)
        {
            BaseSettings.TextControl.text += text.Substring(textIndex, 1);
            textIndex++;
            yield return new WaitForSeconds(1 / TextSetting.FadeSpeed);
        }
        //终止文本效正
        BaseSettings.TextControl.text = text;

        IsPlayText = false;
    }

    /// <summary>
    /// 是否在播放文本
    /// </summary>
    public bool IsPlayText { get; set; }

    /// <summary>
    /// 加载解析解析的文本
    /// </summary>
    /// <param name="path">Path.</param>
    public void LoadPreloadedText(string path)
    {
       
        PreloadedText = new List<string>(SaveUtile.LoadLines(path)).FindAll(delegate (string text)
        {
            return text != "";
        });
        
        if (PreloadedText.Count==0)
        {
            Debug.Log("读取到空文本:"+ path);
            return;
        }
        

        ContentIndex = 0;
        ExistenceText = NextContent();

    }

    /// <summary>
    /// 当前讲述者
    /// </summary>
    public string Speaker;

    /// <summary>
    /// 解析文本成为当前要呈现的内容
    /// </summary>
    /// <returns><c>true</c>, if text was analysised, <c>false</c> otherwise.</returns>
    public bool AnalysisText(string _content)
    {
        if (_content.IndexOf("-") != 0)
        {
            int nameIndex = _content.IndexOf("：");
            //提取分离讲述人
            if (_content.IndexOf("：") != -1)
            {
                Speaker = _content.Substring(0, nameIndex);
                TextContent = _content.Substring(nameIndex + 1, _content.Length - nameIndex - 1);
            }
            else
            {
                //不进行分离
                Speaker = "";
                TextContent = _content;
            }

            return true;
        }
        else
        {

            CodeLibrary.Parse(_content.Substring(1, _content.Length - 1));
            return false;
        }




    }

    /// <summary>
    /// 当玩家按下按键时
    /// </summary>
    public void KeyDown()
    {
        //有文本时
        if (ExistenceText)
        {
            //不在播放中时可以进行内容切换
            if (IsPlayText)
            {
                ExistenceText = NextContent();
            }
            else {
                IsPlayText = false;

            }
          


           
        }
        else
        {
            PlotEnd();
            

        }
    }




    /// <summary>
    /// 切换播放下一内容，如果到了尽头内容，就会返回假
    /// </summary>
    public bool NextContent()
    {


        //解析当前文本,如果是剧情就会播放该剧情
        if (AnalysisText(PreloadedText[ContentIndex]))
        {
            //逐渐出现文本
            FadeText();
        }

        ContentIndex++;
        if (ContentIndex < PreloadedText.Count)
        {

            return true;
        }
        return false;
    }



}
