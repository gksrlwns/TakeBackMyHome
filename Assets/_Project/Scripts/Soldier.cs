using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed;
    public Vector3 newPos;
    public Player player;
    WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(3f);
    CapsuleCollider capsuleCollider;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
    }
    IEnumerator VelocityZero()
    {
        yield return seconds;
    }

    void UnFreezeRotationY()
    {
        rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            this.gameObject.SetActive(false);
            player.soldierCount--;
        }
    }
}
