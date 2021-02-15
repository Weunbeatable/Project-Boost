using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float MainThrust = 200f;

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "friendly":
                //do nothing
                print("OK");
                break;

            case "Fuel":
                print("Fuel");
                break;

            default:
                print("Dead");

            break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))// can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * MainThrust);
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

        
        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            
            transform.Rotate(Vector3.back * rotationSpeed);
        }

        rigidBody.freezeRotation = false; // resume control of rigidbody. 
    }

   
}
