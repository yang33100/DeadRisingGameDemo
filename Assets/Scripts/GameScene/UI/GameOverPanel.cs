using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text nowGold;
    public Button buttonSure;
    public override void Init()
    {
        Cursor.lockState = CursorLockMode.None;
        int money = GameLevelMgr.Instance.player.Money;
        nowGold.text = money.ToString();
        buttonSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            DataManager.Instance.playerData.hasMoney += money;
            DataManager.Instance.SavePlayerData();
            GameLevelMgr.Instance.ClearInfo();
            SceneManager.LoadScene("BeginScene");
        });
    }

    
}
