using UnityEngine;
using System;
using System.Collections;

public class Matrix_rain_creator : MonoBehaviour {

    //private float timeAlive;
    private float timePast;
    private Vector3 offset;
    private Vector3 position;
    private int amount;
    private int speed;
    private System.Random rnd = new System.Random();
    public GameObject meshRenderer_0;
    public GameObject meshRenderer_1;
    public int max_length_of_chain;

	// Use this for initialization
    void Start()
    {
        //timeAlive = UnityEngine.Random.Range(0f, 3f);
        offset = new Vector3 (0.0f,-0.2f,0f);
        position = new Vector3(0f, 0f, 0f);
        max_length_of_chain = Math.Max(max_length_of_chain, 5);
        amount = rnd.Next(5, max_length_of_chain);
        speed = rnd.Next(10, 30);
    }
	
	// Update is called once per frame
	void Update () {

        if (timePast >= 0.1/speed)
        {
            if (rnd.NextDouble() > 0.5)
                Instantiate(meshRenderer_0, transform.position + position + offset, Quaternion.identity);
            else
                Instantiate(meshRenderer_1, transform.position + position + offset, Quaternion.identity);

            position += offset;

            if (amount > 0)
            {
                amount -= 1;
                timePast = 0;
            }
            else
                Destroy(gameObject);
        }
        else
            timePast += Time.deltaTime;

	}
}
