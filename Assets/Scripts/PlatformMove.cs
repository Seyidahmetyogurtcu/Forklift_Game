using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    Transform trPlatform;
    private int sign = 1;

    void Start()
    {
        trPlatform = GetComponent<Transform>();
        trPlatform.localPosition = new Vector3(Random.Range(-9, 9), trPlatform.localPosition.y, trPlatform.localPosition.z);
    }

    void FixedUpdate()
    {
        while (Mathf.Abs((int)trPlatform.localPosition.x) <= 10)
        {
            trPlatform.localPosition += (sign) * (new Vector3(5f * Time.fixedDeltaTime, 0, 0));
            break;
        }
        if ((int)trPlatform.localPosition.x > 10)
        {
            trPlatform.localPosition = new Vector3(10, 0, 0);
            sign = -sign;
        }
        if ((int)trPlatform.localPosition.x < -10)
        {
            trPlatform.localPosition = new Vector3(-10, 0, 0);
            sign = -sign;
        }
    }
}
