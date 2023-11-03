using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Enemy Move Modules")]
    [SerializeField] private float speed;

    void Update()
    {
        Move();
    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            EnemyPool.instance.ReturnEnemyWave(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            EnemyPool.instance.ReturnEnemyWave(gameObject);
        }
    }
}