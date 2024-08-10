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
    ZombieCustomizing zombieCustomizing;
    TargetSearch targetSearch;
    NavMeshAgent agent;

    private void Awake()
    {
        targetSearch = GetComponent<TargetSearch>();
        agent = GetComponent<NavMeshAgent>();
        zombieCollider = GetComponent<CapsuleCollider>();
        zombieAnimator = GetComponent<ZombieAnimator>();
        zombieMovement = GetComponent<ZombieMovement>();
        zombieAttack = GetComponent<ZombieAttack>();
        zombieHealth = GetComponent<ZombieHealth>();
        zombieCustomizing = GetComponentInChildren<ZombieCustomizing>();
        agent.enabled = false;
        zombieAnimator.InitDict();
        zombieHealth.InitializeComponents(zombieAnimator, zombieCollider, agent);
        zombieMovement.InitializeComponents(zombieAttack, zombieAnimator,targetSearch ,agent);
        zombieAttack.InitializeComponents(zombieAnimator, zombieMovement);
    }

    public void InitializeSetUp()
    {
        zombieAnimator.InitializeSetUp();
        zombieCustomizing.InitializeSetUp();
        zombieMovement.InitializeSetUp(zombieData);
        zombieAttack.InitializeSetUp(zombieData);
        zombieHealth.InitializeSetUp(zombieData);
        zombieCustomizing.SetZombieCustomizing();
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

    public void GetEndPoint(Transform endPoint) => zombieMovement.GetEndPoint(endPoint);

}
