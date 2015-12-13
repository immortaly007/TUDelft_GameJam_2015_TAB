using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player died
        if (other.gameObject.tag == "DeathZone")
            CheckpointManager.Instance.RestoreLastCheckpoint(transform);

        if (other.gameObject.tag == "CheckPoint")
            CheckpointManager.Instance.SetCheckpoint(transform);

    }
}
