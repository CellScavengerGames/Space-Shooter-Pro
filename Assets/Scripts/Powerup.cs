using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3f;
    [SerializeField] //0 = Triple Shot 1 = Speed 2 = Shield
    private int powerupID;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
