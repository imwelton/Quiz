using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    public RoundData[] todasAsRodadas;
    public int rodadaIndex;
    public int playerHighScore;
    public AudioClip audioClick;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerProgress();
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu");
    }


    public void SetRoundData(int round)
    {
        rodadaIndex = round;
        audioSource.PlayOneShot(audioClick);
    }
    public RoundData GetCurrenntRoundData()
    {
        return todasAsRodadas[rodadaIndex];
    }
    public void EnviarNovoHighScore(int newScore)
    {
        if (newScore > playerHighScore)
        {
            playerHighScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighScore()
    {
        return playerHighScore;
    }
    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highScore", playerHighScore);
    }
    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            playerHighScore = PlayerPrefs.GetInt("highScore");
        }
    }
}
