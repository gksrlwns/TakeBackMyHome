using System.Collections.Generic;
using UnityEngine;

public enum SoldierAnimationName { m_weapon_death_A, Run, Shot };

public class SoldierAnimator : BaseAnimator
{
    Dictionary<SoldierAnimationName, float> animationDeleayDict;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //애니메이션의 길이를 미리 캐싱
        animationDeleayDict = new Dictionary<SoldierAnimationName, float>
        {
            { SoldierAnimationName.m_weapon_death_A, GetAnimationLength("m_weapon_death_A") }
        };
    }
    
    public void InitializeSetUp()
    {
        OnMove(true);
        OnDead(false);
    }
    public float GetAnimationSeconds(SoldierAnimationName name)
    {
        if (!animationDeleayDict.TryGetValue(name, out var delay))
        {
            animationDeleayDict[name] = GetAnimationLength(name.ToString());
        }

        return delay;
    }

}
