using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _photonPrefab;
    [SerializeField]
    private float _laserFireRate = 0.2f;
    private float _photonFireRate = 0.5f;
    private float _canFire = -1f;
    private int _startAmmo = 15;
    private int _currentAmmo;

    private int _lives = 3;

    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private int _shieldState = 0;
    private bool _isSpeedBoostActive = false;
    private bool _isPhotonActive = false;
    private bool _isPlayerInvulnerable;
    private float _playerDamageTime;
    private float _playerSafePeriod = 0.2f;

    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private GameObject _leftEngineVisualiser, _rightEngineVisualiser;
    
    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserShotClip;

    private Animator _playerAnim;
    private Shield _shieldStateColor;
    private Camera_Shake _cameraShake;

    void Start()
    {
        _currentAmmo = _startAmmo;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _playerAnim = GetComponent<Animator>();
        _shieldStateColor = GameObject.Find("Shield").GetComponentInChildren<Shield>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<Camera_Shake>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio source on player is NULL");
        }
        else
        {
            _audioSource.clip = _laserShotClip;
        }

        if (_playerAnim == null)
        {
            Debug.LogError("Player Animator is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetButton("Fire1") && Time.time > _canFire && _isPhotonActive == true)
        {
            FirePhoton();
        }
        else if (Input.GetButtonDown("Fire1") && Time.time > _canFire)
        {
            FireLaser();
        }

        if (Input.GetButton("Trigger1") && _isSpeedBoostActive == false)
        {
            _speed = 16f;
        }
        else if (Input.GetButtonUp("Trigger1") && _isSpeedBoostActive == false)
        {
            _speed = 10f;
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float _inputX = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), Mathf.Clamp(transform.position.y, -3.8f, 1.2f), 0);
        bool isMoving = (Mathf.Abs(_inputX)) > 0;
        _playerAnim.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            _playerAnim.SetFloat("x", _inputX);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _laserFireRate;        

        if (_currentAmmo <= 0)
        {
            return;
        }

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if (_isTripleShotActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        }

        _audioSource.Play();

        PlayerAmmo(-1);

    }

    void FirePhoton()
    {
        _canFire = Time.time + _photonFireRate;
        Instantiate(_photonPrefab, transform.position, Quaternion.identity);
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_playerDamageTime < Time.time)
        {
            _isPlayerInvulnerable = false;
        }

        if (_isPlayerInvulnerable == false)
        {
            /*if (_isShieldActive == true)
            {
                _isShieldActive = false;
                _shieldVisualiser.SetActive(false);
                return;
            }*/

            if (_shieldState == 3)
            {
                _shieldStateColor.ShieldColor(2);
                _shieldState -= 1;
                _isPlayerInvulnerable = true;
                _playerDamageTime = Time.time + _playerSafePeriod;
                return;
            }
            else if (_shieldState == 2)
            {
                _shieldStateColor.ShieldColor(1);
                _shieldState -= 1;
                _isPlayerInvulnerable = true;
                _playerDamageTime = Time.time + _playerSafePeriod;
                return;
            }
            else if (_shieldState == 1)
            {
                //_shieldVisualiser.SetActive(false);
                _shieldStateColor.ShieldColor(0);
                _shieldState -= 1;
                _isPlayerInvulnerable = true;
                _playerDamageTime = Time.time + _playerSafePeriod;
                return;
            }
            else
            {
                _lives -= 1;

                _isPlayerInvulnerable = true;
                _playerDamageTime = Time.time + _playerSafePeriod;
                StartCoroutine(_cameraShake.CamShake(0.4f, 0.1f));
            }
            /*_lives -= 1;

            _isPlayerInvulnerable = true;
            _playerDamageTime = Time.time + _playerSafePeriod;*/
        }


        if (_lives == 2)
        {
            _rightEngineVisualiser.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngineVisualiser.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        //tripleShotActive becomes true
        _isTripleShotActive = true;
        //start the powerdown coroutine for triple shot
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRoutine
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        if (_isSpeedBoostActive == false)
        {
            _speed = 10f;
            _isSpeedBoostActive = true;
            _speed *= _speedMultiplier;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _shieldState = 3;
        //_shieldVisualiser.SetActive(true);
        _shieldStateColor.ShieldColor(3);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void PlayerAmmo(int updateAmmo)
    {
        _currentAmmo += updateAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
    }

    public void RestoreHealth()
    {
        _lives += 1;
        if (_lives >= 3)
        {
            _lives = 3;
            _rightEngineVisualiser.SetActive(false);
            _leftEngineVisualiser.SetActive(false);
        } else if (_lives == 2)
        {
            _leftEngineVisualiser.SetActive(false);
        }
        _uiManager.UpdateLives(_lives);
    }

    public void PhotonActive()
    {
        StartCoroutine(PhotonActiveRoutine());
    }

    IEnumerator PhotonActiveRoutine()
    {
        _isPhotonActive = true;
        yield return new WaitForSeconds(5);
        _isPhotonActive = false;
    }
}
