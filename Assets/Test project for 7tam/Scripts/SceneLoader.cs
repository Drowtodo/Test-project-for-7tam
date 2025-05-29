using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private ProgressBar progressBar;

    [SerializeField]
    private TMP_Text percentageText;

    /// <summary>
    /// При помощи корутины асинхронно загружается сцена с заданным индексом
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            progressBar.MoveProgress(operation.progress);

            percentageText.text = operation.progress * 100f + "%";

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
