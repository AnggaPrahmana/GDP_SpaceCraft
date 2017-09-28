using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public GameObject bullet;
	int bulletDmg;
	public gameManager gM;
	public Transform topLeftRay, topRightRay, botleftray, botRightRay, topMidRay;
	public Text cot;
	public string[] bacot;
	//string[] bacot= new string[]{"besak ilmu raden santang ni","geser dulu","eaa dak keno","eeits","eea lewat","mampus cok!","ngolai aji alem bae","wak wak buto bae pacak","tembes coi"};
	public bool IsAutoPilot, shoot;
	public float AsteroidDistance;
	int trigger;

	float limitX,limitY;

	Vector3 initPos;

	public int hitPoint, score;
	// Use this for initialization
	void Start () {
		hitPoint = 100;
		bulletDmg = 25;
		score = 0;
		IsAutoPilot = false;
		shoot = true;
		trigger = 0;
		limitX = 8.10f;
		limitY = 4.10f;

		transform.position = new Vector3 (0, -4, 0);
		//bullet = GameObject.Find ("bullet");	
		StartCoroutine(timeToShootBaby());
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject) {
			if (!IsAutoPilot) {
				PlayerMovement ();
			}
			if (IsAutoPilot) {
				autoPilot ();
			}
			PlayerAbility ();

			if (hitPoint <= 0) {
				cot.text = "OUCH.. MOKAT DAH";
				Destroy (gameObject);
			}
		}
	}

	void PlayerMovement(){
		if (Input.GetKey (KeyCode.LeftArrow)&&transform.position.x >= -limitX) {
			Vector3 newpos = transform.position;
			newpos.x = newpos.x - 0.1f;
			transform.position = newpos;
		}
		if (Input.GetKey (KeyCode.RightArrow)&&transform.position.x <= limitX) {
			Vector3 newpos = transform.position;
			newpos.x = newpos.x + 0.1f;
			transform.position = newpos;
		}
		if (Input.GetKey (KeyCode.UpArrow)&&transform.position.y <= limitY) {
			Vector3 newpos = transform.position;
			newpos.y = newpos.y + 0.1f;
			transform.position = newpos;
		}
		if (Input.GetKey (KeyCode.DownArrow)&&transform.position.y >= -limitY) {
			Vector3 newpos = transform.position;
			newpos.y = newpos.y - 0.1f;
			transform.position = newpos;
		}
	}

	void PlayerAbility(){
		if (Input.GetKeyDown (KeyCode.Space)) {
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			IsAutoPilot = !IsAutoPilot;
			cot.text = "auto pilot triggered!";
		}
	}

	void autoPilot(){
		RaycastHit hit;
		Ray frontMidRay = new Ray(transform.position,topMidRay.position);
		Ray frontLeftRay = new Ray(transform.position,topLeftRay.position);
		Ray frontRightRay = new Ray(transform.position,topRightRay.position);
		Ray backLeftRay = new Ray(transform.position, botleftray.position);
		Ray backRightRay = new Ray(transform.position, botRightRay.position);

		if(Physics.Raycast(frontMidRay,out hit,10f)){
			if(hit.collider.tag=="Asteroid"){
				if(transform.position.x >0){
					trigger = 1;
					cot.text = bacot[Random.Range(1,9)];
				}
				if(transform.position.x <0){
					trigger = 2;
					cot.text = bacot[Random.Range(1,9)];
				}
			}
		}
		if(Physics.Raycast(frontLeftRay,out hit, 10f)){
			if(hit.collider.tag=="Asteroid"){
				trigger=2;
				cot.text = bacot[Random.Range(1,9)];
			}
		}
		if(Physics.Raycast(frontRightRay,out hit, 10f)){
			if(hit.collider.tag=="Asteroid"){
				trigger=1;
				cot.text = bacot[Random.Range(1,9)];
			}
		}
		if(transform.position.x >= 7){
			trigger=1;
			cot.text = bacot[Random.Range(1,9)];
		}
		if(transform.position.x <= -7){
			trigger=2;
			cot.text = bacot[Random.Range(1,9)];
		}

		if(trigger == 1){//left
			Vector3 newpos = transform.position;
			newpos.x = newpos.x - 0.1f;
			transform.position = newpos;
		}
		if(trigger == 2){//right
			Vector3 newpos = transform.position;
			newpos.x = newpos.x + 0.1f;
			transform.position = newpos;
		}
		if(trigger == 3){//up
			Vector3 newpos = transform.position;
			newpos.y = newpos.y + 0.1f;
			transform.position = newpos;
		}
		if(trigger == 4){//down
			Vector3 newpos = transform.position;
			newpos.y = newpos.y - 0.1f;
			transform.position = newpos;
		}
		else{
			//do nothing
		}
	}

	IEnumerator timeToShootBaby(){
		while(shoot){
			GameObject spawnedBullet = (GameObject)Instantiate (bullet);
			spawnedBullet.transform.position = new Vector3 (transform.position.x, transform.position.y+1 ,transform.position.z);
			spawnedBullet.GetComponent<BulletScript>().speed = 0.25f;
			spawnedBullet.GetComponent<BulletScript> ().Damage = bulletDmg;
			yield return new WaitForSeconds(0.25f);
		}
	}

	void OnDrawGizmos(){
		//Ray frontMidRay = new Ray(transform.position,topMidRay.position*9f);
		//Ray frontLeftRay = new Ray(transform.position,topLeftRay.position);
		//Ray frontRightRay = new Ray(transform.position,topRightRay.position);
		//Ray backLeftRay = new Ray(transform.position, botleftray.position);
		//Ray backRightRay = new Ray(transform.position, botRightRay.position);

		Gizmos.DrawRay(transform.position,topMidRay.position);
		Gizmos.DrawRay(transform.position,topLeftRay.position);
		Gizmos.DrawRay(transform.position,topRightRay.position);
		Gizmos.DrawRay(transform.position,botleftray.position);
		Gizmos.DrawRay(transform.position,botRightRay.position);
	}
}
