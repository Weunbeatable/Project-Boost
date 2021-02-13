using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource RocketAudioScourse;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        RocketAudioScourse = GetComponent<AudioSource>();
     
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))// can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!RocketAudioScourse.isPlaying) // so we dont have the same audio source layering on top of itself.
            {
                RocketAudioScourse.Play();
            }
            else
            {
                RocketAudioScourse.Stop();
            }

        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.back);
        }

        rigidBody.freezeRotation = false; // resume control of rigidbody. 
    }

   
}
