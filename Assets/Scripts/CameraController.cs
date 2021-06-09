using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    private int i = 0;
    private int numberOfCamera = 3;
    private bool isCameraChange;
    private int cameraSwitchNumber;

    void Update()
    {
        CameraChange();
    }   

    void CameraChange()
    { 
        cameraSwitchNumber = UIManager.singleton.cameraSwitchNumber;
        isCameraChange = UIManager.singleton.isCameraChange;
        if (isCameraChange)
        {
            cameras[(cameraSwitchNumber + 0) % numberOfCamera].SetActive(true);
            cameras[(cameraSwitchNumber + 1) % numberOfCamera].SetActive(false);
            cameras[(cameraSwitchNumber + 2) % numberOfCamera].SetActive(false);
        }

    }
}
