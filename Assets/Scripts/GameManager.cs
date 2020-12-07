using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    public void GameOver()
    {
        _isGameOver = true;
    }

    void Update()
    {
        // Handle restarting the level
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // User pressed the R key
            // Restart the level  
            SceneManager.LoadScene(1); //SceneManager.LoadScene("Game");
        }
    }

}
