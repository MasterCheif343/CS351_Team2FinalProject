using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyAnimalPrefab;

    [SerializeField] public int spawnEveryNumberOfDays = 1;

    private int lastSpawnedDay = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        DayProgression.OnDayChanged += TrySpawnEnemy;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        DayProgression.OnDayChanged -= TrySpawnEnemy;
    }

    private void TrySpawnEnemy()
    {
        if(DayProgression.Day >= lastSpawnedDay + spawnEveryNumberOfDays)
        {
            SpawnEnemy();
            lastSpawnedDay = DayProgression.Day;
        }
    }
    private void SpawnEnemy()
    {
        if (EnemyAnimalPrefab.Length == 0)
        {
            Debug.LogError("No enemy animals set for spawning!");
            return;
        }

        GameObject chosenEnemy = EnemyAnimalPrefab[Random.Range(0, EnemyAnimalPrefab.Length)];

        Vector3 spawnPos = GetSafeSpawnPosition();
        Instantiate(chosenEnemy, spawnPos, Quaternion.identity);
    }

    private Vector3 GetSafeSpawnPosition()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f),
                0f);

            Collider2D hit = Physics2D.OverlapCircle(pos, 0.4f, LayerMask.GetMask("Plants"));

            if (hit == null)
                return pos;
        }

        return transform.position; 
    }
}
