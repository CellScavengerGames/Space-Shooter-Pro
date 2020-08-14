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
<<<<<<< HEAD
    [SerializeField]
    private AudioClip _clip;
    
=======
    
    void Start()
    {
        
    }

>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
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
<<<<<<< HEAD

            AudioSource.PlayClipAtPoint(_clip, new Vector3(transform.position.x, transform.position.y, -10));

=======
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
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
