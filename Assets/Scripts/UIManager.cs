using UnityEngine;



public class UIManager : MonoBehaviour
{
    public static UIManager singleton;
    #region Pedal system
    public bool isGasSteppedOn;
    public bool isReverseGasSteppedOn;
    public bool isBrakeSteppedOn;
    public bool isLiftUpSteppedOn;
    public bool isLiftDownSteppedOn;
    public bool isCameraChange;
    public int cameraSwitchNumber=0;

    #endregion

    #region Inputs
    public  bool isStoped;
    public  float input_x, input_y, temp_y;
    public  bool inputLiftUp;
    public  bool inputLiftDown;
    #endregion
    private void Awake()
    {
        singleton = this;
    }

    #region EventTrigger 
    //Gaz Pedal
    public void GazPedalDown()
    {
        isGasSteppedOn = true;
    }
    public void GazPedalExit()
    {
        isGasSteppedOn = false;
    }
    public void GazPedalEnter()
    {
        isGasSteppedOn = true;
    }
    public void GazPedalUp()
    {
        isGasSteppedOn = false;
    }

    //Reverse Pedal
    public void ReversePedalDown()
    {
        isReverseGasSteppedOn = true;
    }
    public void ReversePedalExit()
    {
        isReverseGasSteppedOn = false;
    }
    public void ReversePedalEnter()
    {
        isReverseGasSteppedOn = true;
    }
    public void ReversePedalUp()
    {
        isReverseGasSteppedOn = false;
    }

    //Brake
    public void BrakeDown()
    {
        isBrakeSteppedOn = true;
    }
    public void BrakeExit()
    {
        isBrakeSteppedOn = false;
    }
    public void BrakeEnter()
    {
        isBrakeSteppedOn = true;
    }
    public void BrakeUp()
    {
        isBrakeSteppedOn = false;
    }

    //LiftUp
    public void LiftUpDown()
    {
        isLiftUpSteppedOn = true;
    }
    public void LiftUpExit()
    {
        isLiftUpSteppedOn = false;
    }
    public void LiftUpEnter()
    {
        isLiftUpSteppedOn = true;
    }
    public void LiftUpUp()
    {
        isLiftUpSteppedOn = false;
    }

    //LiftDown
    public void LiftDownDown()
    {
        isLiftDownSteppedOn = true;
    }
    public void LiftDownExit()
    {
        isLiftDownSteppedOn = false;
    }
    public void LiftDownEnter()
    {
        isLiftDownSteppedOn = true;
    }
    public void LiftDownUp()
    {
        isLiftDownSteppedOn = false;
    }
    //ChangeCamera
    public void ChangeCameraClick()
    {
        cameraSwitchNumber += 1;
    }
    public void ChangeCameraDown()
    {
        isCameraChange = true;
    }
    public void ChangeCameraUp()
    {
        isCameraChange = false;
    }

    #endregion

    private void Update()
    {
        GetUIMovements();
    }

    void GetUIMovements()
    {
        input_x = -SteeringWheelMovement.singleton.output; // We get rotational input from whell on UI 

        inputLiftUp = isLiftUpSteppedOn;
        inputLiftDown = isLiftDownSteppedOn;
        if (isGasSteppedOn)
        {
            input_y = 1.0f;
        }
        else if (isReverseGasSteppedOn)
        {
            input_y = -1.0f;
        }
        else if (!isReverseGasSteppedOn && !isGasSteppedOn)
        {
            input_y = 0.0f;
        }
        isStoped = isBrakeSteppedOn ? true : false;
        // input_y = !isReverseGasSteppedOn && !isGasSteppedOn ? 0 : input_y;
        // input_y = isGasSteppedOn ? 1f : input_y;
        // input_y = isReverseGasSteppedOn ? -1f : input_y;
    }
}

