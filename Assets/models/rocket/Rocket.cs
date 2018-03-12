using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;

    private readonly KeyCode thrust = KeyCode.Space;
    private readonly KeyCode left = KeyCode.LeftArrow;
    private readonly KeyCode right = KeyCode.RightArrow;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
    }


    private void HandleMovement() {


        if (Input.GetKey(thrust)) {
            rigidBody.AddRelativeForce(Vector3.up);
        }

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
