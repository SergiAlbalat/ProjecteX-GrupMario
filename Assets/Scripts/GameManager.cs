using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    [SerializeField] private static GameObject coinUI;
    private static int _lives = 3;
    private static int _coins = 0;
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
    private static void GotCoin()
    {
        _coins++;
        //extMeshPro mText = coinUI.GetComponent();

    }
}
