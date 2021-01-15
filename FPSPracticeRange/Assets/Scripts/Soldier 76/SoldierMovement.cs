using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour {
	public float playerSpeed = 10f;
	private Rigidbody rb;
	public Vector3 reset;
	public float timer;
	public float bulletTimer;
	public float bioTimer;
	public float JumpSpeed = 250f;
	public int Health = 200;
	public int Ammo = 25;
	public GameObject bulletPrefab;
	public GameObject barrel1;
	public GameObject bioPrefab;
	public GameObject weapon;
	public bool sprint = false;
	public bool fire = true;
	public Camera camera;


	void Awake(){
		Ammo = 25;
		Health = 200;
		bioTimer = 13;
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		timer += Time.deltaTime;
		bulletTimer += Time.deltaTime;
		bioTimer += Time.deltaTime;

		if (Mathf.Abs (h) > 0.1f || Mathf.Abs (v) > 0.1f) {
			transform.Translate (new Vector3 (playerSpeed * v * Time.deltaTime, 0f, playerSpeed * h * Time.deltaTime));
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (Vector3.up * JumpSpeed);
		}
		if (transform.position.y < 0) {
			transform.position = reset;
		}

		if (sprint == false) {
			fire = true;
			playerSpeed = 10f;
			weapon.transform.localEulerAngles = (new Vector3 (0, -90, 0));
		} else {
			fire = false;
			playerSpeed = 20f;
			weapon.transform.localEulerAngles = (new Vector3 (0, 180, -45));
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) && sprint == false) {
			sprint = true;
		} else if (Input.GetKeyDown (KeyCode.LeftShift) && sprint == true) {
			sprint = false;
		}
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
//		if (Physics.Raycast (ray, out hit, 100)) {
//			Debug.DrawLine (ray.origin, hit.point);
//		}	
		if (Input.GetMouseButton (0) && Ammo > 0) { 
			if (fire == false) {
				sprint = false;
				fire = true;
			}
			if (bulletTimer >= 0.13f) {
				GameObject bulletGO1 = Instantiate (bulletPrefab, barrel1.transform.position, barrel1.transform.rotation);
				if (Physics.Raycast (ray, out hit, 100)) {
					bulletGO1.transform.LookAt (hit.point);
				}
				bulletTimer = 0;
				Ammo -= 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Ammo = 25;
		}
		if (Input.GetKeyDown(KeyCode.E) && bioTimer > 13) {
			Vector3 loc = new Vector3(transform.position.x, 0.01f, transform.position.z);
			Instantiate (bioPrefab, loc, Quaternion.identity);
			bioTimer = 0;
		}
	}		

	Coroutine heal;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "BioticField"){
			BioticField biotic = other.transform.GetComponent<BioticField>();
			heal = StartCoroutine (Heal (biotic.healing));
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "BioticField") {
			StopCoroutine (heal);
			heal = null;
		}
	}

	public IEnumerator Heal(int health)
	{
		while (true) {
			Health += health;
			yield return null;
		}
	}
}


