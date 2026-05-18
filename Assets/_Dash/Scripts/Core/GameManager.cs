using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Playing,
        Paused,
        GameOver
    }

    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    
    private float score = 0;
    private bool isGameOver = false;

    private GameState _currentState;
    public GameState CurrentState => _currentState;

    public bool IsReady => _currentState == GameState.Ready;
    public bool IsPlaying => _currentState == GameState.Playing;
    public bool IsPaused => _currentState == GameState.Paused;
    public bool IsGameOverState => _currentState == GameState.GameOver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        SetState(GameState.Playing);
    }

    private void SetState(GameState newState)
    {
        _currentState = newState;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        score += Time.deltaTime * 10;
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        SetState(GameState.GameOver);
        
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Slow motion effect
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(1f);
        
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
