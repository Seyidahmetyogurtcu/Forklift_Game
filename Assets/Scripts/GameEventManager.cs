using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public event Action<int> OnFuelTriggerEnter;
    
    public static GameEventManager singleton;
    private void Awake()
    {
        singleton = this;
    }

    public void FuelTriggerEnter(int fuelNumber)
    {
        OnFuelTriggerEnter?.Invoke(fuelNumber);
        #region Optional Same Code
        //if (OnFuelTriggerEnter!=null)
        //{
        //    OnFuelTriggerEnter(fuelNumber);
        //}
        #endregion
    }
}
