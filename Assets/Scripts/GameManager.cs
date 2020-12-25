using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

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
        if (_isGameOver && ( Input.GetKeyDown(KeyCode.R) || CrossPlatformInputManager.GetButtonDown("Fire")))
        {
            // User pressed the R key
            // Restart the level  
            SceneManager.LoadScene(1); //SceneManager.LoadScene("Game");
        }
    }

}
