using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix_rain_creator : MonoBehaviour {
    // Single character settings
    public GameObject symbolPrefab;
    public Gradient gradient;
    public float averageSymbolSwitchTime = 10f;
    public List<Sprite> symbols = new List<Sprite>();
    public float maxTimeAlive = 3f;

    // Raindrop settings
    public float minSpeed = 10;
    public float maxSpeed = 30;
    [Range(5, 10000)]
    public int maxChainLength;

    //private float timeAlive;
    private float timePast;
    private Vector3 offset;
    private Vector3 position;
    private int amount;
    private float speed;

	// Use this for initialization
    void Start()
    {
        //timeAlive = UnityEngine.Random.Range(0f, 3f);
        offset = new Vector3(0, -symbolPrefab.transform.localScale.y, 0);
        position = new Vector3(0, 0, 0);
        maxChainLength = Mathf.Max(maxChainLength, 5);
        amount = Random.Range(5, maxChainLength);
        speed = Random.Range(minSpeed, maxSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        if (timePast >= 0.1f/speed)
        {
            var symbolGO = MatrixSymbolPool.instance.Get(transform.position + position);
            var textMesh = symbolGO.GetComponent<TextMesh>();
            //symbolGO.transform.SetParent(transform.parent);
            DigitalRainSymbol symbol = symbolGO.GetComponent<DigitalRainSymbol>();
            symbol.gradient = gradient;
            symbol.averageSymbolSwitchTime = averageSymbolSwitchTime;
            symbol.symbols = symbols;
            symbol.maxTimeAlive = maxTimeAlive;
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
