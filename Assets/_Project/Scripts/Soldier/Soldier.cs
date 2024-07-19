using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    SoldierData soldierData;
    SoldierAnimator soldierAnimator;

    [Header("Zombie Status Info")]
    [SerializeField] float maxHp;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;
    public float curHp;

    

    [Header("etc")]
    public Vector3 newPos;
    public Player player;
    WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(3f);
    CapsuleCollider capsuleCollider;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
    }
    public void Init()
    {
        maxHp = soldierData.MaxHp;
        attackRange = soldierData.AttackRange;
        attackSpeed = soldierData.AttackSpeed;
        moveSpeed = soldierData.MoveSpeed;
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
