using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [Header("Enemy Pool")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> enemyWaves = new List<GameObject>();
    [SerializeField] private Transform spawnPos;
    [SerializeField, Min(10)] private float spawningTime;

    private bool isDead;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        enemyWaves = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemyWave = Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
            enemyWave.SetActive(true);
            enemyWaves.Add(enemyWave);
        }
    }

    IEnumerator SpawningTime()
    {
        if (enemyWaves.All(enemy => !enemy.activeInHierarchy))
        {
            yield return new WaitForSeconds(spawningTime);
            GameObject enemy = GetEnemyWave();

            List<GameObject> inActiveGameObjects = enemyWaves.Where(enemy => !enemy.activeInHierarchy).ToList();
            foreach (var item in inActiveGameObjects)
            {
                item.SetActive(true);
            }
            isDead = false;
        }
    }

    GameObject GetEnemyWave()
    {
        for (int i = 0; i < enemyWaves.Count; i++)
        {
            if (!enemyWaves[i].activeInHierarchy)
            {
                return enemyWaves[i];
            }
        }

        GameObject enemyWave = Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
        enemyWave.SetActive(false);
        enemyWaves.Add(enemyWave);

        return enemyWave;
    }

    public void ReturnEnemyWave(GameObject enemyWave)
    {
        enemyWave.transform.position = transform.position;
        enemyWave.SetActive(false);
        StartCoroutine(SpawningTime());
    }
}