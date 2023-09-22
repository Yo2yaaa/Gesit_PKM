using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSceneManager : MonoBehaviour
{
    public static GateSceneManager Instance;
    [SerializeField] private Transform questionPage;
    [SerializeField] private Sprite openGateSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if (GameManager.Instance.GetEnemyList().Length <= 0)
            {
                questionPage.gameObject.SetActive(true);
                // Time.timeScale = 0;
            }
        }
    }

    public void SwitchSpriteToOpen()
    {
        spriteRenderer.sprite = openGateSprite;
    }
}
