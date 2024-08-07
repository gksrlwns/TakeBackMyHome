using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieAnimationName { Z_emerge, Z_death_A };
public class ZombieAnimator : BaseAnimator
{
    Dictionary<ZombieAnimationName, float> animationDeleayDict;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void InitializeSetUp()
    {
        OnMove(true);
        OnAttack(false);
        OnDead(false);
    }
    public void InitDict()
    {
        animationDeleayDict = new Dictionary<ZombieAnimationName, float>
        {
            { ZombieAnimationName.Z_emerge, GetAnimationLength("Z_emerge") },
            { ZombieAnimationName.Z_death_A, GetAnimationLength("Z_death_A") }
        };
    }

    public float GetAnimationSeconds(ZombieAnimationName name)
    {
        if (!animationDeleayDict.TryGetValue(name, out var delay))
        {
            animationDeleayDict[name] = GetAnimationLength(name.ToString());
        }

        return delay;
    }
}
