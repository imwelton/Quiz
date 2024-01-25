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
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu");
    }


    public void SetRoundData(int round)
    {
        rodadaIndex = round;
        audioSource.clip = null;
        audioSource.PlayOneShot(audioClick);
    }
    public RoundData GetCurrenntRoundData()
    {
        return todasAsRodadas[rodadaIndex];
    }
}
