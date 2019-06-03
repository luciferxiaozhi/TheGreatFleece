using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);

        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
