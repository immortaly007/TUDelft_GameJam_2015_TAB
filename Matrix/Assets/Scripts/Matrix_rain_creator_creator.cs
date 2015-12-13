using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix_rain_creator_creator : MonoBehaviour {

    // Single character settings
    public Gradient gradient;
    public float averageSymbolSwitchTime = 10f;
    public List<char> symbols = new List<char>() { '0', '1' };
    public float maxTimeAlive = 3f;

    // Raindrop settings
    public GameObject rainCreatorPrefab;
    public float minSpeed = 10;
    public float maxSpeed = 30;
    [Range(5, 10000)]
    public int maxChainLength;

    // Rain settings
    public float averageTimeBetweenDrops = 1.5f;

    private float timeToNext;
    private float timePast;
    private Vector3 position;
    private float width;
    private float height;
    
	void Start () {
        timeToNext = Random.Range(0f, 0.4f);
        timePast = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (timePast >= timeToNext)
        {
            width = this.transform.parent.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            height = this.transform.parent.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            position = new Vector3 (Random.Range(-width,width),Random.Range(-height,height),0);
            var raindropGO = (GameObject)Instantiate(rainCreatorPrefab, transform.position + position, Quaternion.identity);
            // Make new nodes our children
            raindropGO.transform.SetParent(transform);

            var raindrop = raindropGO.GetComponent<Matrix_rain_creator>();
            // Copy the settings
            raindrop.gradient = gradient;
            raindrop.averageSymbolSwitchTime = averageSymbolSwitchTime;
            raindrop.symbols = symbols;
            raindrop.maxTimeAlive = maxTimeAlive;
            raindrop.minSpeed = minSpeed;
            raindrop.maxSpeed = maxSpeed;
            raindrop.maxChainLength = maxChainLength;

            timeToNext = Random.Range(0f, averageTimeBetweenDrops * 2) / ( width * 2 );
            timePast = 0;
        }

        else
        {
            timePast += Time.deltaTime;
        }
        
	}
}
