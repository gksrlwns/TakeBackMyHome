using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed;
    public Vector3 newPos;
    public Player_Input player;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 dir = player.transform.TransformDirection(player.transform.position);
        rigid.velocity = dir * speed * Time.deltaTime;
    }
}
