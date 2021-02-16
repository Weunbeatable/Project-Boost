using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float MainThrust = 200f;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] AudioClip LevelComplete;
    [SerializeField] AudioClip EngineFailiure;
    int SceneIndex = 0;

    Rigidbody rigidBody;
    AudioSource RocketAudioSource;
    // Start is called before the first frame update

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        RocketAudioSource = GetComponent<AudioSource>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
            // somewhere stop sound on death. 
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
             // kind of like a input file stream check from C++ 

            switch (collision.gameObject.tag)
        {
            case "friendly":
            
                break;

            case "Finish":
                LevelCompleteSequence();
                // parameterise time  
                // invoke means at some time the loadnextscene will be called but during that time another 
                //function can still be called. 
                break;

            default:
                DeathSequence();
                // parameterise the time so there is a delay after you die. 

                break;
        }
    }

   

    private void LevelCompleteSequence()
    {
        state = State.Transcending;
        RocketAudioSource.Stop();
        RocketAudioSource.PlayOneShot(LevelComplete);
        Invoke("LoadNextScene", 1f);
    }

    private void DeathSequence()
    {
        state = State.Dying;
        RocketAudioSource.Stop();
        RocketAudioSource.PlayOneShot(EngineFailiure);
        Invoke("LoadFirstLevel", 1f);
    }

    private  void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo allow for more than 2 levels
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); // loading the first level 
    }
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {// can thrust while rotating
            ApplyThrust();
        }
        else
        {
            RocketAudioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * MainThrust);
        if (!RocketAudioSource.isPlaying) // so we dont have the same audio source layering on top of itself.
        {
            RocketAudioSource.PlayOneShot(MainEngine); // this allows us to deal with multiple audio clips
        }


    }

    private void RespondToRotateInput()
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
