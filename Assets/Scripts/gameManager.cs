using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
	public GameObject asteroid, player, enemy;
	public Text playerHp, playerScore;
	Transform temp;
	Vector3 randomPos;
	// Use this for initialization
	void Start () {
		StartCoroutine (spawnerAsteroid ());
		StartCoroutine(spawnerEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameObject) {
			if (temp) {
				player.GetComponent<PlayerScript> ().AsteroidDistance = Vector3.Distance (temp.position, player.transform.position);
			}
			playerHp.text = player.GetComponent<PlayerScript> ().hitPoint.ToString ();
			playerScore.text = player.GetComponent<PlayerScript> ().score.ToString ();
		}
	}

	IEnumerator spawnerAsteroid(){
		while (true) {
			GameObject spawnedAsteroid = (GameObject)Instantiate (asteroid);
			randomPos = new Vector3 (Random.Range (-7.5f, 7.5f), 6, 0);
			spawnedAsteroid.transform.position = randomPos;
			temp = spawnedAsteroid.transform;
			spawnedAsteroid.GetComponent<SpriteRenderer> ().sprite = 
				spawnedAsteroid.GetComponent<asteroidScript> ().arrayObs[Random.Range (0, 2)];
			spawnedAsteroid.GetComponent<asteroidScript> ().speed = Random.Range (0.1f, 0.2f);
			yield return new WaitForSeconds (1.5f);
		}
	}
	IEnumerator spawnerEnemy(){
		while (true){
			GameObject spawnerEnemy = (GameObject)Instantiate(enemy);
			randomPos = new Vector3 (Random.Range (-7.5f, 7.5f), 4	, 0);
			spawnerEnemy.transform.position = randomPos;
			int type = Random.Range(0,3);
			spawnerEnemy.GetComponent<enemyScript> ().setEnemyType(type);
			yield return new WaitForSeconds(3f);
		}
	}
}
