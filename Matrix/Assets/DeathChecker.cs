using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player died
        if (other.gameObject.tag == "DeathCheck")
            CheckpointManager.Instance.RestoreLastCheckpoint(transform);

        if (other.gameObject.tag == "CheckPoint")
            CheckpointManager.Instance.SetCheckpoint(transform);

    }
}
