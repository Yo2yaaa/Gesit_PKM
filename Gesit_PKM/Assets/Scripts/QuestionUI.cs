using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;
using static GateSceneManager;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private GateSceneManager gateSceneManager;
    [SerializeField] private QuestionSO questionSO;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] optionsArray;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = questionSO.GetQuestion();

        for (int i = 0; i < optionsArray.Length; i++)
        {
            optionsArray[i].text = questionSO.GetAnswer(i);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryAnswer(int index)
    {
        // string answer = GetComponentInChildren<TMP_Text>().text;
        int answer = questionSO.GetCorrectAnswerIndex();
        if (index == answer)
        {
            Time.timeScale = 1;
            // Debug.Log("You are correct!");
            gateSceneManager.SwitchSpriteToOpen();
            gameObject.SetActive(false);

            GateType gateType = gateSceneManager.GetGateType();
            if (gateType == GateType.Close)
            {
                Invoke(nameof(FadeTransition), 1);
            }
            else if (gateType == GateType.BossGate)
            {
                Invoke(nameof(DoLoadWinCondition), 1);
            }
        }
    }

    private void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene();
    }

    private void FadeTransition()
    {
        gateSceneManager.FadeTransition();
    }

    private void DoLoadWinCondition()
    {
        gateSceneManager.LoadWinCondition();
    }
}
