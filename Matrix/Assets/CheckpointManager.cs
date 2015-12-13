using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CheckpointManager : MonoBehaviour {
    [Serializable]
    public struct ObjectState
    {
        public GameObject gameObject;
        public Vector3 postion;
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
        var gos = GameObject.FindGameObjectsWithTag("StoreOnCheckpoint");

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
            postion = go.transform.position,
            rotation = go.transform.rotation
        };
        LastCheckpoint.Add(state);

        // Find the children and store their states
        for (int i = 0; i < go.transform.GetChildCount(); i++)
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
        go.SetActive(state.isActive);
        go.transform.position = state.postion;
        go.transform.rotation = state.rotation;
    }
}
