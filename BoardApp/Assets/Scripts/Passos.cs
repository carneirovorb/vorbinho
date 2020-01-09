using UnityEngine;
using System.Collections;

public class Passos : MonoBehaviour {

	private string[] passosTrilha = new string[12];
	private string[] passosFunc = new string[4];
	private int elementoInt;

	public void passos(string elemento, string comando, string local){

		Debug.Log(elemento+" "+comando+" "+local);

		int.TryParse(elemento , out elementoInt);


		if(local=="trilha"){
			passosTrilha[elementoInt-1] = comando;
		}else if(local=="funcao"){
			passosFunc[elementoInt-1] = comando;
		}

	}


}
