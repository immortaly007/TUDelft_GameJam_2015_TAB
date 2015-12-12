using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryManager : MonoBehaviour {

    private List<MatrixModifier> modifiers = new List<MatrixModifier>();
    private List<Button> buttons = new List<Button>();

    public GameObject buttonPrefab;
    public GameObject buttonPanel;

	public GameObject thePivot;

	private bool animationBusy = false;
	private float  animationTimestep = 0;
	private float  animationLength = 2;
	private Vector3 animationOldPosition;
	private Vector3 animationOldLocalScale;
	private Quaternion animationOldLocalRotation;

	private GameObject animationTarget;
	private MatrixModifier animationModifier;


    // Singleton stuff
    public static InventoryManager instance;
    public InventoryManager()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        AddMatrixModifier(new RotationMatrixModifier(90));
        AddMatrixModifier(new TranslationMatrixModifier(new Vector2(-2, 0)));
	}

	void startAnimation(GameObject target, MatrixModifier animation, Transform oldTransform) {
		animationTarget = target;

		animationModifier = animation;
		animationOldPosition = oldTransform.localPosition;
		animationOldLocalScale = oldTransform.localScale;
		animationOldLocalRotation = oldTransform.localRotation;
		animationBusy = true;
		animationTimestep = 0.0f;
	}

	void doAnimation() {
		MatrixModifier temp = animationModifier.Clone(); // get the animation
		float ratio = animationTimestep/animationLength;

		Debug.Log("ratio " + ratio);

		if (ratio > 1) {
			ratio = 1;
			animationBusy = false;
		}
		temp.Tween(ratio);

		animationTarget.transform.localPosition = animationOldPosition;
		animationTarget.transform.localScale = animationOldLocalScale;
		animationTarget.transform.localRotation = animationOldLocalRotation;

		temp.Apply (animationTarget.transform);
		animationTimestep += Time.deltaTime;
		if (animationBusy == false) {
			Debug.Log ("animation done");

			if (animationModifier is TranslationMatrixModifier) {
				Vector3 movement = animationTarget.transform.localPosition - animationOldPosition;
				animationTarget.transform.localPosition = animationOldPosition;

				Transform[] ts = animationTarget.GetComponentsInChildren<Transform>();

				foreach (Transform t in ts) {
					if (t == animationTarget.transform)
						continue;
					t.localPosition += movement;
				}

			}

			animationModifier = null;
			animationTarget = null;


		}
	}

	// Update is called once per frame
	void Update () {

		if (this.animationBusy == false) { // if currently not animating anything
			if (Input.GetKeyDown ("space")) {
        
				MatrixModifier koe = (new TranslationMatrixModifier(new Vector2(-2,2)));

				Debug.Log ("starting animation");
				startAnimation (thePivot, koe, thePivot.transform);

				//koe.Apply (thePivot.transform);
			}
		} else { // animate current thingy
			
			doAnimation();

		}
	
	}

    void UpdateButtonLayout()
    {
        // Delete the old buttons
        foreach (var button in buttons)
            Destroy(button.gameObject);
        buttons.Clear();

        // Create the new buttons
        for (int i = 0; i < modifiers.Count; i++)
        {
            var buttonGO = Instantiate(buttonPrefab);
            buttonGO.transform.SetParent(buttonPanel.transform);
            var button = buttonGO.GetComponent<Button>();
            buttons.Add(button);
            var buttonIndex = buttonGO.GetComponent<IndexCache>();
            buttonIndex.listIndex = i;
        }
    }

    public void AddMatrixModifier(MatrixModifier modifier)
    {
        modifiers.Add(modifier);
        UpdateButtonLayout();
    }
}
