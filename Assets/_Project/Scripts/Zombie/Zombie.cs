using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [Header("Zombie Status Info")]
    [SerializeField] ZombieData zombieData;
    [SerializeField] Soldier target;

    CapsuleCollider zombieCollider;
    ZombieAnimator zombieAnimator;
    ZombieMovement zombieMovement;
    ZombieAttack zombieAttack;
    ZombieHealth zombieHealth;
    TargetSearch targetSearch;
    NavMeshAgent agent;

    private void Awake()
    {
        targetSearch = GetComponent<TargetSearch>();
        agent = GetComponent<NavMeshAgent>();
        zombieCollider = GetComponent<CapsuleCollider>();
        zombieAnimator = GetComponentInChildren<ZombieAnimator>();
        zombieMovement = GetComponent<ZombieMovement>();
        zombieAttack = GetComponent<ZombieAttack>();
        zombieHealth = GetComponent<ZombieHealth>();
        agent.enabled = false;
        zombieAnimator.InitDict();
        zombieHealth.InitializeComponents(zombieAnimator, zombieCollider, agent);
        zombieMovement.InitializeComponents(zombieAttack, zombieAnimator,targetSearch ,agent);
        zombieAttack.InitializeComponents(zombieAnimator, zombieMovement);
    }

    public void InitializeSetUp()
    {
        zombieAnimator.InitializeSetUp();
        zombieMovement.InitializeSetUp(zombieData);
        zombieAttack.InitializeSetUp(zombieData);
        zombieHealth.InitializeSetUp(zombieData);
    }

    private void OnEnable()
    {
        InitializeSetUp();
        StartCoroutine(Emerge());
    }

    IEnumerator Emerge()
    {
        yield return CoroutineManager.DelaySeconds(zombieAnimator.GetAnimationSeconds(ZombieAnimationName.Z_emerge));
        agent.enabled = true;
        StartCoroutine(zombieMovement.CheckNearTargetLoop());
    }

}
