using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform winUI;
    [SerializeField] private Transform loseUI;

    private Enemy[] enemies;
    private PlayerController player;
    private Vector3 lastPlayerPosition;
    private GameObject playerPrefab;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        // playerPrefab = player.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        enemies = FindObjectsOfType<Enemy>();

        if (!player) return;
        lastPlayerPosition = player.transform.position;
    }

    public Enemy[] GetEnemyList()
    {
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

    public void Revive()
    {
        Time.timeScale = 1;
        loseUI.gameObject.SetActive(false);
    }
}
