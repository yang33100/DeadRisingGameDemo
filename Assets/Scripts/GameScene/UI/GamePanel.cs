using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHp;
    public Text textHpInfo, textCntInfo, textGoldInfo;
    public Button buttonQuit;
    public Transform botTrans;
    public List<TowerButton> towerButtons;
    private Rect hpRect;
    public override void Init()
    {
        hpRect = imgHp.rectTransform.rect;

        buttonQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        botTrans.gameObject.SetActive(false);
    }

    public void UpdateHp(int hp, int maxHp)
    {
        textHpInfo.text = hp + "/" + maxHp;
        imgHp.rectTransform.sizeDelta = new Vector2((float)hp / maxHp * hpRect.width, hpRect.height);
    }

    public void UpdateCnt(int cnt)
    {
        textCntInfo.text = cnt.ToString();
    }

    public void UpdateGold(int gold)
    {
        textGoldInfo.text = gold.ToString();
    }
}
