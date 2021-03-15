using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] Text loadText = null;
    [SerializeField] Text loadDisplayText = null;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return new WaitForSeconds(2f);
        loadText.gameObject.SetActive(true);
        loadDisplayText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
 
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadDisplayText.text = progress * 100f + " %" ;
            yield return null;
        }

    }
}
