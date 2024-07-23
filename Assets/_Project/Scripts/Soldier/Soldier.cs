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
    CapsuleCollider capsuleCollider;
    Rigidbody rigid;
    SoldierAnimator soldierAnimator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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
    public void Stop()
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
