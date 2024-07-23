using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool isAttacking;
    Dictionary<SecondsType, WaitForSeconds> waitForSecondsDict;
    enum SecondsType { AttackSpeed, Attack_Anim};



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        waitForSecondsDict = new Dictionary<SecondsType, WaitForSeconds>();
    }

    public void OnInitAnimation()
    {
        animator.SetBool("isLive", true);
        animator.SetBool("isVictory", false);
    }

    public void OnMove(bool isMove)
    {
        animator.SetBool("isMove", isMove);
    }

    //public void OnAttack()
    //{
    //    StartCoroutine(AttackRoutine());
    //}

    //public void OnRevive()
    //{
    //    animator.SetBool("isLive", true);
    //}
    //IEnumerator AttackRoutine()
    //{
    //    isAttacking = true;
    //    animator.SetTrigger("Attack");

    //    //Debug.Log("기본 공격 사용");
    //    yield return waitForSecondsDict[SecondsType.Attack_Anim];

    //    yield return waitForSecondsDict[SecondsType.AttackSpeed];
    //    isAttacking = false;
    //    //animator.SetBool("isAttacking", isAttacking);
    //}
    //public void OnDamaged()
    //{
    //    if (!isAttacking)
    //    {
    //        animator.SetTrigger("Hurt");
    //    }
    //}
    //public void OnDead()
    //{
    //    animator.SetBool("isLive", false);
    //    animator.SetTrigger("Dead");
    //}
    //public void OnVictory()
    //{
    //    animator.SetBool("isVictory", true);
    //}
}
