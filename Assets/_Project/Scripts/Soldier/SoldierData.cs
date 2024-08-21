using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "ScriptableObjcet/SoldierData")]
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
    public float AttackSpeed { get => attackSpeed; set { attackSpeed = value; } }
    public float AttackRange { get => attackRange; }
    public float MoveSpeed { get => moveSpeed; }


    //public void UpdateSoldierStatus(SoldierStatus soldierStatus)
    //{
    //    maxHp = soldierStatus.soldierMaxHp;
    //    damage = soldierStatus.soldierDamage;
    //    attackSpeed = soldierStatus.soldierAttackSpeed;
    //}

}

