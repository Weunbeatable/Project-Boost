using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
   
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);

    //todo remove from inspector later
    [Range(0, 1)] //  the modifying movement factor
    [SerializeField]
    float movementFactor; //0 for not moved, 1 for fully moved. 
    [SerializeField] float period = 2f;
    Vector3 StartingPos;
    void Start()
    {
        StartingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period; // time.time is framerate Independent 

        const float tau = Mathf.PI * 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinwave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = StartingPos + offset;
    }
}
