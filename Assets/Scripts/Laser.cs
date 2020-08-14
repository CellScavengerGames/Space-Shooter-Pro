using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _laserSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        //translate laser up on instantiate
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 8)
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
}
