using UnityEngine;

enum SoldierState { Move, Sort, Battle, Dead }
public class Soldier : MonoBehaviour
{
    [Header("Soldier Status Info")]
    [SerializeField] SoldierData soldierData;
    

    [Header("etc")]
    public Player player;
    
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;
    SoldierAttack soldierAttack;
    SoldierHealth soldierHealth;
    CapsuleCollider soldierCollider;
    Rigidbody rigid;

    private void Awake()
    {
        soldierAnimator = GetComponent<SoldierAnimator>();
        soldierMovement = GetComponent<SoldierMovement>();
        soldierAttack = GetComponent<SoldierAttack>();
        soldierHealth = GetComponent<SoldierHealth>();
        soldierCollider = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        soldierAttack.InitializeComponents(soldierAnimator, soldierHealth,soldierMovement);
        soldierMovement.InitializeComponents(soldierAnimator, soldierAttack, rigid, soldierCollider);
        soldierHealth.InitializeComponents(soldierAnimator,soldierAttack ,soldierCollider);
    }
    private void OnEnable() => InitializeSetUp();
    void InitializeSetUp()
    {
        soldierAnimator.InitializeSetUp();
        soldierAttack.InitializeSetUp(soldierData);
        soldierMovement.InitializeSetUp(soldierData);
        soldierHealth.InitializeSetUp(soldierData);
    }
    public void MoveDestination(Vector3 destination)
    {
        Debug.Log($"솔져의 목표 위치 : {destination}");
        StartCoroutine(soldierMovement.MoveLoop(destination));
    }
    public void GetTargetSearch(TargetSearch _targetSearch) => soldierAttack.GetTargetSearch(_targetSearch);

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
            player.soldierList.Remove(this);
            player.soldierCount--;
        }
    }
}
