using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinUI;
    [SerializeField] private TextMeshProUGUI lifeUI;
    public static GameManager gm;
    private AudioSource _audio;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_lives <= 0)
        {
            _lives = 3;
        }
        _audio = gameObject.GetComponent<AudioSource>();
        lifeUI = GameObject.Find("Lifes").GetComponent<TextMeshProUGUI>();
        coinUI = GameObject.Find("Coins").GetComponent<TextMeshProUGUI>();

        lifeUI.text = _lives.ToString();
        coinUI.text = _coins.ToString();
        _audio.Stop();
        PlayAudio(SoundManager.AudioClips.Music);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoseLive()
    {
        _lives--;
        lifeUI.text = _lives.ToString();
        Debug.Log($"Lives remaining: {_lives}");
        if (_lives > 0)
            SceneManager.LoadScene("Game");
        else
        {
            PlayAudio(SoundManager.AudioClips.GameOver);
            SceneManager.LoadScene("GameOver");
        }
    }
    public void GotCoin()
    {
        _coins++;
        if(_coins >= 100)
        {
            _coins = 0;
            _lives++;
            lifeUI.text = _lives.ToString();
        }
        coinUI.text = _coins.ToString();
    }
    public void PlayAudio(SoundManager.AudioClips clip)
    {
        AudioClip audioClip = SoundManager.sm.GetClip(clip);
        if (audioClip != null)
            _audio.PlayOneShot(audioClip);
    }
}