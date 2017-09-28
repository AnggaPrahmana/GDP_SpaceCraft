using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	public int Type,Hp,damage,score;
	public Sprite[] enemySprite;
	public GameObject bullet;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Hp <= 0) {
			if (player.gameObject) {
				player.GetComponent<PlayerScript> ().score += score;
				Destroy (gameObject);
			}
		}
	}

	public void setEnemyType(int t){
		Type = t;
		GetComponent<SpriteRenderer>().sprite = enemySprite[t];

		if (Type == 0) {
			Hp = 100;
			damage = 10;
			score = 100;
		} else if (Type == 1) {
			Hp = 50;
			damage = 25;
			score = 150;
		} else if (Type == 2) {
			Hp = 70;
			damage = 10;
			score = 200;
		}

		StartCoroutine(movement());
		StartCoroutine(Shoot());
	}

	IEnumerator movement(){
		while(true){


			yield return new WaitForSeconds(1f);
		}

	}

	IEnumerator Shoot(){
		while(true){
			GameObject spawnedBullet = (GameObject)Instantiate (bullet);
			spawnedBullet.transform.position = new Vector3 (transform.position.x, transform.position.y-1 ,transform.position.z);
			spawnedBullet.GetComponent<BulletScript>().speed = -0.25f;
			spawnedBullet.GetComponent<BulletScript> ().Damage = damage;
			yield return new WaitForSeconds(1.5f);
		}
	}
}
