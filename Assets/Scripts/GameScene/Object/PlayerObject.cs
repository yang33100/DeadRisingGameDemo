using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;
    private int atk, money;
    private float roundSpeed = 50f;
    public Transform attackPoint;
    public int Money
    {
        get => money;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    public void Init(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Roll");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }
    /// <summary>
    /// µ¶ ¹¥»÷¼ì²â
    /// </summary>
    public void KnifeEvent()
    {
        
        Collider[] monsters = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        foreach(Collider collider in monsters)
        {
            //¹ÖÎïÊÕµ½¹¥»÷Âß¼­
        }
    }

    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(attackPoint.position, attackPoint.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));
        foreach(RaycastHit hit in hits) 
        {
            //¹ÖÎïÊÕµ½¹¥»÷Âß¼­
        }
    }

    public void UpdateMoney()
    {
        (UIManager.Instance["GamePanel"] as GamePanel).UpdateGold(money);
    }

    public void ChangeMoney(int  amount) 
    {
        money += amount;
        UpdateMoney();
    }
}
