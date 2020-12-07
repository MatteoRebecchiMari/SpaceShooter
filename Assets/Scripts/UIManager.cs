using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameoverText;

    [SerializeField]
    private Text _restartLevelText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _livesVisualizer;

    // Game manager
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = $"Score: {0}";
        _gameoverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game_Manager not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if(_flickeringCoroutine != null)
            StopCoroutine(_flickeringCoroutine);
    }

    // Update the score value
    public void UpdateScore(int scoreValue)
    {
        _scoreText.text = $"Score: {scoreValue}";
    }

    // Update the ui image based on lives count
    public void UpdateLives(int livesCount)
    {
        // Ensure to not outrange the index
        int index = Mathf.Clamp(livesCount, 0, _liveSprites.Length);

        // Set the image sprite based on the lives count
        _livesVisualizer.sprite = _liveSprites[index];

        if(livesCount <= 0)
        {
            // Set the game over status on the manager
            _gameManager.GameOver();

            // Show gameover (flickering) and restart level texts
            _gameoverText.gameObject.SetActive(true);
            _restartLevelText.gameObject.SetActive(true);
            _flickeringCoroutine = StartCoroutine(FlickeringGameoverCoroutine());
        }

    }

    Coroutine _flickeringCoroutine;

    // Handle flickering effect on gameover text
    IEnumerator FlickeringGameoverCoroutine()
    {
        
        while (true)
        {
            
            // Skip one frame
            yield return null;

            yield return new WaitForSeconds(0.5f);

            // Hide gameover text
            _gameoverText.gameObject.SetActive(false);

            // Skip one frame
            yield return null;

            yield return new WaitForSeconds(0.5f);

            // Show gameover text
            _gameoverText.gameObject.SetActive(true);

        }

    }

}
