using System.Collections;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public int fuelNumber;
    float delayTime = 0.2f;
    private void Start()
    {
        GameEventManager.singleton.OnFuelTriggerEnter += OnFuelDisable;
    }
    private void OnDestroy()
    {   // This is the same thing as "-= OnFuelDisable".Here we don't need to add another method which is like "OnFuelDisable".
        //----------------------------------------------------------------------------
        GameEventManager.singleton.OnFuelTriggerEnter -= (int fuelNumber) =>
        {
            if (fuelNumber == this.fuelNumber)
            {
                gameObject.SetActive(false);
            }
        }; //------------------------------------------
    }
    public void OnFuelDisable(int fuelNumber)
    {
        if (fuelNumber == this.fuelNumber)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEventManager.singleton.FuelTriggerEnter(fuelNumber);
        //      other.gameObject.SetActive(false);
    }

    void Update()
    {
        StartCoroutine(RotateCourotine());
    }
    IEnumerator RotateCourotine()
    {
        transform.Rotate(new Vector3(0, 30 * delayTime, 0));
        yield return new WaitForSeconds(delayTime);
    }
}
