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
        if (!agent.enabled) return;
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
        if (agent.isOnNavMesh)
        {
            zombieAnimator.OnMove(agent.velocity);
            if (target == null) direction = GameManager.instance.player.transform.position + new Vector3(0,0,-5f);
            else direction = target.transform.position;
            agent.SetDestination(direction);
            if (agent.remainingDistance > 0.1f && agent.remainingDistance <= attackRange)
            {
                agent.isStopped = true;
                zombieAttack.AttackMotion();
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            Debug.LogWarning("Cannot move agent because it is not on the NavMesh.");
        }
        //zombieAnimator.OnMove(agent.velocity);
        //if (target == null) direction = GameManager.instance.player.transform.position;
        //else direction = target.transform.position;
        //if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        //{
        //    //agent.Warp(hit.position); // NavMesh 위로 에이전트 이동
        //}
        //else
        //{
        //    Debug.LogWarning("Zombie is not on the NavMesh!");
        //    return;
        //}
        //agent.SetDestination(direction);
        //if (agent.remainingDistance > 0 && agent.remainingDistance <= attackRange)
        //{
        //    agent.isStopped = true;
        //    zombieAttack.AttackMotion();
        //}
        //else
        //{
        //    agent.isStopped = false;
        //}
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
