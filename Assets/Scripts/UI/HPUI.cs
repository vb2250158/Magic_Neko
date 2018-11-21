using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPUI : MonoBehaviour
{

    public LimitInt HP;
    public Text text;
    public ProgressBarUI progressBar;

    //  Rect rect;
    private void Start()
    {

        //  ImageSize= 
    }

    private void Update()
    {
        //父节点的宽度*0.n
        // Debug.Log(image.);
        //   image.offsetMin= new Vector3((HP.The / HP.Max) * image.rect.width, image.offsetMin.y);
        //    Debug.Log(image.offsetMin);
        text.text = HP.The + "/" + HP.Max;

        progressBar.value = ((float)HP.The) / HP.Max;

    }


}
