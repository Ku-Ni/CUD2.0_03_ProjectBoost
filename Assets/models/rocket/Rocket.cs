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
    [SerializeField]
    private AudioClip thrustAudio;
    [SerializeField]
    private AudioClip explosionAudio;
    [SerializeField]
    private ParticleSystem thrustParticle;
    [SerializeField]
    private ParticleSystem crashParticle;
    [SerializeField]
    private ParticleSystem successParticle;


    private bool isActive = true;
    private Rigidbody rigidBody;
    private GameManager gameManager;
    private AudioSource audioSource;


    private readonly KeyCode thrust = KeyCode.Space;
    private readonly KeyCode left = KeyCode.LeftArrow;
    private readonly KeyCode right = KeyCode.RightArrow;

    // Use this for initialization
    void Start() {
        Initialize();

    }

    private void Initialize() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        if (!gameManager)
            throw new UnityException("GameManager not found!");
        if (!audioSource)
            throw new UnityException("AudioSource not found!");
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            ProcessThrust();
            ProcessRotate();
        }
    }

    private void ProcessThrust() {
        if (Input.GetKey(thrust)) {
            rigidBody.AddRelativeForce(Vector3.up * (thrustSpeed * Time.deltaTime));
            if (audioSource.isPlaying == false) {
                audioSource.PlayOneShot(thrustAudio);
                thrustParticle.Play();
            }
        }
        else {
            audioSource.Stop();
            thrustParticle.Stop();
        }
    }

    private void ProcessRotate() {
        if (Input.GetKey(left) ^ Input.GetKey(right)) {

            if (Input.GetKey(right)) {
                transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            }
            else if (Input.GetKey(left)) {
                transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
            }

        }
    }

    void OnCollisionEnter(Collision collision) {
        if (!isActive) {return;}

        Tags collisionTag = (Tags) Enum.Parse(typeof(Tags), collision.gameObject.tag.ToUpper());

        switch (collisionTag) {
            case Tags.FINISH:
                CompleteLevel();
                break;
            case Tags.FRIENDLY:
                // Friendly item, Do nothing
                break;
            case Tags.UNTAGGED:
                Crash();
                break;
            default:
                throw new UnityException("Tag "+ collisionTag+" not handled in collision code");
        }
    }

    private void CompleteLevel() {
        Debug.Log("Level completed!");
        isActive = false;
        successParticle.Play();
        gameManager.CompleteCurrentLevel();
    }

    private void Crash() {
        Debug.Log("Blood...blood...blood...and death." + "\n" + "You crashed! Are you alive? No, no you are not");
        isActive = false;
        rigidBody.freezeRotation = false;
        audioSource.Stop();
        thrustParticle.Stop();
        audioSource.PlayOneShot(explosionAudio);
        crashParticle.Play();
        gameManager.Invoke("Crash", (explosionAudio.length-1f));
    }
    
}
