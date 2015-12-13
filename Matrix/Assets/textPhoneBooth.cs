using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textPhoneBooth : MonoBehaviour {


	public Text textDozer;

	private string textString;
	private int character=0;

	void Start(){
		textString = textDozer.text;
	}

	void OnTriggerEnter2D(Collider2D other){
		character = 0;
	}

	void OnTriggerStay2D(Collider2D other){	
		if (character < textString.Length) {
			character += 1;
		}

		//Debug.Log ("Text is : "+textDozer.text);
		//Debug.Log ("textDozer.ToString().Length : "+textDozer.ToString().Length);
		//Debug.Log ("textString : " + textString);
		textDozer.text = textString.Substring (0, character);
		textDozer.gameObject.SetActive (true);
	}

	void OnTriggerExit2D(Collider2D other){
		//Debug.Log ("Exit the trigger");
		textDozer.gameObject.SetActive (false);
	}
}
