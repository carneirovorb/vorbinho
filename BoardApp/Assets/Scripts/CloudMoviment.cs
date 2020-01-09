using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoviment : MonoBehaviour {

	public float velocidade;
	private float randonVel;


	// Use this for initialization
	void Start () {

		//gameController = FindObjectOfType(typeof(GameController)) as GameController;
		float minHeight = velocidade;
		float maxHeight = (float)(velocidade*3);
		randonVel = Random.Range(minHeight, maxHeight);
	}

	// Update is called once per frame
	void Update () {

		transform.position += new Vector3(randonVel,0,0)* Time.deltaTime;

			if(transform.position.x <-10){
				gameObject.SetActive(false);
			}


	}
}
