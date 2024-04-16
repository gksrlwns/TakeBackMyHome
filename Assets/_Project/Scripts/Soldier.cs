using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed;
    public Vector3 newPos;
    public Player player;
    CapsuleCollider capsuleCollider;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
    }
    private void Start()
    {
        float x = Random.Range(0, 0.1f);
        float z = Random.Range(0, 0.1f);
        newPos = new Vector3(x, 0, z);
        //rigid.velocity = newPos*5f;
        StartCoroutine(EnableCollider());
    }
    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        rigid.AddForce(dir.normalized, ForceMode.Impulse);
    }

    IEnumerator EnableCollider()
    {
        yield return null;
        capsuleCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {

        }
    }
}
