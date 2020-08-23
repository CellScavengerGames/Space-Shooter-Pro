using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer _shieldStateColor;
    // Start is called before the first frame update
    void Start()
    {
        _shieldStateColor = GetComponent<SpriteRenderer>();
        ShieldColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShieldColor(int _shieldState)
    {
        switch (_shieldState)
        {
            case 0:
                _shieldStateColor.color = new Color(255, 255, 255, 0);
                break;
            case 1:
                _shieldStateColor.color = new Color(255, 0, 0, 255);
                break;
            case 2:
                _shieldStateColor.color = new Color(135, 0, 255, 255);
                break;
            case 3:
                _shieldStateColor.color = new Color(0, 255, 0, 255);
                break;
        }
    }
}
