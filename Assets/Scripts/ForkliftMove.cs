using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ForkliftMove : MonoBehaviour
{
    private bool isStoped;
    private float input_x, input_y, temp_y;
    public WheelCollider leftFrontWheelCollider, rightFrontWheelCollider, leftBackWheelCollider, rightBackWheelCollider;
    public Transform leftFrontWheel, rightFrontWheel, leftBackWheel, rightBackWheel;
    public Transform hand;
    public float wheelTorque = 100, breakTorque = 50, maxTurnAngle = 45, handSpeed = 0.5f, maxHandPos = 2, MinHandPos = 0;
    private Rigidbody rb;
    private Vector3 centerOfMass = new Vector3(0, -0.35f, 0);
    public Slider fuelTankFill;
    private float realTimeInSeconds;
    private float timer;


    #region steering wheel
    //private bool wheelBeingHeld = false;
    //public RectTransform wheel;
    //private float wheelAngle = 0;
    //private float lastWheelAngle = 0;
    //private Vector2 center;
    //public float maxSteerAngle = 200f;
    //public float releaseSpeed = 300f;
    //public float output;


    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    wheelBeingHeld = true;
    //    center = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, wheel.position);
    //    lastWheelAngle = Vector2.Angle(Vector2.up, eventData.position - center);
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    float newAngle = Vector2.Angle(Vector2.up, eventData.position - center);
    //    if (eventData.position.x > center.x)
    //    {
    //        wheelAngle += newAngle - lastWheelAngle;
    //    }
    //    else
    //    {
    //        wheelAngle -= newAngle - lastWheelAngle;
    //    }
    //    wheelAngle = Mathf.Clamp(wheelAngle, -maxSteerAngle, maxSteerAngle);
    //    lastWheelAngle = newAngle;
    //}
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    OnDrag(eventData);
    //    wheelBeingHeld = false;
    //}

    //private void Update()
    //{
    //    if (!wheelBeingHeld && wheelAngle != 0f)
    //    {
    //        float deltaAngle = releaseSpeed * Time.deltaTime;
    //        if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
    //        {
    //            wheelAngle = 0;
    //        }
    //        else if (wheelAngle > 0f)
    //        {
    //            wheelAngle -= deltaAngle;
    //        }
    //        else
    //        {
    //            wheelAngle += deltaAngle;
    //        }
    //    }
    //    wheel.localEulerAngles = new Vector3(0, 0, -wheelAngle);
    //    output = wheelAngle / maxSteerAngle;
    //}
    #endregion

    #region Pedal system
    public bool isGasSteppedOn;
    public bool isReverseGasSteppedOn;
    public bool isBrakeSteppedOn;
    public bool isLiftUpSteppedOn;
    public bool isLiftDownSteppedOn;
    #endregion
    public bool inputLiftUp;
    public bool inputLiftDown;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    void FixedUpdate()
    {
        Timer();
        GetUIMovements();
        MoveFortlift();
        turnWheelsMeshes();
        GameOver();
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
    #endregion


    void GetUIMovements()
    {
        //#if !MOBILE_INPUT
        //        isStoped = Input.GetKey(KeyCode.Q);
        //        input_y = Input.GetAxis("Vertical"); 
        //        input_x = Input.GetAxis("Horizontal");
        //        inputLiftUp = Input.GetKey(KeyCode.T);  
        //         inputLiftDown = Input.GetKey(KeyCode.G);
        //#endif

        input_x= SteeringWheelMovement.singleton.output;

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

    void turnWheelsMeshes()
    {
        //get Colliders possition and quaternion adn set Meshs position and Quaternion
        leftFrontWheelCollider.GetWorldPose(out Vector3 pos1, out Quaternion quat1);
        leftFrontWheel.position = pos1;
        leftFrontWheel.rotation = quat1;

        rightFrontWheelCollider.GetWorldPose(out Vector3 pos2, out Quaternion quat2);
        rightFrontWheel.position = pos2;
        rightFrontWheel.rotation = quat2;

        leftBackWheelCollider.GetWorldPose(out Vector3 pos3, out Quaternion quat3);
        leftBackWheel.position = pos3;
        leftBackWheel.rotation = quat3;

        rightBackWheelCollider.GetWorldPose(out Vector3 pos4, out Quaternion quat4);
        rightBackWheel.position = pos4;
        rightBackWheel.rotation = quat4;
    }

    void Timer()
    {
        //Time.timeSinceLevelLoad;
        realTimeInSeconds = Time.realtimeSinceStartup;
        timer += Time.fixedDeltaTime;
        if (timer >= 0.2)
        {
            fuelTankFill.value -= 0.003f;
            timer = 0f;
        }

         Debug.Log((int)timer);
    }

    void MoveFortlift()
    {
        //move each whells
        leftFrontWheelCollider.motorTorque = input_y * wheelTorque;
        rightFrontWheelCollider.motorTorque = input_y * wheelTorque;
        leftBackWheelCollider.motorTorque = input_y * wheelTorque;
        rightBackWheelCollider.motorTorque = input_y * wheelTorque;

        //stop
        if (isStoped)
        {
            breakTorque = 50f;
        }
        else if (!isStoped)
        {
            breakTorque = 0f;
        }
        leftFrontWheelCollider.brakeTorque = breakTorque;
        rightFrontWheelCollider.brakeTorque = breakTorque;
        leftBackWheelCollider.brakeTorque = breakTorque;
        rightBackWheelCollider.brakeTorque = breakTorque;

        //turn back_wheels
        leftBackWheelCollider.steerAngle = maxTurnAngle * input_x;
        rightBackWheelCollider.steerAngle = maxTurnAngle * input_x;

        //lifters vertical move 
        temp_y = hand.localPosition.y;

        if (inputLiftUp && ! inputLiftDown)
        {
            temp_y += handSpeed * Time.fixedDeltaTime; //"temp_y" meter per second
            temp_y = Mathf.Clamp(temp_y, MinHandPos, maxHandPos);
            hand.localPosition = new Vector3(hand.localPosition.x, temp_y, hand.localPosition.z); //add back to clamped temp_y
        }
        else if (!inputLiftUp && inputLiftDown)
        {
            temp_y -= handSpeed * Time.fixedDeltaTime;
            temp_y = Mathf.Clamp(temp_y, MinHandPos, maxHandPos); //"temp_y" meter per second
            hand.localPosition = new Vector3(hand.localPosition.x, temp_y, hand.localPosition.z);//add back to clamped temp_y
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            fuelTankFill.value += 0.25f;
        }
    }
    void GameOver()
    {
        if (fuelTankFill.value == 0f || transform.position.y < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restart
        }
        if (Mathf.Abs(transform.rotation.z) >= 85 && Mathf.Abs(transform.rotation.x) >= 85)
        {
            transform.rotation= Quaternion.identity;
        }
    }
}
