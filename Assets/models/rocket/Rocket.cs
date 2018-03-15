using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource thrustSound;

    private readonly KeyCode thrust = KeyCode.Space;
    private readonly KeyCode left = KeyCode.LeftArrow;
    private readonly KeyCode right = KeyCode.RightArrow;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        thrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotate();
    }

    private void ProcessThrust() {
        if (Input.GetKey(thrust)) {
            rigidBody.AddRelativeForce(Vector3.up);

            if (thrustSound.isPlaying == false) {
                thrustSound.Play();
            }
        }
        else {
            thrustSound.Stop();
        }
    }

    private void ProcessRotate() {
        if (Input.GetKey(left) ^ Input.GetKey(right)) {
            
            if (Input.GetKey(right)) {
                Debug.Log("Rotate right");
                transform.Rotate(Vector3.forward);
            }
            else if (Input.GetKey(left)) {
                Debug.Log("Rotate left");
                transform.Rotate(Vector3.back);
            }
            
        }
    }
}
