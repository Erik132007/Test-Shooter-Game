using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [Header("Bullet Move")]
    [SerializeField, Min(25)] private int speed;
    [SerializeField] private Rigidbody rigid;

    void Update()
    {
        Move();
    }

    void Move()
    {
        rigid.velocity = Vector3.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}