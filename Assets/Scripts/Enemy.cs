using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
    private float _enemySpeed = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
=======
    private float _enemySpeed = 4f;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c

    private Player _player;
    private Animator _enemyDestroyedAnim;
    private AudioSource _audioSource;
<<<<<<< HEAD
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private bool _isEnemyDestroyed;
=======
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c

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
<<<<<<< HEAD
        CalculateMovement();

        if (Time.time > _canFire && _isEnemyDestroyed == false)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
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

=======
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
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

            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
<<<<<<< HEAD
            _isEnemyDestroyed = true;
            Destroy(GetComponent<Collider2D>());
=======
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
<<<<<<< HEAD
            _isEnemyDestroyed = true;
            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
=======
            Destroy(other.gameObject);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
            Destroy(this.gameObject, 2.8f);
            
            if (_player != null)
            {
                _player.AddScore(10);
            }
            
        }
    }
}
