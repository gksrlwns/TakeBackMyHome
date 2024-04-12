using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Transform[] spawnPos;
    public Bounds bounds;
    CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * Time.deltaTime * speed * 3f;
        Vector3 forVec = Vector3.forward * Time.deltaTime * speed;
        transform.Translate(nextVec + forVec);
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }
    private void OnDrawGizmos()
    {
        //Color color = Color.green;
        //color.g = 0.8f;
        //Gizmos.color = Color.green;
        //Matrix4x4 matrix = transform.localToWorldMatrix;
        //matrix.SetTRS(matrix.GetPosition(), matrix.rotation, Vector3.one);

        //Gizmos.matrix = matrix;
        //Gizmos.DrawSphere(bounds.center, 2f);
        //DrawGizmosCircle(transform.position, 2f, new Vector3(0,2f,0), Color.green);
        DrawGizmosCircleXZ(transform.position, 2f);
    }

    public static void DrawGizmosCircle(Vector3 pos, float radius, Vector3 up, Color color, int step = 10, Action<Vector3> action = null)
    {
        float theta = 360f / (float)step;
        Vector3 cross = Vector3.Cross(up, Vector3.up);
        if (cross.magnitude == 0f)
        {
            cross = Vector3.forward;
        }

        Vector3 prev = pos + Quaternion.AngleAxis(0f, up) * cross * radius;
        Vector3 next = prev;
        Gizmos.color = color;

        for (int i = 1; i <= step; ++i)
        {
            next = pos + Quaternion.AngleAxis(theta * (float)i, up) * cross * radius;

            Gizmos.DrawLine(prev, next);

            if (null != action)
            {
                action(prev);
            }

            prev = next;
        }
    }
    public static Vector3 DrawGizmosCircleXZ(Vector3 pos, float radius, int circleStep = 10, float ratioLastPt = 1f)
    {
        float theta, step = (2f * Mathf.PI) / (float)circleStep;
        Vector3 p0 = pos;
        Vector3 p1 = pos;
        for (int i = 0; i < circleStep; ++i)
        {
            theta = step * (float)i;
            p0.x = pos.x + radius * Mathf.Sin(theta);
            p0.z = pos.z + radius * Mathf.Cos(theta);

            theta = step * (float)(i + 1);
            p1.x = pos.x + radius * Mathf.Sin(theta);
            p1.z = pos.z + radius * Mathf.Cos(theta);
            Gizmos.DrawLine(p0, p1);
        }

        theta = step * ((float)circleStep * ratioLastPt);
        p0.x = pos.x + radius * Mathf.Sin(theta);
        p0.z = pos.z + radius * Mathf.Cos(theta);

        return p0;
    }
    void EnterPoint(Bounds enterPointBounds, GameObject playerGameObject)
    {
        float randomPointX = transform.position.x + enterPointBounds.center.x + UnityEngine.Random.Range(enterPointBounds.extents.x * -0.5f, enterPointBounds.extents.x * 0.5f);
        float randomPointY = transform.position.y + enterPointBounds.center.y + UnityEngine.Random.Range(enterPointBounds.extents.y * -0.5f, enterPointBounds.extents.y * 0.5f);
        float randomPointZ = transform.position.z + enterPointBounds.center.z + UnityEngine.Random.Range(enterPointBounds.extents.z * -0.5f, enterPointBounds.extents.z * 0.5f);

        playerGameObject.transform.position = new Vector3(randomPointX, randomPointY, randomPointZ);
    }
    
   

}
