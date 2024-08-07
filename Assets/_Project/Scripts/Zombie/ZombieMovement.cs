using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class ZombieMovement : MonoBehaviour, IMovable
{
    NavMeshAgent agent;
    ZombieAttack zombieAttack;
    ZombieAnimator zombieAnimator;
    TargetSearch targetSearch;
    SoldierHealth target;

    public bool isMove;
    float attackRange;
    Vector3 direction;

    public void InitializeComponents(ZombieAttack _zombieAttack, ZombieAnimator _zombieAnimator,TargetSearch _targetSearch, NavMeshAgent _agent)
    {
        agent = _agent;
        zombieAnimator = _zombieAnimator;
        zombieAttack = _zombieAttack;
        targetSearch = _targetSearch;
    }
    public void InitializeSetUp(ZombieData zombieData)
    {
        agent.speed = zombieData.MoveSpeed;
        attackRange = zombieData.AttackRange;
    }

    private void Update()
    {
        if (!isMove) return;
        Move();
    }
    
    public IEnumerator CheckNearTargetLoop()
    {
        while(true)
        {
            target = targetSearch.NearTarget<SoldierHealth>(transform);
            zombieAttack.SetTarget(target);
            yield return CoroutineManager.DelaySeconds(1f);
        }
        
    }
    public void Move()
    {
        zombieAnimator.OnMove(agent.velocity);
        if (target == null) direction = GameManager.instance.player.transform.position;
        else direction = target.transform.position;
        agent.SetDestination(direction);
        if (agent.remainingDistance <= attackRange && agent.remainingDistance != 0)
        {
            agent.isStopped = true;
            zombieAttack.AttackMotion();
        }
        else
        {
            agent.isStopped = false;
        }
        Debug.Log($"{agent.velocity}, {agent.remainingDistance}");
    }


    public void Rotate()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    //public void SetTarget(Transform _target) => target = _target;

}
