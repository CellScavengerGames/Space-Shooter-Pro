﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //create movement speed variable
    [SerializeField]
    private float _speed = 10f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
<<<<<<< HEAD
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    private bool _isPlayerInvulnerable;
    private float _playerDamageTime;
    private float _playerSafetyPeriod = 0.2f;
=======
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c

    //variable reference to the shield visualiser
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

    void Start()
    {
        //set initial location to zero in all axes
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

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
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        //create local variables for horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //create vector3 variable to handle horizontal and vert inputs
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //Constrain movement in x and y
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), Mathf.Clamp(transform.position.y, -3.8f, 1.2f), 0);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;        

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if (_isTripleShotActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        }

        _audioSource.Play();

    }

    public void Damage()
    {
<<<<<<< HEAD
        if (_playerDamageTime < Time.time)
        {
            _isPlayerInvulnerable = false;
        }

        if (_isPlayerInvulnerable == false)
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                _shieldVisualiser.SetActive(false);
                _isPlayerInvulnerable = true;
                _playerDamageTime = Time.time + _playerSafetyPeriod;
                return;
            }

            _lives -= 1;

            _isPlayerInvulnerable = true;
            _playerDamageTime = Time.time + _playerSafetyPeriod;
        }
=======
        //If shield active
        //do nothing
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
            return;
        }

        _lives -= 1;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c

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
<<<<<<< HEAD
=======
        _isSpeedBoostActive = true;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
<<<<<<< HEAD
=======
        _isSpeedBoostActive = false;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualiser.SetActive(true);
    }

    //method to add 10 to score
    //communicate with UI to update score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
