using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{

    CapsuleCollider soldierCollider;
    SoldierAnimator soldierAnimator;
    Rigidbody rigid;
    bool isArrive = false;
    Transform target;

    private void Awake()
    {
        soldierCollider = GetComponent<CapsuleCollider>();
        soldierAnimator = GetComponent<SoldierAnimator>();
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        //if (state != SoldierState.Battle) return;
        if (target != null) Rotate();
    }

    public IEnumerator MoveLoop(Vector3 destination)
    {
        float destinationDistance = 0.1f;
        Vector3 direction = Vector3.zero;
        float distance = 0f;
        soldierCollider.enabled = false;
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
    void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    void Stop()
    {
        soldierAnimator.OnMove(false);
        //state = SoldierState.Battle;
        //StartCoroutine(BattleLoop());
    }



}
