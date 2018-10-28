using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float expiryTimer = 8.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        expiryTimer -= Time.deltaTime;
        if (expiryTimer <= 0.0f)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
