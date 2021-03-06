﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CheckpointManager : MonoBehaviour {
    [Serializable]
    public struct ObjectState
    {
        public GameObject gameObject;
        public Vector3 position;
		public Vector3 scale;
        public Quaternion rotation;
        public bool isActive;
    }

    public List<ObjectState> LastCheckpoint = new List<ObjectState>();

    public static CheckpointManager Instance { get; private set; }
    public CheckpointManager()
    {
        //if (Instance != null) throw new InvalidOperationException("Only one instance of CheckpointManager can exist");
        Instance = this;
    }

    void Start()
    {
        SetCheckpoint();
    }

    public void SetCheckpoint()
    {
        LastCheckpoint.Clear();
        var gos = FindObjectsOfType<GameObjectFlags>()
            .Where(f => (f.flags & GameObjectFlags.MatrixFlags.StoreOnCheckpoint) == GameObjectFlags.MatrixFlags.StoreOnCheckpoint)
            .Select(f => f.gameObject);

        foreach(var go in gos)
        {
            StoreState(go);
        }
    }

    /// <summary>
    /// Recursively calls "StoreState" on objects to make sure they are restored correctly to their prior state
    /// </summary>
    /// <param name="go"></param>
    private void StoreState(GameObject go)
    {
        // Store the state of the current object
        ObjectState state = new ObjectState()
        {
            gameObject = go,
            isActive = go.activeSelf,
			position = go.transform.localPosition,
			scale = go.transform.localScale,
			rotation = go.transform.localRotation
        };
        LastCheckpoint.Add(state);

        // Find the children and store their states
        for (int i = 0; i < go.transform.childCount; i++)
            StoreState(go.transform.GetChild(i).gameObject);
    }

    public void RestoreLastCheckpoint()
    {
        foreach(var state in LastCheckpoint)
        {
            RestoreState(state);
        }
    }

    private void RestoreState(ObjectState state)
    {
        GameObject go = state.gameObject;
        // If the gameobject has been destroyed, we can't restore it
        if (go == null)
        {
            Debug.LogWarning("Some GameObject can't be restored because it has been destroyed.");
            return;
        }

        // Otherwise, we can :)
        go.SetActive(state.isActive);
		go.transform.localPosition = state.position;
		go.transform.localScale = state.scale;
		go.transform.localRotation = state.rotation;
    }
}
