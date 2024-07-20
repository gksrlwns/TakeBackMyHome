using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public bool isAttacking;
    Dictionary<SecondsType, WaitForSeconds> waitForSecondsDict;
    Animator animator;
    enum SecondsType { AttackSpeed, Attack_Anim };



    private void Awake()
    {
        animator = GetComponent<Animator>();
        waitForSecondsDict = new Dictionary<SecondsType, WaitForSeconds>();
    }

    public void OnInitAnimation()
    {
        animator.SetBool("isLive", true);
        animator.SetBool("isVictory", false);
    }

    public void OnMove(Vector3 moveVec)
    {
        animator.SetFloat("Speed", moveVec.magnitude);
    }

    public void OnAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    public void OnRevive()
    {
        animator.SetBool("isLive", true);
    }
    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        //Debug.Log("기본 공격 사용");
        yield return waitForSecondsDict[SecondsType.Attack_Anim];

        yield return waitForSecondsDict[SecondsType.AttackSpeed];
        isAttacking = false;
        //animator.SetBool("isAttacking", isAttacking);
    }
    public void OnDamaged()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Hurt");
        }
    }
    public void OnDead()
    {
        animator.SetBool("isLive", false);
        animator.SetTrigger("Dead");
    }
    
}
