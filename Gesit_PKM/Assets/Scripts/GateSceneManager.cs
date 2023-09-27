using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

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
    [SerializeField] private MMFeedbacks fadeTransition;
    [SerializeField] private MMFeedbacks successFeedbacks;

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
                    FadeTransition();
                    Invoke(nameof(LoadNextScene), 1);
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
                        successFeedbacks.PlayFeedbacks();
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
        successFeedbacks.PlayFeedbacks();
    }

    public void FadeTransition()
    {
        fadeTransition.PlayFeedbacks();
    }

    public void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene();
    }

    private void LoadWinCondition()
    {
        GameManager.Instance.Win();
    }
}
