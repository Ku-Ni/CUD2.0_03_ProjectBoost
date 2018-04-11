using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    private enum RotationAxis {
        X,Y,Z
    }
    private enum RotationDirection {
        POSITIVE, NEGATIVE
    }
    

    [SerializeField]
    private RotationAxis rotationAxis;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    [Range(-1, 1)]
    private int rotationDirection;
    	
	// Update is called once per frame
	void Update () {
        float rotationFactor = rotationDirection * rotationSpeed * Time.deltaTime;

        switch (rotationAxis) {
            case RotationAxis.X:
                transform.Rotate(new Vector3(rotationFactor, 0, 0));
                break;
            case RotationAxis.Y:
                transform.Rotate(new Vector3(0, rotationFactor, 0));
                break;
            case RotationAxis.Z:
                transform.Rotate(new Vector3(0, 0, rotationFactor));
                break;
        }
        

    }
}
