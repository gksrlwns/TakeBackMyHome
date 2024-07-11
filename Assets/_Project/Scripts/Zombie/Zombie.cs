using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    ZombieData zombieData;

    [Header("Zombie Status Info")]
    [SerializeField] float maxHp;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;
    public float curHp;

    public void Init()
    {
        maxHp = zombieData.MaxHp;
        attackRange = zombieData.AttackRange;
        attackSpeed = zombieData.AttackSpeed;
        moveSpeed = zombieData.MoveSpeed;
    }
}
