using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    private int hp, maxHp;
    private bool isDead;

    private static ShieldObject instance;
    public static ShieldObject Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void UpdateHp(int hp, int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;
        UIManager.Instance.GetPanel<GamePanel>().UpdateHp(hp, maxHp);
    }

    public void Wound(int damage)
    {
        if(isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            GameLevelMgr.Instance.GameOver();
        }
        UpdateHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
