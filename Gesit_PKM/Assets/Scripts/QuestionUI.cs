using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;

public class QuestionUI : MonoBehaviour
{
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
            // Debug.Log("You are correct!");
            GateSceneManager.Instance.SwitchSpriteToOpen();
            gameObject.SetActive(false);
            Invoke(nameof(LoadNextScene), 2f);
        }
    }

    private void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene();
    }
}
