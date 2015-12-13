using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player died
        if (other.gameObject.tag == "DeathCheck" || other.gameObject.tag == "DeathZone")
        {
            InventoryManager.instance.Clear();
            CheckpointManager.Instance.RestoreLastCheckpoint();
        }

        if (other.gameObject.tag == "CheckPoint")
        {
            InventoryManager.instance.Clear();
            CheckpointManager.Instance.SetCheckpoint();
            other.gameObject.SetActive(false);
        }

    }
}
