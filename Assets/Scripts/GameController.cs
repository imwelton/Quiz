using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoTimer;
    public TextMeshProUGUI highScoreText;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelDePerguntas;
    public GameObject painelFimRodada;

    public DataController dataController;
    public RoundData rodadaAtual;
    public QuestionData[] questionPool;

    public bool rodadaAtiva;
    public float tempoRestante;
    public int questionIndex;
    public int playerScore;

    public AudioClip audioClipAcertou;
    public AudioClip audioClipErrou;

    List<int> usedValues = new List<int>();
    public List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindAnyObjectByType<DataController>();
        rodadaAtual = dataController.GetCurrenntRoundData();
        questionPool = rodadaAtual.perguntas;
        tempoRestante = rodadaAtual.limiteDeTempo;

        UpdateTimer();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        rodadaAtiva = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (rodadaAtiva)
        {
            tempoRestante -= Time.deltaTime;
            UpdateTimer();
            if (tempoRestante <= 0)
            {
                EndRound();
            }
        }
    }

    public void UpdateTimer()
    {
        textoTimer.text = "Timer: " + Mathf.Round(tempoRestante).ToString();
    }

    public void ShowQuestion()
    {
        RemoveAnswerButtons();
        int random = Random.Range(0, questionPool.Length);
        while (usedValues.Contains(random))
        {
            random = Random.Range(0, questionPool.Length);
        }

        QuestionData questionData = questionPool[random];
        usedValues.Add(random);
        textoPergunta.text = questionData.textoDaPergunta;

        for(int i=0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtongameObject = answerButtonObjectPool.GetObject();
            answerButtongameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtongameObject);
            AnswerButton answerButton = answerButtongameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.respostas[i]);
        }
    }

    public void RemoveAnswerButtons()
    {
        while(answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool estaCorreto)
    {
        tempoRestante = 30;
        if (estaCorreto)
        {
            Audio.Instance.PlayOneShot(audioClipAcertou);
            playerScore += rodadaAtual.pontosPorAcerto;
            textoPontos.text = "Score: " + playerScore.ToString();
        }else if (!estaCorreto)
        {
            Audio.Instance.PlayOneShot(audioClipErrou);
        }

        if(questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {  
        rodadaAtiva = false;
        dataController.EnviarNovoHighScore(playerScore);
        highScoreText.text = "High Score: " + dataController.GetHighScore().ToString();
        painelDePerguntas.SetActive(false);
        painelFimRodada.SetActive(true);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
