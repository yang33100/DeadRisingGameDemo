using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;
    public PlayerObject player;
    private int monsterCnt;
    private GameLevelMgr() 
    {

    }

    public void Init()
    {
        
        UIManager.Instance.ShowPanel<GamePanel>();

        RoleInfo roleInfo = DataManager.Instance.selectedRole;
        Transform bornPos = GameObject.Find("HeroBornPoint").transform;
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), bornPos.position, bornPos.rotation);
        
        player = heroObj.GetComponent<PlayerObject>();
        player.Init(roleInfo.atk, 200);
        Camera.main.GetComponent<CameraMove>().lookPos = heroObj.transform;
        ShieldObject.Instance.UpdateHp(100, 100);
        player.UpdateMoney();
    }

    public void ChangeMonsterCnt(int num)
    {
        if(num == -1)
        {
            player.ChangeMoney(25);
        }
        monsterCnt += num;
        if(monsterCnt <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        UIManager.Instance.HidePanel<GamePanel>();
        UIManager.Instance.ShowPanel<GameOverPanel>();
    }
    public void ClearInfo()
    {
        monsterCnt = 0;
    }
}
