using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    bool isMove;

    [Header("Soldier Status Info")]
    [SerializeField] SoldierData soldierData;
    [SerializeField] float maxHp;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float damage;
    public float curHp;

    [Header("etc")]
    public Vector3 newPos;
    public Player player;


    WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(3f);
    CapsuleCollider soldierCollider;
    Rigidbody rigid;
    SoldierAnimator soldierAnimator;
    Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        soldierCollider = GetComponent<CapsuleCollider>();
        soldierAnimator = GetComponent<SoldierAnimator>();
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
    }

    public void MoveDestination(Vector3 destination)
    {
        soldierCollider.enabled = false;
        Debug.Log($"솔져의 목표 위치 : {destination}");
        StartCoroutine(Move(destination));
    }

    IEnumerator Move(Vector3 destination)
    {
        bool isArrive = false;
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
            }
            yield return null;
        }
    }

    void Rotate()
    {
        
    }
    void Stop()
    {
        soldierAnimator.OnMove(false);
    }
    IEnumerator VelocityZero()
    {
        yield return seconds;
    }

    void UnFreezeRotationY()
    {
        rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
