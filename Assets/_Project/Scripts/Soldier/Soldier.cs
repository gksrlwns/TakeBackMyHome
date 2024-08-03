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

    private void Awake()
    {
        soldierAnimator = GetComponent<SoldierAnimator>();
        soldierMovement = GetComponent<SoldierMovement>();
        soldierAttack = GetComponent<SoldierAttack>();
        soldierAttack.Init(soldierAnimator, soldierData, soldierMovement);
        soldierMovement.Init(soldierAnimator, soldierData, soldierAttack);
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        soldierAnimator.OnDead(false);
        soldierAnimator.OnMove(true);
    }

    public void MoveDestination(Vector3 destination)
    {
        Debug.Log($"솔져의 목표 위치 : {destination}");
        StartCoroutine(soldierMovement.MoveLoop(destination));
    }

    public void GetTargetSearch(TargetSearch _targetSearch)
    {
        soldierAttack.GetTargetSearch(_targetSearch);
    }
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
