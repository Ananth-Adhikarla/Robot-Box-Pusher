using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Canvas optionsCanvas = null;
    [SerializeField] Canvas levelFinishCanvas = null;
    [SerializeField] Canvas tutorialCanvas = null;
    [SerializeField] GameObject finishVFX = null;
    [SerializeField] Slider mySlider = null;
    const string VOLUME = "Volume";
    public float volume;
    GameManager gm;
    const string MAX_CLASSIC_LEVEL = "Classic_80";
    const string MAX_CASUAL_LEVEL = "Casual_40";

    private void Start()
    {
        mySlider.value = PlayerPrefs.GetFloat(VOLUME);
        levelFinishCanvas.gameObject.SetActive(false); // backup;

    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void LoadClassicMenu()
    {
        SceneManager.LoadScene("Classic");
    }

    public void LoadChallengeMenu()
    {
        SceneManager.LoadScene("Casual");
    }

    public void OpenOptions()
    {
        optionsCanvas.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsCanvas.gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        gm = FindObjectOfType<GameManager>();
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().name == MAX_CLASSIC_LEVEL || SceneManager.GetActiveScene().name == MAX_CASUAL_LEVEL)
        {
            LevelSelect();
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelFinish()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var gameType = sceneName.Split('_');
        var name = gameType[0];
        var index = int.Parse( gameType[1] );

        gm = FindObjectOfType<GameManager>();
        gm.SaveLevel(name, index+1);

        Instantiate(finishVFX, transform.position, transform.rotation);
        levelFinishCanvas.gameObject.SetActive(true);
    }

    public void MusicChange()
    {
        FindObjectOfType<MusicPlayer>().MusicChanger();
    }

    public void VolumeChange()
    {
        mySlider = mySlider.GetComponent<Slider>();
        gm = FindObjectOfType<GameManager>();
        gm.SaveVolume(VOLUME, mySlider.value);
    }

    public void OpenTutorialCanvas()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }
    public void CloseTutorialCanvas()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }
}
