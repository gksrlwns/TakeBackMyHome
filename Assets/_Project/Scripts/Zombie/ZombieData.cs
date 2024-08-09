using UnityEngine;

public enum ZombieType { A,B,Running }

[CreateAssetMenu(fileName = "ZombieData", menuName = "ScriptableObjcet/ZombieData", order = 0)]
public class ZombieData : ScriptableObject
{
    [Header("Zombie Status Info")]
    [SerializeField] float maxHp;
    [SerializeField] float damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] ZombieType type;

    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float AttackSpeed { get => attackSpeed; }
    public float AttackRange { get => attackRange; }
    public ZombieType Type { get => type; }

}