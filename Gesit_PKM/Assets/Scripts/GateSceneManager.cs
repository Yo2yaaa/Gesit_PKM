using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateSceneManager : MonoBehaviour
{
    private enum GateType
    {
        Open,
        Close,
        BossGate
    }

    [SerializeField] private GateType gateType;

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
            switch (gateType)
            {
                case GateType.Open:
                    SceneLoader.Instance.LoadNextScene();
                    break;
                case GateType.Close:
                    if (GameManager.Instance.GetEnemyList().Length <= 0)
                    {
                        questionPage.gameObject.SetActive(true);
                    }
                    break;
                case GateType.BossGate:
                    if (GameManager.Instance.GetEnemyList().Length <= 0)
                    {
                        SwitchSpriteToOpen();
                        Invoke(nameof(LoadWinCondition), 1);
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void SwitchSpriteToOpen()
    {
        spriteRenderer.sprite = openGateSprite;
    }

    public void LoadWinCondition()
    {
        GameManager.Instance.Win();
    }
}
