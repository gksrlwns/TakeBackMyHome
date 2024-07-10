using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "ScriptableObjcet/ZombieData", order = 0)]
public class ZombieData : ScriptableObject
{
    [Header("Zombie Status Info")]
    [SerializeField] float maxHp;
    [SerializeField] float damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;

    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float AttackRange { get => attackRange; }
    public float AttackSpeed { get => attackSpeed; }

}