using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSpawner : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject buttonPrefab = null;
    [SerializeField] GameObject leftButton = null;
    [SerializeField] GameObject rightButton = null;

    [Space]
    [SerializeField] GameObject[] panelTransform = null;
    [Space]
    [SerializeField] int[] numberOfLevels = null;

    int currentPage = 0;
    bool showArrows = true;
    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        CreateButtons();
        HidePanels();
        if(numberOfLevels.Length == 1) 
        { 
            showArrows = false;
            HideArrows();
        }
    }

    private void Update()
    {
        if(showArrows)
        {
            if (currentPage == 0)
            {
                leftButton.GetComponent<Button>().interactable = false;
                rightButton.GetComponent<Button>().interactable = true;
            }
            else if (currentPage == numberOfLevels.Length - 1)
            {
                leftButton.GetComponent<Button>().interactable = true;
                rightButton.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            return;
        }
    }
    public void CreateButtons()
    {
        for(int i = 0; i < numberOfLevels.Length; i++)
        {
            var tempLevelIndex = 0;
            if (i > 0)
            {
                tempLevelIndex = numberOfLevels[0];
            }
            for (int j = 0; j < numberOfLevels[i]; j++)
            {
                var myButton = Instantiate(buttonPrefab);
            
                var currentLevel = j + tempLevelIndex + 1;
                var currentLevelName = sceneName + "_" + currentLevel;

                myButton.GetComponentInChildren<TextMeshProUGUI>().text = currentLevel.ToString();
                myButton.name = currentLevelName;

                myButton.GetComponent<LevelLoader>().levelIndex = currentLevel;
                myButton.GetComponent<LevelLoader>().levelName = currentLevelName;

                myButton.transform.SetParent(panelTransform[i].transform, false);

            }
        }
    }

    public void HidePanels()
    {
        for(int i = 1; i < numberOfLevels.Length; i++)
        {
            panelTransform[i].SetActive(false);
        }

    }

    public void NavigateLeft()
    {

            leftButton.GetComponent<Button>().interactable = true;
            panelTransform[currentPage].SetActive(false);
            currentPage--;
            panelTransform[currentPage].SetActive(true);
    }

    public void NavigateRight()
    {

            rightButton.GetComponent<Button>().interactable = true;
            panelTransform[currentPage].SetActive(false);
            currentPage++;
            panelTransform[currentPage].SetActive(true);
    }

    private void HideArrows()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }


}
