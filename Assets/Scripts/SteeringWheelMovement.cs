
using UnityEngine;
using UnityEngine.EventSystems;
public class SteeringWheelMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
   public static SteeringWheelMovement singleton;
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            singleton = null;
        }
    }

    private bool wheelBeingHeld = false;
    public RectTransform wheel;
    public float wheelAngle = 0;
    private float lastWheelAngle = 0;
    private Vector2 center;
    public float maxSteerAngle = 200f;
    public float releaseSpeed = 400f;
    public float output;

    public void OnPointerDown(PointerEventData eventData)
    {
        wheelBeingHeld = true;
        center = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, wheel.position);
        lastWheelAngle = Vector2.Angle(Vector2.up, eventData.position - center);
    }
    public void OnDrag(PointerEventData eventData)
    {
        float newAngle = Vector2.Angle(Vector2.up, eventData.position - center);
        if (eventData.position.x > center.x)
        {
            wheelAngle += newAngle - lastWheelAngle;
        }
        else
        {
            wheelAngle -= newAngle - lastWheelAngle;
        }
        wheelAngle = Mathf.Clamp(wheelAngle, -maxSteerAngle, maxSteerAngle);
        lastWheelAngle = newAngle;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnDrag(eventData);
        wheelBeingHeld = false;
    }

    private void Update()
    {
        TurnTheSteeringWheel();
    }

    void TurnTheSteeringWheel()  
    {
        if (!wheelBeingHeld && wheelAngle != 0f)  //TurnTheSteeringWheel initial poition
        {
            float deltaAngle = releaseSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
            {
                wheelAngle = 0;
            }
            else if (wheelAngle > 0f)
            {
                wheelAngle -= deltaAngle;
            }
            else
            {
                wheelAngle += deltaAngle;
            }
        }
        wheel.localEulerAngles = new Vector3(0, 0, -wheelAngle); 
        output = wheelAngle / maxSteerAngle;  //gives 0-1 range
    }
}
