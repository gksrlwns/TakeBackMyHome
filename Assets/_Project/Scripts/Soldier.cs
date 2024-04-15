using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed;
    public Vector3 newPos;
    public Player player;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        rigid.AddForce(dir.normalized, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {

        }
    }
}
