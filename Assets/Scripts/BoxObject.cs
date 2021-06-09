using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxObject : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundTrigger"))
        {
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restart
            GameLoader.singleton.Loading(SceneManager.GetActiveScene().buildIndex);//restart asynchronously
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//Next Level
            GameLoader.singleton.Loading(SceneManager.GetActiveScene().buildIndex + 1);//Next Level asynchronously
        }
    }
}
