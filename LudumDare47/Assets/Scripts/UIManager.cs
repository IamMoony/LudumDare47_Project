using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreUI;
    public Text highScoreUI;
    public GameObject panelGameOver;
    public Transform player;

    private bool gameOver = false;
    private int score;
    private int highScore;

    private void Awake()
    {
        panelGameOver.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreUI.text = "" + highScore;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene("Game");
        }
        else if (player != null)
        {
            score = Mathf.RoundToInt(Mathf.Clamp(player.position.x, 0, Mathf.Infinity));
            scoreUI.text = "" + score;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScore = 0;
            highScoreUI.text = "" + 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        panelGameOver.SetActive(true);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
