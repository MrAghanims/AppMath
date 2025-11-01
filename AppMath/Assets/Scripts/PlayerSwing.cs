using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    public float swingAngle = 90f;     // how far to swing
    public float swingSpeed = 5f;      // how fast to swing
    private Quaternion startRotation;  // original rotation
    private Quaternion swingRotation;  // target swing rotation
    private bool swinging = false;     // prevent multiple swings

    void Start()
    {
        startRotation = transform.localRotation;
        swingRotation = Quaternion.Euler(0, swingAngle, 0) * startRotation; // adjust axis as needed
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        swinging = true;

        // Forward swing
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Slerp(startRotation, swingRotation, t);
            yield return null;
        }

        // Return swing
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Slerp(swingRotation, startRotation, t);
            yield return null;
        }

        swinging = false;
    }
}