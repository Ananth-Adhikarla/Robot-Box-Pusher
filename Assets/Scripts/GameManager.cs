using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int classicLevel;
    public int modernLevel;
    public float volume;
    public string currentScene;
    const string CLASSIC = "Classic";
    const string CASUAL = "Casual";
    const string VOLUME = "Volume";

    private void Start()
    {
        //ClearData();
        if(PlayerPrefs.GetInt(CLASSIC) == 0)
        {
            PlayerPrefs.SetInt(CLASSIC, 1);
        }
        if(PlayerPrefs.GetInt(CASUAL) == 0)
        {
            PlayerPrefs.SetInt(CASUAL, 1);
        }
        if (PlayerPrefs.GetFloat(VOLUME) == 0)
        {
            PlayerPrefs.SetFloat(VOLUME, 0.5f);
        }
        
    }

    public void SaveLevel(string name, int levelIndex)
    {
        PlayerPrefs.SetInt(name, levelIndex);
    }
    public int LoadLevel(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    public void SaveVolume(string name, float newVol)
    {
        PlayerPrefs.SetFloat(name, newVol);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt(CLASSIC) == 0)
        {
            PlayerPrefs.SetInt(CLASSIC, 1);
        }
        if (PlayerPrefs.GetInt(CASUAL) == 0)
        {
            PlayerPrefs.SetInt(CASUAL, 1);
        }
    }

}
