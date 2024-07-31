using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

enum SoldierState { Move, Sort, Battle, Dead }
public class Soldier : MonoBehaviour
{
    [Header("Soldier Status Info")]
    [SerializeField] SoldierState state;
    [SerializeField] SoldierData soldierData;
    [SerializeField] float maxHp;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float damage;
    [SerializeField] Transform bulletPos;
    public float curHp;

    [Header("etc")]
    public Vector3 newPos;
    public Player player;
    [SerializeField] bool isArrive = false;


    WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(3f);
    
    Rigidbody rigid;
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;

    public TargetSearch targetSearch;

    [SerializeField] Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        soldierAnimator = GetComponent<SoldierAnimator>();
        soldierMovement = GetComponent<SoldierMovement>();
    }

    private void OnEnable()
    {
        Init();
        soldierAnimator.OnMove(true);
    }

    

    public void Init()
    {
        maxHp = soldierData.MaxHp;
        damage = soldierData.Damage;
        attackRange = soldierData.AttackRange;
        attackSpeed = soldierData.AttackSpeed;
        isArrive = false;
        soldierAnimator.OnDead(false);
        state = SoldierState.Move;
    }

    public void MoveDestination(Vector3 destination)
    {
        
        Debug.Log($"솔져의 목표 위치 : {destination}");
        state = SoldierState.Sort;
        StartCoroutine(soldierMovement.MoveLoop(destination));
    }

   
    IEnumerator BattleLoop()
    {
        while(true)
        {
            target = targetSearch.NearTarget();//seconds
            if (target != null) Attack();

            yield return CoroutineManager.DelaySeconds(attackSpeed);
        }
    }



    void Attack()
    {
        soldierAnimator.OnAttack();
        Debug.Log($"{this.name} 공격");
    }


    public void Dead()
    {
        state = SoldierState.Dead;
        soldierAnimator.OnDead(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
            player.soldierList.Remove(this);
            player.soldierCount--;
        }
    }
}
