using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public int troopCnt, zombieCntATtoop;
    private int uncreateZombieCnt;
    public List<int> monsterIDList;
    private int nowId;

    public float createTimeOffset, troopTimeOffset, firstTroopDelayTime;

    private void Start()
    {
        Invoke("CreateTroop", firstTroopDelayTime);
    }

    private void CreateTroop()
    {
        if (troopCnt <= 0)
        {
            //Ê¤Àû
            return;
        }
        nowId = monsterIDList[Random.Range(0, monsterIDList.Count)];
        CreateMonster();
        uncreateZombieCnt = zombieCntATtoop;
        troopCnt--;
        
    }

    private void CreateMonster()
    {
        
        MonsterInfo info = DataManager.Instance.monsterInfoList[nowId - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), transform.position, Quaternion.identity);
        MonsterObject monsterObj = obj.GetComponent<MonsterObject>();
        monsterObj.Init(info);
        uncreateZombieCnt--;
        if (uncreateZombieCnt > 0)
            Invoke("CreateMonster", createTimeOffset);
        else
            Invoke("CreateTroop", troopTimeOffset);
    }

    public bool CheckOver()
    {
        return troopCnt == 0 && uncreateZombieCnt == 0;
    }
}
