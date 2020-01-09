using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using System.Threading;
using System;

public class trashContent : MonoBehaviour {

	public Animator anim;

	private bool abre;
	public float timeTemp;
	private float currentTime =0;
	public bool limpar = false;





	void Start () {
	
		GetComponent<PressGesture>().StateChanged += HandleStateChanged;


	}






	void HandleStateChanged (object sender, GestureStateChangeEventArgs e)
	{
		
		if(e.State == Gesture.GestureState.Ended){


			//btOn.SetActive(true);
			//btOff.SetActive(false);
			//bt.overrideSprite
			//currentTime = 0;
			abre = true;
			limpar = true;
			StartCoroutine(finalizar());

		}
	}

	IEnumerator finalizar() {

		yield return new WaitForSeconds(0.01f);
		limpar = false;

	}
	
	// Update is called once per frame
	void Update () {



		anim.SetBool("abre", abre);

		currentTime += Time.deltaTime;
		if(currentTime>=timeTemp){
			abre=false;
			anim.SetBool("fecha", true);

		}



	}
}
