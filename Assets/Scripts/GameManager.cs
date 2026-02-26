using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private int _lives = 3;
    private int _score = 0;
    private void Awake()
    {
        gameManager = new GameManager();
    }
    public void LoseLive()
    {
        _lives -= 1;
        if( _lives > 0)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
