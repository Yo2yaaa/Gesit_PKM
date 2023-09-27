using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform winUI;
    [SerializeField] private Transform loseUI;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Enemy[] GetEnemyList()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        return enemies;
    }

    public void Win()
    {
        winUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        loseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
