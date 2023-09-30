using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using _Joytstick.Scripts;

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
                        GameInput.Instance.SetJoystickCanvas(false);
                        questionPage.gameObject.SetActive(true);
                    }
                    break;
                case GateType.BossGate:
                    if (GameManager.Instance.GetEnemyList().Length <= 0)
                    {
                        GameInput.Instance.SetJoystickCanvas(false);
                        successFeedbacks.PlayFeedbacks();
                        SwitchSpriteToOpen();
                        Invoke(nameof(LoadWinCondition), 1);
                        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                        // Check to unlock the next level
                        if (nextSceneIndex > PlayerPrefs.GetInt("levelAt"))
                        {
                            PlayerPrefs.SetInt("levelAt", nextSceneIndex);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void SetJoystickCanvasToActive()
    {
        GameInput.Instance.SetJoystickCanvas(true);
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
