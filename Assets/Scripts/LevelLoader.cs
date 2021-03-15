using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public int levelIndex;
    public string levelName;
    public string gameType;
    public bool isClickable = true;
    Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
        DisableButtons();

    }
    public void OnClick()
    {
        //print("Loading " + name);
        SceneManager.LoadScene(name);
    }

    public void DisableButtons()
    {
        var number = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);

        var tempIndex = this.name.Split('_');

        if(int.Parse( tempIndex[1] ) <= number)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }

    }


}
