using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private AudioSource _audio;
    private TextMeshProUGUI coinUI;
    private TextMeshProUGUI lifeUI;
    public Player player;

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
        _audio = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var lifeObj = GameObject.Find("Lifes");
        var coinObj = GameObject.Find("Coins");
        if (lifeObj == null || coinObj == null) return;

        lifeUI = lifeObj.GetComponent<TextMeshProUGUI>();
        coinUI = coinObj.GetComponent<TextMeshProUGUI>();

        player = FindAnyObjectByType<Player>();
        if (_lives <= 0)
        {
            _lives = 3;
            PlayAudio(SoundManager.AudioClips.StartSound);
        }

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
        if (player != null)
            player.gameObject.SetActive(false);

        _lives--;
        if (lifeUI != null)
            lifeUI.text = _lives.ToString();

        if (_lives > 0)
        {
            PlayAudio(voidDeath
                ? SoundManager.AudioClips.VoidDeath
                : SoundManager.AudioClips.NormalDeath);
            StartCoroutine(LoadSceneAfterDelay(1f));
        }
        else
        {
            _audio.Stop();
            PlayAudio(SoundManager.AudioClips.GameOver);
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GotCoin()
    {
        _coins++;
        PlayAudio(SoundManager.AudioClips.Coin);

        if (_coins >= 100)
        {
            _coins = 0;
            _lives++;
            if (lifeUI != null)
                lifeUI.text = _lives.ToString();
        }

        if (coinUI != null)
            coinUI.text = _coins.ToString();
    }

    public void PlayAudio(SoundManager.AudioClips clip)
    {
        if (_audio == null) return;
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