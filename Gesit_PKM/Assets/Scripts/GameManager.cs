using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform winUI;
    [SerializeField] private Transform loseUI;

    protected override void Awake()
    {
        base.Awake();
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
        Time.timeScale = 0;
        winUI.gameObject.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        loseUI.gameObject.SetActive(true);
    }
}
