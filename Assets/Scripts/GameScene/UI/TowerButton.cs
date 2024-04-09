using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public Image img;
    public Text textTip, textNeedGold;
    public int id, gold;

    public void Init(TowerInfo info)
    {
        id = info.id;
        img.sprite = Resources.Load<Sprite>(info.imgRes);
        textTip.text = info.name;
        gold = info.money;
        textNeedGold.text = "$" + info.money.ToString();
    }
}
