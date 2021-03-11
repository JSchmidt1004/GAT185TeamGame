using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionsScreen;
    public GameObject pauseScreen;
    public Transition transition;

    public AudioMixer audioMixer;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public int highScore = 0;

    bool isPaused = false;
    float timeScale;

    static GameController instance = null;
    public static GameController Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        audioMixer.SetFloat("MasterVolume", masterVolume);
        MasterSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
        audioMixer.SetFloat("MusicVolume", musicVolume);
        MusicSlider.value = musicVolume;

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
        audioMixer.SetFloat("SFXVolume", sfxVolume);
        SFXSlider.value = sfxVolume;
    }

    public void SetHighScore(int score)
    {
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnLoadGameScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(LoadGameScene(sceneName));
    }

    IEnumerator LoadGameScene(string sceneName)
    {
        transition.StartTransition(Color.black, 2);
        transition.StartTransition(Color.clear, 2);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }


    public void OnLoadMenuScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(LoadMenuScene(sceneName));
    }

    IEnumerator LoadMenuScene(string sceneName)
    {
        transition.StartTransition(Color.black, 1);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }


    public void OnTitleScreen()
    {
        titleScreen?.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        optionsScreen?.SetActive(false);
    }

    public void OnOptionsScreen()
    {
        titleScreen?.SetActive(false);
        optionsScreen?.SetActive(true);
    }

    public void OnPauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = timeScale;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }

    public void OnMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }
}
