using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    //Script Tempat dan mekanis enemySpawn [Sementara Nonaktif] 

    [SerializeField] private GameObject enemiesPrefab;
    [SerializeField] private float maxSpawnTime;
    private float timeUntilSpawn;
    int enemiesAmount;
    int enemiesLimit = 4;
  
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    //selisih waktu yg didapat saat random dengan waktu perdetik (deltaTime)
    timeUntilSpawn -= Time.deltaTime;

    //Munculkan clone enemies, limit enemies
    if (timeUntilSpawn <= 0){
        if (enemiesAmount >= enemiesLimit){
            Destroy(gameObject);
        }
        else{
            Instantiate(enemiesPrefab, transform.position, Quaternion.identity);                SetTimeUntilSpawn();
            enemiesAmount += 1;
            Debug.Log(enemiesAmount);
            SetTimeUntilSpawn();
        }
    }
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(3, maxSpawnTime);
    }
}
