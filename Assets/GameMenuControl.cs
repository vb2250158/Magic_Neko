using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuControl : MonoBehaviour {

    /// <summary>
    /// 菜单主控件
    /// </summary>
    public GameMenuUI gameMenu;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(WorldTree.keyBuff.playerKey.Esc))
        {
            //查看是否已经激活UI
            if (gameMenu.gameObject.activeSelf)
            {
                gameMenu.CloseMenu();
            }
            else
            {
                gameMenu.OpenMenu();
            }
        }
    }
}
