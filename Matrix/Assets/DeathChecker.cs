using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player died
        if (other.gameObject.tag == "DeathCheck" || other.gameObject.tag == "DeathZone")
            CheckpointManager.Instance.RestoreLastCheckpoint();

        if (other.gameObject.tag == "CheckPoint")
            CheckpointManager.Instance.SetCheckpoint();

    }
}
