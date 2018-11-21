using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GridAutoType
{
    /// <summary>
    /// 上为焦点缩放
    /// </summary>
    Top,
    /// <summary>
    /// 下为焦点缩放
    /// </summary>
    Bottom

}

/// <summary>
/// 基于格子布局的自适应UI
/// </summary>
[RequireComponent(typeof(GridLayoutGroup))]
public class GridAutoUI : MonoBehaviour
{
    /// <summary>
    /// 横排个数
    /// </summary>
    public float Number;
    /// <summary>
    /// 自动缩放类型
    /// </summary>
    public GridAutoType gridAutoType;
    /// <summary>
    /// 格子组件
    /// </summary>
    private GridLayoutGroup grid;
    /// <summary>
    /// 当前的rect
    /// </summary>
    private RectTransform rect;

    private void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponent<RectTransform>();
        GridAutoUIUpdate();
    }




    /// <summary>
    /// 当transform改变时
    /// </summary>
    private void OnRectTransformDimensionsChange()
    {
        GridAutoUIUpdate();
    }
    /// <summary>
    /// 获取格子大小
    /// </summary>
    public float GridSize
    {
        get
        {
            return grid.cellSize.y;
        }
    }

    /// <summary>
    /// 格子大小更新
    /// </summary>
    public void GridAutoUIUpdate()
    {
        if (rect != null)
        {
            //获取目标大小
            float size = ((rect.rect.width - ((grid.padding.left + grid.padding.right) + ((Number - 1) * grid.spacing.x))) / Number);

            grid.cellSize = new Vector2(size, size);
        }

    }


    /// <summary>
    /// 容器大小自适应
    /// </summary>
    /// <param name="GridNumber">格子个数</param>
    public void ContentSizeUpdate(int GridNumber = 0)
    {


        switch (gridAutoType)
        {
            case GridAutoType.Top:
                rect.offsetMin = new Vector2(rect.offsetMin.x,
                 (rect.offsetMin.y + rect.rect.height) - //0点偏移
                  (GridNumber / Number) * GridSize -//（格子总个数/横排格子个数）*格子大小
                  grid.spacing.y * ((GridNumber / Number) - 1) - //spacing
                 (grid.padding.top + grid.padding.bottom)//padding
                 );
                break;

            case GridAutoType.Bottom:
                //   (rect.offsetMax.y - rect.rect.height)

                rect.offsetMax = new Vector2(rect.offsetMax.x,
           (rect.offsetMax.y - rect.rect.height) + //0点偏移
            (GridNumber / Number) * GridSize +//（格子总个数/横排格子个数）*格子大小
            grid.spacing.y * ((GridNumber / Number) - 1) +//spacing
              (grid.padding.top + grid.padding.bottom)  //padding
           );
                break;
            default:
                break;
        }





    }


}
