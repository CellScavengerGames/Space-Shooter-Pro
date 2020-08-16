﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _laserSpeed = 10.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            //check if object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy parent too
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            //check if object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy parent too
            Destroy(this.gameObject);

        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
            Destroy(gameObject);
        }
    }
}
