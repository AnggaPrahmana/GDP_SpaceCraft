using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {
	public Sprite[] arrayObs;
	public float speed;
	GameObject player,gM;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		gM = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y - speed, transform.position.z);
		if (transform.position.y < -6) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag=="Player"){
			player.GetComponent<PlayerScript> ().hitPoint -= 25;
			Destroy (gameObject);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		if (player.gameObject!=null) {
			Gizmos.DrawLine (transform.position, player.transform.position);
		}
	}
}
