using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreUI;
    public GameObject panelGameOver;

    private bool gameOver = false;

    private void Awake()
    {
        panelGameOver.SetActive(false);
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        panelGameOver.SetActive(true);
    }
}
