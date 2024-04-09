using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private MonsterInfo monsterInfo;
    private int hp;
    public bool isDead = false;
    private float lastAttackTime = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Init(MonsterInfo info)
    {
        monsterInfo = info;
        hp = monsterInfo.hp;
        agent.speed = agent.acceleration = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    public void BornOver()
    {
        agent.SetDestination(ShieldObject.Instance.transform.position);
        animator.SetBool("Run", true);
    }

    private void Update()
    {
        if (isDead) 
            return;
        animator.SetBool("Run", agent.velocity != Vector3.zero);

        if (Vector3.Distance(transform.position, ShieldObject.Instance.transform.position) <= 5 && Time.time - lastAttackTime >= monsterInfo.atkOffset) 
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;

        }
        
    }
    public void AtkEvent()
    {
        DataManager.Instance.PlaySound("Music/Eat");
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("Shield"));
        foreach (Collider collider in colliders)
        {
            if( collider.gameObject == ShieldObject.Instance.gameObject )
            {
                ShieldObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
    public void Wound(int damage)
    {
        if (isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        animator.SetTrigger("Wound");
        DataManager.Instance.PlaySound("Music/Wound");
    }

    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        DataManager.Instance.PlaySound("Music/Dead");
        animator.SetBool("Dead", true);
    }

    public void DeadEvent()
    {
        GameLevelMgr.Instance.ChangeMonsterCnt(-1);
        Destroy(gameObject);
    }
}
