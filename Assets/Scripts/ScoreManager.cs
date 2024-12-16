using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timerText;

    private bool firstTenCoinsCollected = false; 
    private float gameStartTime;
    private float elapsedTime; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameStartTime = Time.time;
    }

    void Update()
    {
        if (!firstTenCoinsCollected)
        {
            elapsedTime = Time.time - gameStartTime; 
            if (timerText != null)
            {
                timerText.text = "Time: " + elapsedTime.ToString("F2") + "s"; 
            }
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void AddScore(int points)
    {
        score += points;

        if (score >= 10 && !firstTenCoinsCollected)
        {
            firstTenCoinsCollected = true; 
            float timeTaken = Time.time - gameStartTime; 

            Analytics.CustomEvent("Coin", new System.Collections.Generic.Dictionary<string, object>
            {
                { "TimeTaken", timeTaken }
            });

            Debug.Log($"First 10 coins collected in {timeTaken} seconds. Event sent to Analytics.");
        }
    }
}
