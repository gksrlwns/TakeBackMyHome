using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : BaseAnimator
{

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnMove(bool isMove)
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDead(bool isDead)
    {
        throw new System.NotImplementedException();
    }
}
