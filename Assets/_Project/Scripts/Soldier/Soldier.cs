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
        soldierMovement.InitializeComponents(soldierAnimator, soldierAttack ,soldierHealth , rigid, soldierCollider);
        soldierHealth.InitializeComponents(soldierAnimator,soldierAttack ,soldierCollider);
    }
    private void OnEnable() => InitializeSetUp();
    void InitializeSetUp()
    {
        //soldierData.UpdateSoldierStatus(DataBaseManager.Instance.playerData);
        soldierAnimator.InitializeSetUp();
        soldierAttack.InitializeSetUp(soldierData);
        soldierMovement.InitializeSetUp(soldierData);
        soldierHealth.InitializeSetUp(soldierData);
    }
    
    public void MoveDestination(Vector3 destination)
    {
        StartCoroutine(soldierMovement.MoveLoop(destination));
    }
    public void GetTargetSearch(TargetSearch _targetSearch) => soldierAttack.GetTargetSearch(_targetSearch);

    public void GetPlayer(Player _player)
    {
        player = _player;
        soldierHealth.GetPlayer(_player);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            //AudioManager.Instance.PlaySFX(SFX.Soldier_Die_Obstacle);
            PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
            player.soldierList.Remove(this);
            player.soldierCount--;
        }
    }
}
