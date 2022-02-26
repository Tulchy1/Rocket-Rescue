using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon){return;}
        // obstacle movement
        const float tau = Mathf.PI * 2;
        float cycles = Time.time / period; // growing over time
        float sinWave = Mathf.Sin(cycles * tau); // -1 to 1

        movementFactor = (sinWave + 1f)/2f; // goes from 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
