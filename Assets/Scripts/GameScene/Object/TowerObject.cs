using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public Transform head, shootPoint;
    private float roundSpeed = 50f;
    private TowerInfo info;
    private MonsterObject targetObj;
    private float nowTime = -1;

    private void Start()
    {
        //Init(DataManager.Instance.towerInfoList[7]);
    }
    public void Init(TowerInfo info)
    { 
        this.info = info; 
    }

    private void Update()
    {
        //单体攻击
        if ( info.type == 1)
        {
            if( targetObj == null || targetObj.isDead || Vector3.Distance(transform.position, targetObj.transform.position) > info.atkRange ) 
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, info.atkRange, 1 << LayerMask.NameToLayer("Monster"));
                if( colliders.Length > 0 )
                {
                    targetObj = colliders[0].gameObject.GetComponent<MonsterObject>();
                }
            }
            else
            {
                Vector3 pos = targetObj.transform.position;
                pos.y = head.transform.position.y;
                head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(pos - head.position), roundSpeed * Time.deltaTime);
                if( Vector3.Angle(head.forward, pos - head.position) < 5 && Time.time - nowTime >= info.offsetTime )
                {
                    nowTime = Time.time;
                    targetObj.Wound(info.atk);
                    DataManager.Instance.PlaySound("Music/Tower");
                    GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), shootPoint.position, shootPoint.rotation);
                    Destroy(effObj, 0.5f);

                }
            }
        }
        //群体攻击
        else 
        {
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, info.atkRange, 1 << LayerMask.NameToLayer("Monster"));
            if( colliders.Length > 0 && Time.time - nowTime >= info.offsetTime)
            {
                nowTime = Time.time;
                DataManager.Instance.PlaySound("Music/Tower");
                foreach (Collider collider in colliders)
                {
                    collider.gameObject.GetComponent<MonsterObject>().Wound(info.atk);
                    GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), shootPoint.position, shootPoint.rotation);
                    Destroy(effObj, 0.5f);
                }
            }
            
        }
    }
}
