using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMove : MonoBehaviour
{
    Transform trPendulum, trPivotPendulum;
    private float changeRotationTimer;

    void Start()
    {
        trPendulum = GetComponent<Transform>();
        trPivotPendulum = transform.parent.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        changeRotationTimer += Time.fixedDeltaTime;
        if (changeRotationTimer <= 3)
        {
            trPendulum.RotateAround(trPivotPendulum.position, Vector3.forward, 30f * Time.fixedDeltaTime);
        }
        if (changeRotationTimer >= 3 && changeRotationTimer <= 9)
        {
            trPendulum.RotateAround(trPivotPendulum.position, Vector3.back, 30f * Time.fixedDeltaTime);
        }
        if (changeRotationTimer >= 9 && changeRotationTimer <= 12)
        {
            trPendulum.RotateAround(trPivotPendulum.position, Vector3.forward, 30f * Time.fixedDeltaTime);
        }
        if (changeRotationTimer >=12)
        {
            changeRotationTimer = 0f;
        }
    }
}
