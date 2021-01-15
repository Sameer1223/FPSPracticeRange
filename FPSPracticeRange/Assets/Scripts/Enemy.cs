using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int enemyHealth = 250;
	public float enemySpeed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void takeDmg(int damage){
		Debug.Log ("I'm hit!");
		enemyHealth -= damage;
		if (enemyHealth <= 0) {
			Destroy (gameObject);
		}
	}
}
