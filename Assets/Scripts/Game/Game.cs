using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    public eState State { get; set; } = eState.Title;
    public float Score { get; set; } = 0;

    static Game instance;
    public static Game Instance { get { return instance; } }

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI endTextUI;
    public TextMeshProUGUI highScoreTextUI;
    public TextMeshProUGUI timerUI;
    public GameObject startScreen;
    public GameObject endScreen;
    public AudioSource music;
    public Character player;

    private float highScore = 0;
    private float timer = 90.0f;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (State)
        {
            case eState.Title:
                endScreen.SetActive(false);
                startScreen.SetActive(true);
                break;
            case eState.StartGame:
                Score = 0;
                startScreen.SetActive(false);
                scoreUI.text = string.Format("{0:D4}", (int)Score);
                State = eState.Game;
                player.ResetCharacter();
                music.Play();
                break;
            case eState.Game:
                AddPoints(Time.deltaTime);
                if (player.health.health <= 0.1)
                {
                    music.Stop();
                    State = eState.GameOver;
                }
                break;
            case eState.GameOver:
                if (Score > highScore)
                {
                    highScore = Score;
                    highScoreTextUI.text = "High Score " + string.Format("{0:D4}", (int)highScore);
                }
                endScreen.SetActive(true);
                endTextUI.text = "You finished with -" + string.Format("{0:D4}", (int)Score) + "- points.";
                break;
            default:
                break;
        }
    }

    private void ShooterGame()
    {
        switch (State)
        {
            case eState.Title:
                endScreen.SetActive(false);
                startScreen.SetActive(true);
                break;
            case eState.StartGame:
                timer = 30;
                Score = 0;
                scoreUI.text = string.Format("{0:D4}", (int)Score);
                startScreen.SetActive(false);
                music.Play();
                music.volume = 0.01f;
                State = eState.Game;
                break;
            case eState.Game:
                AddPoints(Time.deltaTime);
                timerUI.text = string.Format("{0}", (int)timer);
                if (timer <= 0)
                {
                    music.Stop();
                    State = eState.GameOver;
                }
                break;
            case eState.GameOver:
                if (Score > highScore)
                {
                    highScore = Score;
                    highScoreTextUI.text = "High Score " + string.Format("{0:D4}", (int)highScore);
                }
                endScreen.SetActive(true);
                endTextUI.text = "You finished with -" + string.Format("{0:D4}", (int)Score) + "- points.";
                break;
            default:
                break;
        }

    }

    public void AddPoints(float points)
    {
        Score += points;
        scoreUI.text = string.Format("{0:D4}", (int)Score);
    }

    public void StartGame()
    {
        State = eState.StartGame;
    }

    public void Title()
    {
        State = eState.Title;
    }
}
