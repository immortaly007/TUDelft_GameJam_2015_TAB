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
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            AddMatrixModifier(new RotationMatrixModifier(90));
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
