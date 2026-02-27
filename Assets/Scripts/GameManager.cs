using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int _lives = 3;
    public static int _score = 0;
    public static void LoseLive()
    {
        _lives -= 1;
        if( _lives > 0)
        {
            SceneManager.LoadScene("Game");
            Debug.Log(_lives);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
