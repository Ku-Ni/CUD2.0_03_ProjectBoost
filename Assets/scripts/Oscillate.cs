using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillate : MonoBehaviour {

    [SerializeField]
    private Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField]
    private float movementPeriod = 2f;
    [SerializeField]
    private MovementDirection movementDirection = MovementDirection.BOTH;

    private Vector3 startingPosition;
    private static float tau = Mathf.PI * 2;

    // Use this for initialization
    void Start () {
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (movementPeriod <= Mathf.Epsilon) {
            return;
        }

        // amount of complete cycles completed
        float cycles = Time.time / movementPeriod;

        float rawSinWave = Mathf.Sin(cycles * tau);

        float movementFactor = 0;
        switch (movementDirection) {
            case MovementDirection.POSITIVE:
                // 0 < movement factor < 1
                movementFactor = rawSinWave / 2f + 0.5f;
                break;
            case MovementDirection.NEGATIVE:
                // -1 < movement factor < 0
                movementFactor = rawSinWave / 2f - 0.5f;
                break;
            case MovementDirection.BOTH:
                // -1 < movement factor < 1
                movementFactor = rawSinWave;
                break;
            default:
                throw new UnityException("MovementDirection not recognised: " + movementDirection);
        }

        Vector3 displacement = movementVector * movementFactor;
        transform.position = startingPosition + displacement;
	}
}
