using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "ScriptableObjcet/SoldierData", order = 0)]
public class SoldierData : ScriptableObject
{
    [Header("Soldier Status Info")]
    [SerializeField] float maxHp;
    [SerializeField] float damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;

    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float AttackSpeed { get => attackSpeed; }
    public float AttackRange { get => attackRange; }

}
