using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }
<<<<<<< HEAD

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
=======
>>>>>>> 0dbc1b89837988408af8895b30b94a0ec0993b5c
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
