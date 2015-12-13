using UnityEngine;
using System.Collections;
using System;

public class CheckpointManager : MonoBehaviour {

    public struct CheckPoint
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }

    public CheckPoint LastCheckpoint = new CheckPoint();

    public static CheckpointManager Instance { get; private set; }
    public CheckpointManager()
    {
        if (Instance != null) throw new InvalidOperationException("Only one instance of CheckpointManager can exist");
        Instance = this;
    }

    public void SetCheckpoint(Transform transform)
    {
        LastCheckpoint.Position = transform.position;
        LastCheckpoint.Rotation = transform.rotation;
    }

    public void RestoreLastCheckpoint(Transform transform)
    {
        transform.position = LastCheckpoint.Position;

    }
}
