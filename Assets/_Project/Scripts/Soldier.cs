using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Vector3 dir = GameManager.instance.transform.position - transform.position;
        //dir = dir.normalized;
        //rigid.MovePosition(dir * speed * Time.deltaTime);
    }
}
