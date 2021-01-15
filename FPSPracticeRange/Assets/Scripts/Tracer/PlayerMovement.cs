using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float playerSpeed = 10f;
	private Rigidbody rb;
	public Vector3 reset;
	public float timer;
	public float bulletTimer;
	public float JumpSpeed = 100.0f;
	public float BlinkDist = 7f;
	private bool ASD;
	public int Blink = 3;
	private float Recharge = 3;
	public int RechargeLim = 3;
	public int Health = 150;
	public int Ammo = 40;
	public GameObject bulletPrefab;
	public GameObject barrel1, barrel2;


	void Awake(){
		Ammo = 40;
		Health = 150;
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Recharge = RechargeLim;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");
		timer += Time.deltaTime;
		bulletTimer += Time.deltaTime;

		if (Mathf.Abs (h) > 0.1f || Mathf.Abs (v) > 0.1f) {
			transform.Translate (new Vector3 (playerSpeed * v * Time.deltaTime, 0f, playerSpeed * h * Time.deltaTime));
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			rb.AddForce (Vector3.up * JumpSpeed);
		}
		if (transform.position.y < 0){
			transform.position = reset;
		}
		if (Input.GetMouseButton(0) && Ammo > 0) { 

			if (bulletTimer >= 0.1f) {
				GameObject bulletGO1 = Instantiate (bulletPrefab, barrel1.transform.position, barrel1.transform.rotation);
				GameObject bulletGO2 = Instantiate (bulletPrefab, barrel2.transform.position, barrel2.transform.rotation);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					bulletGO1.transform.LookAt (hit.point);
					bulletGO2.transform.LookAt (hit.point);
				}
				bulletTimer = 0;
				Ammo -= 2;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Ammo = 40;
		}


		ASD = false;
		if(Blink <= 2){
			Recharge = Recharge - Time.deltaTime;
			if(Recharge <= 0){
				Blink++;
				Recharge = RechargeLim;
			}
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
			ASD = true;
		}
		if(Blink >= 1){
			if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)){
				transform.Translate(-Vector3.forward * BlinkDist);
				Blink--;
			}
			if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)){
				transform.Translate(-Vector3.right * BlinkDist);
				Blink--;
			}
			if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A)){
				transform.Translate(Vector3.forward * BlinkDist);
				Blink--;
			}
			if(Input.GetKeyDown(KeyCode.LeftShift)){
				if(ASD == false){
					transform.Translate(Vector3.right * BlinkDist);
					Blink--;
				}
			}
		}
	}
}

		
			