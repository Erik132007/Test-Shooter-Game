using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [Header("Bullet Pool")]
    [SerializeField, Min(5)] private int poolCount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pooledObjects.Add(bullet);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}