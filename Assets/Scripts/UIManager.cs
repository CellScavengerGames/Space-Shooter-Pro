using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Slider _boostBar;
    private GameManager _gameManager;

    private float _maxBoost = 100f;
    private float _currentBoost;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoText.text = "Ammo: " + 15;
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _currentBoost = _maxBoost;
        _boostBar.maxValue = _maxBoost;
        _boostBar.value = _maxBoost;

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on currentLives index
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateAmmo(int playerAmmo)
    {
        _ammoText.text = "Ammo: " + playerAmmo.ToString();
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _restartLevelText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UseBoost(float boost)
    {
        _currentBoost = boost;
        if (_currentBoost <= 0)
        {
            _currentBoost = 0;
        }
        _boostBar.value = _currentBoost;
    }

    /*IEnumerator UseBoostRoutine(int boost)
    {
        if (_currentBoost >= boost)
        {
            _currentBoost -= boost;
            _boostBar.value = _currentBoost;
        }
        else
        {
            Debug.Log("Not enough boost");
        }
        
    }*/

    /*public void UseBoost(int boost)
    {
        StartCoroutine(UseBoostRoutine(boost));
    }*/
}
