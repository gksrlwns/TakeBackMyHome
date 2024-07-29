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
    [SerializeField] bool isArrive = false;
    bool isDead = false;


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
    private void OnEnable()
    {
        Init();
        soldierAnimator.OnMove(true);
    }

    private void FixedUpdate() => rigid.velocity = Vector3.zero;

    private void Update()
    {
        if (!isArrive) return;
        if (targetSearch.colliders.Length == 0) return;
        else target = targetSearch.NearTarget();

        Rotate();
        Attack();
    }

    public void Init()
    {
        maxHp = soldierData.MaxHp;
        damage = soldierData.Damage;
        attackRange = soldierData.AttackRange;
        attackSpeed = soldierData.AttackSpeed;
        isArrive = false;
        isDead = false;
    }

    public void MoveDestination(Vector3 destination)
    {
        soldierCollider.enabled = false;
        Debug.Log($"������ ��ǥ ��ġ : {destination}");
        StartCoroutine(Move(destination));
    }

    IEnumerator Move(Vector3 destination)
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
                //��ġ ����
                transform.position = destination;
                soldierCollider.enabled = true;
                Stop();
            }
            yield return null;
        }
    }

    void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // 2D ���ӿ����� y�� ȸ�� ����

        // Ÿ�� �������� ȸ���ϴ� ��ǥ ȸ���� ���
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ���� ȸ�������� ��ǥ ȸ�������� �ε巴�� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    void Attack()
    {
        soldierAnimator.OnAttack();
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
