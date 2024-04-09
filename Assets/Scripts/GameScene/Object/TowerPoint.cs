using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    public GameObject towerObj = null;
    public TowerInfo towerInfo = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UIManager.Instance.GetPanel<GamePanel>().botTrans.gameObject.SetActive(true);
            UIManager.Instance.GetPanel<GamePanel>().UpdateTower(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            UIManager.Instance.GetPanel<GamePanel>().botTrans.gameObject.SetActive(false);
    }
}
