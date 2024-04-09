using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button buttonSure, buttonBuy, buttonBack, buttonLeft, buttonRight;
    public Text textNowGold, textHeroName;
    private Transform heroPos;
    private GameObject heroObj;
    private RoleInfo nowRoleInfo;
    private int nowIndex;
    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;
        textNowGold.text = DataManager.Instance.playerData.hasMoney.ToString();
        ChangeHero();

        int cnt = DataManager.Instance.roleInfoList.Count;
        
        buttonSure.onClick.AddListener(() =>
        {
            DataManager.Instance.selectedRole = nowRoleInfo;
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            //进入游戏场景
            AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene 1");
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.Init();
            };
            
        });

        buttonBuy.onClick.AddListener(() =>
        {
            if (DataManager.Instance.playerData.hasMoney >= nowRoleInfo.unlockMoney)
            {
                DataManager.Instance.playerData.hasMoney -= nowRoleInfo.unlockMoney;
                DataManager.Instance.playerData.unlockedHero.Add(nowRoleInfo.id);
                textNowGold.text = DataManager.Instance.playerData.hasMoney.ToString();
                DataManager.Instance.SavePlayerData();
                UpdateUnlockButton();

            }
        });

        buttonBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAni>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });

        buttonLeft.onClick.AddListener(() =>
        {
            nowIndex = (nowIndex - 1 + cnt) % cnt;
            ChangeHero();
        });

        buttonRight.onClick.AddListener(() =>
        {
            nowIndex = (nowIndex + 1) % cnt;
            ChangeHero();
        });

        
    }

    private void ChangeHero()
    {
        nowRoleInfo = DataManager.Instance.roleInfoList[nowIndex];
        textHeroName.text = nowRoleInfo.name;
        UpdateUnlockButton();
        if (heroObj != null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleInfo.res), heroPos.position, heroPos.rotation);
        Destroy(heroObj.GetComponent<PlayerObject>());
    }

    private void UpdateUnlockButton()
    {
        if( nowRoleInfo.unlockMoney > 0 && !DataManager.Instance.playerData.unlockedHero.Contains(nowRoleInfo.id))
        {
            textHeroName.text += " <color=#ff0000ff>$" + nowRoleInfo.unlockMoney + "</color>";
            buttonBuy.gameObject.SetActive(true);
            buttonSure.gameObject.SetActive(false);
        }
        else
        {
            textHeroName.text = nowRoleInfo.name;
            buttonBuy.gameObject.SetActive(false);
            buttonSure.gameObject.SetActive(true);
        }
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if( heroObj != null )
        {
            DestroyImmediate( heroObj);
            heroObj = null;
        }
    }
}
