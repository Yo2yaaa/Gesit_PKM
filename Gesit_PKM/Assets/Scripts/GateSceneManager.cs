using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using _Joytstick.Scripts;

public class GateSceneManager : MonoBehaviour
{
    public enum GateType
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
    [SerializeField] private MMFeedbacks wrongAnswerFeedbacks;
    [SerializeField] private InterstitialAd ads;

    private SpriteRenderer spriteRenderer;
    private PlayerController player;

    private void Awake()
    {
        Instance = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            player = playerController;
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
                        Time.timeScale = 0;
                    }
                    break;
                case GateType.BossGate:
                    if (GameManager.Instance.GetEnemyList().Length <= 0)
                    {
                        questionPage.gameObject.SetActive(true);
                        Time.timeScale = 0;
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void WrongAnswer()
    {
        wrongAnswerFeedbacks.PlayFeedbacks();
    }

    public GateType GetGateType()
    {
        return gateType;
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

    public void LoadWinCondition()
    {
        ads.ShowAd();
        GameManager.Instance.Win();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check to unlock the next level
        if (nextSceneIndex > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneIndex);
        }
    }

    public void UnpauseTime()
    {
        Time.timeScale = 1;
    }
}
