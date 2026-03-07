using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinUI;
    public static GameManager gm;
    private int _lives = 3;
    private int _coins = 0;
    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
            return;
        }
        gm = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoseLive()
    {
        _lives -= 1;
        Debug.Log($"Lives remaining: {_lives}");
        if (_lives > 0)
            SceneManager.LoadScene("Game");
        else
            SceneManager.LoadScene("GameOver");
    }
    public void GotCoin()
    {
        _coins++;
        if(_coins >= 100)
        {
            _coins = 0;
            _lives++;
        }
        coinUI.text = _coins.ToString();
    }
}