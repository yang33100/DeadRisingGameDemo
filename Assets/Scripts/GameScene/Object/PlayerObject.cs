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
        //Debug.DrawRay(attackPoint.position, attackPoint.forward,Color.red);
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
        if (Input.GetKeyDown (KeyCode.BackQuote))
            Cursor.lockState = CursorLockMode.None;
        else if (Input.GetKeyUp(KeyCode.BackQuote))
            Cursor.lockState= CursorLockMode.Locked;
    }
    /// <summary>
    /// µ¶ ¹¥»÷¼ì²â
    /// </summary>
    public void KnifeEvent()
    {
        DataManager.Instance.PlaySound("Music/Kinfe");
        Collider[] monsters = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        foreach(Collider collider in monsters)
        {
            GameObject effObj = Instantiate(Resources.Load<GameObject>(DataManager.Instance.selectedRole.hitEff));
            effObj.transform.position = collider.transform.position;
            Destroy(effObj, 1);
            collider.gameObject.GetComponent<MonsterObject>().Wound(atk);
        }
    }

    public void ShootEvent()
    {
        DataManager.Instance.PlaySound("Music/Gun");
        //RaycastHit[] hits = Physics.RaycastAll(new Ray(attackPoint.position, transform.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), 1000, 1 << LayerMask.NameToLayer("Monster"));
        foreach (RaycastHit hit in hits)
        {
            GameObject effObj = Instantiate(Resources.Load<GameObject>(DataManager.Instance.selectedRole.hitEff));
            effObj.transform.position = hit.point;
            Destroy(effObj, 1);
            hit.transform.gameObject.GetComponent<MonsterObject>().Wound(atk);
        }
    }

    public void UpdateMoney()
    {
        if (UIManager.Instance.GetPanel<GamePanel>() == null) return;
        (UIManager.Instance["GamePanel"] as GamePanel).UpdateGold(money);
    }

    public void ChangeMoney(int  amount) 
    {
        money += amount;
        UpdateMoney();
    }
}
