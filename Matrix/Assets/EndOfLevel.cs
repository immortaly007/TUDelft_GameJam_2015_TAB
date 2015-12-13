using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {
    public string nextLevel;

    void OnTriggerEnter2D(Collider2D something)
    {
        if (something.gameObject.tag == "Player")
        {
            Application.LoadLevel(nextLevel);
        }
    }
	
}
