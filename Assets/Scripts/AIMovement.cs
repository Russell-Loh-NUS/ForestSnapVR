using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public float moveX = 0;
    public float moveY = 0;
    public float moveZ = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Vector3 moveForward = new Vector3(moveX, moveY, moveZ);
        rb.velocity = moveForward;
	}
}
