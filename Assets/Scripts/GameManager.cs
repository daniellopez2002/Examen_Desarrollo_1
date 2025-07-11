using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CharacterController characterController;
    public TMP_Text HealthText;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    public TMP_Text GameOverText;
    public Camera MainCamera;
    public GameObject DamageItemPrefab;
    public GameObject FruitItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GameOverPanel;
    public GameObject InGamePanel;
    private bool isGameOver = false;
    private System.Random random = new System.Random();
    private float spawnInterval = 1.25f; // Time in seconds between item spawns
    private float spawnTimer = 0f; // Timer to track spawn intervals

    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MainCamera.sensorSize.y);
        if (isGameOver) return; // Skip update if game is over
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        UpdateUI();
        if (spawnTimer >= spawnInterval)
        {
            SpawnItem();
            spawnTimer = 0f; // Reset timer after spawning an item
        }
    }

    void UpdateUI()
    {
        HealthText.text = $"{characterController.Health}";
        ScoreText.text = $"{characterController.Score}";
        TimeText.text = $"{Math.Round(timer)}";
    }
    public void Damage(float damage)
    {
        characterController.TakeDamage(damage);
    }
    void SpawnItem()
    {
        int itemType = random.Next(0, 2); // Randomly choose between Danger (0) and Fruit (1)
        double randomX = random.NextDouble() * 3 - 1.5; // Random X position between -1.5 and 1.5
        switch (itemType)
        {
            case 0: // Danger item
                GameObject dangerItem = Instantiate(DamageItemPrefab);
                dangerItem.transform.position = new Vector3((float)randomX, MainCamera.transform.position.y + 1, 0);
                dangerItem.tag = "Danger";
                break;
            case 1: // Fruit item
                GameObject fruitItem = Instantiate(FruitItemPrefab);
                fruitItem.transform.position = new Vector3((float)randomX, MainCamera.transform.position.y + 1, 0);
                fruitItem.tag = "Fruit";
                break;
        }
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
        InGamePanel.SetActive(false);
        GameOverText.text = $"Game Over\nScore: {characterController.Score}\nTime: {Math.Round(timer)}";
    }

    public void RestartGame()
    {
        // Logic to restart the game, e.g., reload the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
