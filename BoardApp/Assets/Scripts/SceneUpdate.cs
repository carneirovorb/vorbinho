using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("date time" System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
		Debug.Log(PlayerPrefs.GetString("date time"));
	}

}
