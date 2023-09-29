using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour
{
    [SerializeField] private int startingLevelIndex = 2;
    [SerializeField] private int levelTotalPerIsland = 8;
    [SerializeField] private Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", startingLevelIndex);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i * levelTotalPerIsland + startingLevelIndex > levelAt)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
