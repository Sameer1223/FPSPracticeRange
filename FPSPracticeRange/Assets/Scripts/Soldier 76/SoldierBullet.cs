using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBullet : MonoBehaviour {
	public float bulletSpeed = 100f;
	public bool kms = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (transform.right * bulletSpeed * Time.deltaTime);
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy") {
			other.transform.GetComponent<Enemy> ().takeDmg (19);
		} else if (other.gameObject.tag == "Whatever") {
			return;
		}
		Destroy (transform.parent.gameObject);
	}
}
