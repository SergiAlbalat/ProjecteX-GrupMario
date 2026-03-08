using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinUI;
    [SerializeField] private TextMeshProUGUI lifeUI;
    [SerializeField] public Player player;
    public static GameManager gm;
    private AudioSource _audio;
    private int _lives = 0;
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
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindAnyObjectByType<Player>();
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        if (_lives <= 0)
        {
            PlayAudio(SoundManager.AudioClips.StartSound);
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

    public void LoseLive(bool voidDeath)
    {
        player.gameObject.SetActive(false);
        _lives--;
        lifeUI.text = _lives.ToString();
        Debug.Log($"Lives remaining: {_lives}");
        if (_lives > 0)
        {
            StartCoroutine(LoadSceneAfterDelay(1f));
            if (voidDeath)
            {
                PlayAudio(SoundManager.AudioClips.VoidDeath);
            }
            else
            {
                PlayAudio(SoundManager.AudioClips.NormalDeath);
            }
        }
        else
        {
            PlayAudio(SoundManager.AudioClips.GameOver);
            SceneManager.LoadScene("GameOver");
        }
    }
    public void GotCoin()
    {
        GameManager.gm.PlayAudio(SoundManager.AudioClips.Coin);
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
    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game");
    }
}