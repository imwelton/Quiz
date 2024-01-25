using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private DataController data;
    // Start is called before the first frame update
    void Start()
    {
        data = FindAnyObjectByType<DataController>();
    }
    public void StartGame(int round)
    {
        data.SetRoundData(round);
        SceneManager.LoadScene("Game");
    }
}
