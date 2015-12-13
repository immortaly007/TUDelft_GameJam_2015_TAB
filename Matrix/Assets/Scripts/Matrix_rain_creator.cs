using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Matrix_rain_creator : MonoBehaviour {

    //private float timeAlive;
    private float timePast;
    private Vector3 offset;
    private Vector3 position;
    private int amount;
    private int speed;
    private System.Random rnd = new System.Random();
    public List<Char> symbols = new List<Char>() { '0', '1' };
    public GameObject symbolPrefab;
    public int max_length_of_chain;

	// Use this for initialization
    void Start()
    {
        //timeAlive = UnityEngine.Random.Range(0f, 3f);
        offset = new Vector3 (0.0f,-0.2f,0f);
        position = new Vector3(0, 0, 0);
        max_length_of_chain = Math.Max(max_length_of_chain, 5);
        amount = rnd.Next(5, max_length_of_chain);
        speed = rnd.Next(10, 30);
    }
	
	// Update is called once per frame
	void Update () {

        if (timePast >= 0.1/speed)
        {
            float probability = 1f / (float)symbols.Count;
            var symbolChar = symbols[(int)(rnd.NextDouble() / probability)];
            var symbolGO = (GameObject)Instantiate(symbolPrefab, transform.position + position + offset, Quaternion.identity);
            var textMesh = symbolGO.GetComponent<TextMesh>();
            textMesh.text = symbolChar.ToString();

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
