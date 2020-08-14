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

            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0.5f;
            _audioSource.Play();
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
