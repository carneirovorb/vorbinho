using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {


	public float maxHeight;
	public float minHeight;
	public float rateSpawn;
	private float currentSpawn;

	public GameObject tubePrefab;


	public int maxSpawn;

	public List<GameObject> tubes;

	// Use this for initialization
	void Start () {

		for (int i = 0; i<maxSpawn; i++){
			GameObject temp = Instantiate(tubePrefab) as GameObject;
			tubes.Add(temp);
			temp.SetActive(false);
		}

	}

	// Update is called once per frame
	void Update () {
		

			currentSpawn += Time.deltaTime;
			if (currentSpawn > rateSpawn){
				currentSpawn = 0;
				spawn();
			}


	}


	private void spawn(){
		float randHeight = Random.Range(minHeight, maxHeight);

		GameObject temp = null;

		for (int i = 0; i<maxSpawn; i++){
			if(tubes[i].activeSelf == false){
				temp = tubes[i];
				break;
			}
		}

		if(temp != null){
			temp.transform.position = new Vector3(transform.position.x, randHeight, transform.position.z);
			temp.SetActive(true);
		}
	}
}
