using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

    [SerializeField]
    private float thrustSpeed = 1000f;
    [SerializeField]
    private float rotationSpeed = 100f;

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
            rigidBody.AddRelativeForce(Vector3.up * (thrustSpeed * Time.deltaTime));

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
                transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            }
            else if (Input.GetKey(left)) {
                Debug.Log("Rotate left");
                transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
            }

        }
    }

    void OnCollisionEnter(Collision collision) {
        Tags collisionTag = (Tags) Enum.Parse(typeof(Tags), collision.gameObject.tag.ToUpper());

        switch (collisionTag) {
            // TODO complete
            case Tags.FINISH:
                Debug.Log("Level completed!");
                break;
            case Tags.FRIENDLY:
                Debug.Log("Friendly item, Do nothing");
                break;
            default:
                Debug.Log("You crashed!" + "\n" +
                    "Blood...blood...blood...and death.");
                break;
        }
    }
}
