/* Adam Krenek
 * Final Game Project
 * This script manages how the enemies will spawn
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyAnimalPrefab;

    [SerializeField] public int spawnEveryNumberOfDays = 1;

    [SerializeField] private float delaySpawnBySeconds = 10f; 

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
            StartCoroutine(SpawnEnemyDelayed(delaySpawnBySeconds));
            lastSpawnedDay = DayProgression.Day;
        }
    }
    private IEnumerator SpawnEnemyDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        if(EnemyAnimalPrefab.Length == 0)
        {
            Debug.LogError("No enemy set for spawning, blin!");
            yield break;
        }

        GameObject choseEnemy = EnemyAnimalPrefab[Random.Range(0, EnemyAnimalPrefab.Length)];

        Vector3 spawnPosition = GetSafeSpawnPosition();
        Instantiate(choseEnemy, spawnPosition, Quaternion.identity);    
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
