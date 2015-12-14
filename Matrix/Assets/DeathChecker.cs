using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour {
    public float deathPressButtonTime = 0.5f;
    private float deathPressedTime = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player died
        if (other.gameObject.tag == "DeathCheck" || other.gameObject.tag == "DeathZone")
            Die();

        if (other.gameObject.tag == "CheckPoint")
            Checkpoint(other.gameObject);
       ;
    }

    void Checkpoint(GameObject checkpoint)
    {
        InventoryManager.instance.Clear();
        CheckpointManager.Instance.SetCheckpoint();
        checkpoint.SetActive(false);
    }

    void Die()
    {
        ScreenShakeManager.instance.ScreenShake(0.3f, 0.7f);
        InventoryManager.instance.Clear();
        CheckpointManager.Instance.RestoreLastCheckpoint();
    }

    void Update()
    {
        if (Input.GetButton("Restart")) deathPressedTime += Time.deltaTime;
        else deathPressedTime = 0;

        if (deathPressedTime > deathPressButtonTime)
            Die();
    }
}
