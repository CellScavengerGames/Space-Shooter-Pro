using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    private float _photonSpeed = 15f;
    //[SerializeField]
    //private Transform _target;
    //private GameObject[] photonTargets;
    //private Transform[] enemies;

    private void Start()
    {
        //Find all existing enemies
        //locate the closest enemy
    }

    private void Update()
    {
        //if there is an enemy or enemies on screen, move towards the closest
        //if not, move off screen
        MoveUp();
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _photonSpeed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
