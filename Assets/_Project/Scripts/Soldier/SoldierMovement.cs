using System.Collections;
using UnityEngine;

public class SoldierMovement : MonoBehaviour, IMovable
{
    CapsuleCollider soldierCollider;
    SoldierAnimator soldierAnimator;
    SoldierAttack soldierAttack;
    SoldierHealth soldierHealth;
    Rigidbody rigid;
    Transform target;

    bool isArrive = false;
    float moveSpeed;
    Vector3 direction;

    private void FixedUpdate()
    {
        if(soldierHealth.isDead) return;
        if(!isArrive) rigid.velocity = Vector3.zero;
        if (target != null) Rotate();
    }
    public void InitializeComponents(SoldierAnimator _soldierAnimator, SoldierAttack _soldierAttack,SoldierHealth _soldierHealth , Rigidbody _rigid, CapsuleCollider _capsuleCollider)
    {
        soldierAnimator = _soldierAnimator;
        soldierAttack = _soldierAttack;
        rigid = _rigid;
        soldierCollider = _capsuleCollider;
        soldierHealth = _soldierHealth;
    }
    public void InitializeSetUp(SoldierData soldierData)
    {
        moveSpeed = soldierData.MoveSpeed;
        rigid.isKinematic = false;
    }

    public IEnumerator MoveLoop(Vector3 destination)
    {
        float destinationDistance = 0.1f;
        float distance = 0f;
        soldierCollider.enabled = false;
        while (true)
        {
            if (isArrive) break;
            direction = destination - transform.position;
            distance = direction.magnitude;

            if (distance > destinationDistance)
            {
                Move();
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


    void Stop()
    {
        soldierAnimator.OnMove(false);
        rigid.isKinematic = true;
        StartCoroutine(soldierAttack.AttackLoop());
    }

    public void SetTarget(Transform _target) => target = _target;

    public void Move()
    {
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
