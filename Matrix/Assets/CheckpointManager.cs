using UnityEngine;
using System.Collections;
using System;

public class CheckpointManager : MonoBehaviour {

    public Checkpoint LastCheckpoint;

    public static CheckpointManager Instance { get; private set; }
    public CheckpointManager()
    {
        if (Instance != null) throw new InvalidOperationException("Only one instance of CheckpointManager can exist");
        Instance = this;
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;
    }
}
