using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
<<<<<<< HEAD
    private float _laserSpeed = 5.0f;
    private bool _isEnemyLaser = false;
=======
    private float _laserSpeed = 8.0f;
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    void MoveUp()
=======
        CalculateMovement();
    }

    void CalculateMovement()
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
    {
        //translate laser up on instantiate
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

<<<<<<< HEAD
        if (transform.position.y > 8)
=======
        if (transform.position.y >= 8)
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
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
<<<<<<< HEAD

    void MoveDown()
    {
        //translate laser down on instantiate
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(gameObject);
            }
        }
    }
=======
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
}
