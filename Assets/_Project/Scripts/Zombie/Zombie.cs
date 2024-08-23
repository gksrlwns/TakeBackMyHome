using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.DocumentationSortingAttribute;

public class Zombie : MonoBehaviour
{
    [Header("Zombie Status Info")]
    [SerializeField] ZombieData zombieData;
    [SerializeField] ZombieStats zombieStats;
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
        InitializeSetUpZombieStats(StageManager.Instance.level);
        zombieAnimator.InitializeSetUp();
        zombieCustomizing.InitializeSetUp();
        zombieMovement.InitializeSetUp(zombieStats);
        zombieAttack.InitializeSetUp(zombieStats);
        zombieHealth.InitializeSetUp(zombieStats);
        zombieCustomizing.SetZombieCustomizing();
    }
    void InitializeSetUpZombieStats(int stageLevel)
    {
        zombieStats = new ZombieStats
        {
            maxHp = zombieData.MaxHp + stageLevel,
            damage = zombieData.Damage + (float)stageLevel/10,
            attackRange = zombieData.AttackRange,
            attackSpeed = zombieData.AttackSpeed,
            moveSpeed = zombieData.MoveSpeed
        };
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
[System.Serializable]
public struct ZombieStats
{
    public float maxHp;
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float moveSpeed;
}
