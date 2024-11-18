using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
        zombieAttack.InitializeComponents(zombieAnimator);
    }

    public void InitializeSetUp()
    {
        if(StageManager.Instance != null)
        {
            InitializeSetUpZombieStats(StageManager.Instance.level);
        }
        else
        {
            InitializeSetUpZombieStats(1000);
        }
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
    
    public void GetSpawnManager(ZombieSpawnManager _zombieSpawnManager) => zombieHealth.GetSpawnManager(_zombieSpawnManager);

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
