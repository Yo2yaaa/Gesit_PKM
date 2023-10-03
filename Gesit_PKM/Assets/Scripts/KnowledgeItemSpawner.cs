using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeItemSpawner : MonoBehaviour
{
    [SerializeField] private KnowledgeItemSO knowledgeItemSO;
    [SerializeField] private Transform knowledgeItemGameObject;

    private Vector3 spawnPosition;
    private bool hasSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetEnemyList().Length == 1)
        {
            Enemy lastEnemy = GameManager.Instance.GetEnemyList()[0];
            spawnPosition = lastEnemy.transform.position;
        }
        else if (GameManager.Instance.GetEnemyList().Length <= 0 && !hasSpawn)
        {
            hasSpawn = true;
            Transform knowledgeTransform = Instantiate(knowledgeItemGameObject, spawnPosition, Quaternion.identity);
            knowledgeTransform.gameObject.SetActive(true);
        }
    }

    public KnowledgeItemSO GetKnowledgeItemSO()
    {
        return knowledgeItemSO;
    }
}
