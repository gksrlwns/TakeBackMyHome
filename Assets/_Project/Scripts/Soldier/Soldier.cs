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
    CapsuleCollider soldierCollider;
    Rigidbody rigid;
    SoldierAnimator soldierAnimator;
    public TargetSearch targetSearch;
    [SerializeField] Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        soldierCollider = GetComponent<CapsuleCollider>();
        soldierAnimator = GetComponent<SoldierAnimator>();
    }
    private void Update()
    {
        if (state != SoldierState.Battle) return;
        if (target != null) Rotate();
    }
    private void OnEnable()
    {
        Init();
        soldierAnimator.OnMove(true);
    }

    private void FixedUpdate() => rigid.velocity = Vector3.zero;

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
        soldierCollider.enabled = false;
        Debug.Log($"솔져의 목표 위치 : {destination}");
        state = SoldierState.Sort;
        StartCoroutine(MoveLoop(destination));
    }

    IEnumerator MoveLoop(Vector3 destination)
    {
        float destinationDistance = 0.1f;
        Vector3 direction = Vector3.zero;
        float distance = 0f;
        while (true)
        {
            if (isArrive) break;
            direction = destination - transform.position;
            distance = direction.magnitude;

            if (distance > destinationDistance)
            {
                direction.Normalize();
                transform.Translate(direction * 3f * Time.deltaTime);
            }
            else
            {
                isArrive = true;
                //위치 보정
                transform.position = destination;
                soldierCollider.enabled = true;
                Stop();
                yield break;
            }
            yield return null;
        }
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

    void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    void Attack()
    {
        soldierAnimator.OnAttack();
        Debug.Log($"{this.name} 공격");
    }

    void Stop()
    {
        soldierAnimator.OnMove(false);
        state = SoldierState.Battle;
        StartCoroutine(BattleLoop());
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
