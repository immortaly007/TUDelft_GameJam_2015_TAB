using UnityEngine;
using System.Collections;

public class PickUpMatrix : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public bool rotation;
	public bool translation;
	public bool scale;
	public float angle;
	public float dx;
	public float dy;
	public float sx;
	public float sy;

	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") return;

		MatrixModifier myMatrix = null;
		if (rotation) {
			myMatrix = new RotationMatrixModifier (angle);
		}
		if (translation) {
			myMatrix = new TranslationMatrixModifier (new Vector2(dx,dy));
		}
		if (scale) {
			myMatrix = new ScalingMatrixModifier(new Vector2(sx,sy));
		}
		InventoryManager.instance.AddMatrixModifier (myMatrix);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
