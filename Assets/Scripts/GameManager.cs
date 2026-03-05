using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coinUI;
    public static GameManager gameManager;
    private int _lives = 3;
    private int _coins = 0;
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
    private void GotCoin()
    {
        _coins++;
        //extMeshPro mText = coinUI.GetComponent();

    }
}
