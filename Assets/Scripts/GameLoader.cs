using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingProcessSlider;
    [SerializeField] private Text loadingProcessPercentage;


    public static GameLoader singleton;
    private void Start()
    {
        singleton = this;
    }
    public void Loading(int sceneId)
    {
        StartCoroutine(AsynchronousLoadScene(sceneId));
    }

    IEnumerator AsynchronousLoadScene(int sceneId)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneId);
        loadingPanel.SetActive(true);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); //Loading operation is 90% of AscLoading.
            loadingProcessSlider.value = progress;
            loadingProcessPercentage.text = (progress * 100) + "%";
            yield return null;
        }
    }
}
