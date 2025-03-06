using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText; 
    public GameObject scoreTableText; 

    private int currentScore;
    private int highScore;


    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTableText.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }


    public void AddScore(int points){
        currentScore += points;

        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }
    // Update is called once per frame
    void UpdateScoreUI(){
        scoreText.text = "Score: " + currentScore.ToString("D4");
        highScoreText.text = "High Score: " + highScore.ToString("D4");
    }
}
