using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundTrigger"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restart
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//restart
        }
    }
}
