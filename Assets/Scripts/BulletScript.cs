using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour {
	public float speed;
	public int Damage;
	int temp;
	GameObject player,enemy;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newpos = transform.position;
		newpos.y += speed;
		transform.position = newpos;

		if (speed > 0) {
			if(transform.position.y > 5)
				Destroy (gameObject);
		} else {
			if (transform.position.y < -5)
				Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Enemy"){
			if (enemy.gameObject) {
				enemy.GetComponent<enemyScript> ().Hp -= Damage;
				Destroy (gameObject);
			}
		}
		if(other.gameObject.tag=="Player"){
			player.GetComponent<PlayerScript> ().hitPoint -= Damage;
			Destroy(gameObject);
		}
		if(other.tag=="Asteroid"){
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
