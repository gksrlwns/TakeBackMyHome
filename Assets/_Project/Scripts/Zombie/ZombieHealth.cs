using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : BaseHealth
{
    ZombieAnimator zombieAnimator;
    NavMeshAgent agent;

    ZombieSpawnManager spawnManager;

    public void InitializeComponents(ZombieAnimator _zombieAnimator, CapsuleCollider _capsuleCollider, NavMeshAgent _agent)
    {
        zombieAnimator = _zombieAnimator;
        myCollider = _capsuleCollider;
        agent = _agent;
    }
    public void InitializeSetUp(ZombieStats zombieStats)
    {
        maxHp = zombieStats.maxHp;
        curHp = maxHp;
        isDead = false;
        layer = LayerMask.NameToLayer("Zombie");
        myCollider.enabled = true;
    }
    protected override void Dead()
    {
        base.Dead();
        agent.enabled = false;
        spawnManager.SpawnCurCount--;
        zombieAnimator.OnDead(isDead);
        StartCoroutine(RetunrPool());
    }
    public void GetSpawnManager(ZombieSpawnManager _zombieSpawnManager) => spawnManager = _zombieSpawnManager;

    /// <summary>
    /// �ִϸ��̼� ���̸� ������ �� �ִϸ��̼��� ���� �� Return�ϵ��� ����
    /// </summary>
    IEnumerator RetunrPool()
    {
        yield return CoroutineManager.DelaySeconds(zombieAnimator.GetAnimationSeconds(ZombieAnimationName.Z_death_A));
        PoolManager.instance.ReturnObject(PoolType.ZombieA, gameObject);
    }
}
