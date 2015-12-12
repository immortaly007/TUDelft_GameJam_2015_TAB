using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour {
    class ModifierAndButton
    {
        public MatrixModifier Modifier { get; set; }
        public Button Button { get; set; }

        public ModifierAndButton() { }
        public ModifierAndButton(MatrixModifier modifier, Button button) { Modifier = modifier; Button = button; }
    }
    private List<ModifierAndButton> modifiers = new List<ModifierAndButton>();

    public GameObject buttonPrefab;
    public GameObject buttonPanel;

	public GameObject thePivot;


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



	// Update is called once per frame
	//void Update () {

	//	if (this.animationBusy == false) { // if currently not animating anything
	//		if (Input.GetKeyDown ("space")) {
        
	//			MatrixModifier koe = (new TranslationMatrixModifier(new Vector2(-2,2)));

	//			Debug.Log ("starting animation");
	//			startAnimation (thePivot, koe);

	//			//koe.Apply (thePivot.transform);
	//		}
	//	} else { // animate current thingy
			
	//		doAnimation();

	//	}
	
	//}


    public void AddMatrixModifier(MatrixModifier modifier)
    {
        var buttonGO = Instantiate(buttonPrefab);
        buttonGO.transform.SetParent(buttonPanel.transform);
        var button = buttonGO.GetComponent<Button>();
        modifiers.Add(new ModifierAndButton(modifier, button));
        
    }

    public MatrixModifier GetMatrixModifierForButton(GameObject buttonGO)
    {
        var button = buttonGO.GetComponent<Button>();
        var modifierAndButton = modifiers.FirstOrDefault(m => m.Button == button);
        return modifierAndButton == null ? null : modifierAndButton.Modifier;
    }

    public void Consume(MatrixModifier modifier)
    {
        var modifierAndButton = modifiers.FirstOrDefault(m => m.Modifier == modifier);
        if (modifierAndButton == null) return;
        modifiers.Remove(modifierAndButton);
        Destroy(modifierAndButton.Button);

    }
}
