using System.Collections.Generic;
using UnityEngine;

public enum SoldierAnimationName { Death, Run, Shot };

public class SoldierAnimator : BaseAnimator
{
    public Dictionary<SoldierAnimationName, float> AnimationDeleay;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        AnimationDeleay = new Dictionary<SoldierAnimationName, float>();
    }
    //애니메이션의 길이를 미리 캐싱
    private void Start() => AnimationDeleay.Add(SoldierAnimationName.Death, GetAnimationLength("m_weapon_death_A"));
    public void InitializeSetUp()
    {
        OnMove(true);
        OnDead(false);
    }
}
