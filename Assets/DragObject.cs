using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragObject : MonoBehaviour
{

    public Image image;
    /// <summary>
    /// 设置拖拽物
    /// </summary>
    /// <param name="knapsackItem"></param>
    public void SetItem(KnapsackItem knapsackItem)
    {
        image.sprite = knapsackItem.itemData.picture;
    }


    private void Awake()
    {

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //    Debug.Log(hit.collider);
        //}
       

        //Debug.Log(list.Count);

    }
}
