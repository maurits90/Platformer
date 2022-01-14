using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(2, 10)]
    public float smoothFactor;
    public Vector3 minBound, maxBound;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPos = target.position + offset;

        Vector3 boundPos = new Vector3(
            Mathf.Clamp(targetPos.x, minBound.x, maxBound.x),
            Mathf.Clamp(targetPos.y, minBound.y, maxBound.y),
            Mathf.Clamp(targetPos.z, minBound.z, maxBound.z));
        Vector3 smoothPos = Vector3.Lerp(transform.position, boundPos,smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
