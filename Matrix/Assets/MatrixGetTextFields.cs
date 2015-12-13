using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatrixGetTextFields : MonoBehaviour {

	public Text M00;
	public Text M01;
	public Text M02;
	public Text M10;
	public Text M11;
	public Text M12;
	public Text M20;
	public Text M21;
	public Text M22;

	public Component[] allChildren;

	// Use this for initialization
	void Start () {
		allChildren = gameObject.GetComponentsInChildren<Text>();

		M00 =  getMyText("M00");
		M01 =  getMyText("M01");
		M02 =  getMyText("M02");
		M10 =  getMyText("M10");
		M11 =  getMyText("M11");
		M12 =  getMyText("M12");
		M20 =  getMyText("M20");
		M21 =  getMyText("M21");
		M22 =  getMyText("M22");

	}

	Text getMyText(string childName) {
		foreach (Text myText in allChildren) {
			if (myText.gameObject.name == childName) {
				return myText;
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
