using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject SettingsMenu;

    public Slider sfxVolumeSlider;
    public Slider MusicVolumeSlider;

    public AudioSource music;

    bool isInSettings;

    public float SFXVolumeMultiplier;
    public float MusicVolumeMultiplier;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        SFXVolumeMultiplier = sfxVolumeSlider.value;
    }

    void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in audioSources)
        {
            if (source.name == "music")
            {
                music = source;
            }
        }
    }

    public void ChangeMusicVolume()
    {
        MusicVolumeMultiplier = MusicVolumeSlider.value;
        music.volume = MusicVolumeMultiplier;

    }
    public void openSettings()
    {
        if (!isInSettings)
        {
            Time.timeScale = 1;
            SettingsMenu.SetActive(true);
            Time.timeScale = 0;

            isInSettings = true;
        }
        else
        {
            Time.timeScale = 1;
            SettingsMenu.SetActive(false);
            Time.timeScale = 0;

            isInSettings = false;
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }
}
