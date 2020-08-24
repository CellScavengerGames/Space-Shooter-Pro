using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Player _player;
    private Animator _enemyDestroyedAnim;
    private AudioSource _audioSource;
    private bool _isEnemyDestroyed = false;

    private float _fireRate = 3f;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _enemyLaserPrefab;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        //null check player
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        //assign the component
        _enemyDestroyedAnim = GetComponent<Animator>();

        if (_enemyDestroyedAnim == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire && _isEnemyDestroyed == false)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //if bottom of screen, respawn at top
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 6.4f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            if (_player != null)
            {
                _player.Damage();
            }
            gameObject.tag = "Untagged";
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
            _isEnemyDestroyed = true;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            gameObject.tag = "Untagged";
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
            _isEnemyDestroyed = true;
            Destroy(other.gameObject);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.8f);
            
            if (_player != null)
            {
                _player.AddScore(10);
            }
            
        }
    }
}
