using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioticField : MonoBehaviour {
	public int healing;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 5);
	}
}
