using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatrixRainCreator : MonoBehaviour {
    // Single character settings
    public float averageSymbolSwitchTime = 10f;
    public List<Sprite> symbols = new List<Sprite>();
    public float maxTimeAlive = 3f;

    // Raindrop settings
    public GameObject rainCreatorPrefab;
    public float minSpeed = 10;
    public float maxSpeed = 30;
    [Range(5, 10000)]
    public int maxChainLength;

    // Rain settings
    public float averageTimeBetweenDrops = 0.4f;

    private float timeToNext;
    private float timePast;
    private Vector3 position;
    private float width;
    private float height;
    private Collider2D boundsCollider;

    void Start()
    {
        boundsCollider = GetComponent<Collider2D>();
        timeToNext = Random.Range(0f, 0.4f);
        timePast = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (timePast >= timeToNext)
        {
            width = boundsCollider.bounds.size.x / 2;
            height = boundsCollider.bounds.size.y / 2;

            // Try at most 20 times to find a position within the bounds
            for (int i = 0; i < 20; i++)
            {
                position = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
                position += transform.position;
                if (boundsCollider.OverlapPoint(position))
                    break;
            }

            var raindropGO = (GameObject)Instantiate(rainCreatorPrefab, position, Quaternion.identity);
            // Make new nodes our children
            raindropGO.transform.SetParent(transform);

            var raindrop = raindropGO.GetComponent<Matrix_rain_creator>();
            // Copy the settings
            raindrop.averageSymbolSwitchTime = averageSymbolSwitchTime;
            raindrop.symbols = symbols;
            raindrop.maxTimeAlive = maxTimeAlive;
            raindrop.minSpeed = minSpeed;
            raindrop.maxSpeed = maxSpeed;
            raindrop.maxChainLength = maxChainLength;

            timeToNext = Random.Range(0f, averageTimeBetweenDrops * 2) / (width * 2);
            timePast = 0;
        }

        else
        {
            timePast += Time.deltaTime;
        }

    }
}
