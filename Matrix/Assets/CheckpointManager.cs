using UnityEngine;
using System.Collections;
using System;

public class CheckpointManager : MonoBehaviour {
    [Serializable]
    public struct CheckPoint
    {
        public Vector3 postion;
        public Quaternion rotation;
        public bool isActive;
    }

    public CheckPoint LastCheckpoint = new CheckPoint();

    public static CheckpointManager Instance { get; private set; }
    public CheckpointManager()
    {
        //if (Instance != null) throw new InvalidOperationException("Only one instance of CheckpointManager can exist");
        Instance = this;
    }

    public void SetCheckpoint(Transform transform)
    {
        LastCheckpoint.postion = transform.position;
        LastCheckpoint.rotation = transform.rotation;
        LastCheckpoint.isActive = true;
    }

    public void RestoreLastCheckpoint(Transform transform)
    {
        transform.position = LastCheckpoint.postion;
        transform.rotation = LastCheckpoint.rotation;
    }
}
