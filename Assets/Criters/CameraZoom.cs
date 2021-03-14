using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    public float ZoomSpeed;
    public Vector3 initialPosition;
    
	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var axis = Input.GetAxis("Mouse ScrollWheel");
        //if(axis!=0f)
        //    print("axis " + axis);
        if (axis>0f)
        {
            transform.position += Vector3.forward * ZoomSpeed;
        }
        if (axis < 0f)
        {
            transform.position -= Vector3.forward * ZoomSpeed;
        }
    }
}
