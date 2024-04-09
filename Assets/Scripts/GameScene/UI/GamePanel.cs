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
    private TowerPoint nowTowerPoint;

    public override void Init()
    {
        buttonQuit.onClick.AddListener(() =>
        {
            Cursor.lockState = CursorLockMode.None;
            int money = GameLevelMgr.Instance.player.Money;
            DataManager.Instance.playerData.hasMoney += money;
            DataManager.Instance.SavePlayerData();
            GameLevelMgr.Instance.ClearInfo();
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        
        botTrans.gameObject.SetActive(false);
    }

    public void UpdateHp(int hp, int maxHp)
    {
        textHpInfo.text = hp + "/" + maxHp;
        imgHp.rectTransform.sizeDelta = new Vector2((float)hp / maxHp * 500, 50);
    }

    public void UpdateCnt(int cnt)
    {
        textCntInfo.text = cnt.ToString();
    }

    public void UpdateGold(int gold)
    {
        textGoldInfo.text = gold.ToString();
    }

    public void UpdateTower(TowerPoint tp)
    {
        nowTowerPoint = tp;
        if (tp.towerInfo == null)
        {
            for(int i = 0; i < towerButtons.Count; i++)
            {
                towerButtons[i].gameObject.SetActive(true);
                towerButtons[i].Init(DataManager.Instance.towerInfoList[i * 3]);
            }
        }
        else
        {
            for (int i = 0; i < towerButtons.Count; i++)
            {
                towerButtons[i].gameObject.SetActive(false);
            }
            if( tp.towerInfo.next != 0)
            {
                towerButtons[0].gameObject.SetActive(true);
                towerButtons[0].Init(DataManager.Instance.towerInfoList[tp.towerInfo.next - 1]);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if( botTrans.gameObject.activeSelf ) 
        {
            
            UpdateTower(nowTowerPoint);
            for (int i = 0; i < towerButtons.Count; i++)
            {
                if(towerButtons[i].gameObject.activeSelf && Input.GetKeyDown(KeyCode.Alpha1 + i) && GameLevelMgr.Instance.player.Money >= towerButtons[i].gold)
                {
                    GameLevelMgr.Instance.player.ChangeMoney(-towerButtons[i].gold);
                    if(nowTowerPoint.towerObj != null)
                        Destroy(nowTowerPoint.towerObj);
                    TowerInfo info = DataManager.Instance.towerInfoList[towerButtons[i].id - 1];
                    
                    
                    nowTowerPoint.towerObj = Instantiate(Resources.Load<GameObject>(info.res), nowTowerPoint.transform.position, Quaternion.identity);
                    nowTowerPoint.towerInfo = info;
                    nowTowerPoint.towerObj.GetComponent<TowerObject>().Init(info);
                    break;
                }
            }
        }
    }
}
