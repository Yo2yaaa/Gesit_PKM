using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnowledgeItem : MonoBehaviour
{
    [SerializeField] private KnowledgeItemSpawner knowledgeItemSpawner;
    [SerializeField] private Transform knowledgePage;
    [SerializeField] private TMP_Text knowledgeText;

    // Start is called before the first frame update
    void Start()
    {
        knowledgeText.text = knowledgeItemSpawner.GetKnowledgeItemSO().knowledgeText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            knowledgePage.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
