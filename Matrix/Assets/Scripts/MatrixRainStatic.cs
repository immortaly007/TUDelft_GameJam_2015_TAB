using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatrixRainStatic : MonoBehaviour {
    // Single character settings
    public float averageSymbolSwitchTime = 5f;
    public List<Sprite> symbols = new List<Sprite>();
    public float maxTimeAlive = 3f;

    // Spawner settings
    public float spawnSpeed; // spawnspeed in symbols per second

    private float timePast;
    private Collider2D boundingCollider;

    void Start()
    {
        boundingCollider = GetComponent<Collider2D>();
    }

	// Update is called once per frame
	void Update () {
        float expectedSymbols = Time.deltaTime * spawnSpeed * boundingCollider.bounds.size.x * boundingCollider.bounds.size.y;
        if (expectedSymbols > 1f || Random.value < expectedSymbols)
        {
            int symbolCount = (int)expectedSymbols;
            if (symbolCount == 0) symbolCount = 1;

            for (int i = 0; i < symbolCount; i++)
            {
                // Pick a random position within the collider space
                Vector3 randOffset = new Vector3(Random.Range(0f, boundingCollider.bounds.size.x), Random.Range(0f, boundingCollider.bounds.size.y), 0f);
                Vector3 possiblePosition = boundingCollider.bounds.min + randOffset;
                if (boundingCollider.OverlapPoint(possiblePosition))
                {
                    var symbolGO = MatrixSymbolPool.instance.Get(possiblePosition);
                    var textMesh = symbolGO.GetComponent<TextMesh>();
                    //symbolGO.transform.SetParent(transform);
                    DigitalRainSymbol symbol = symbolGO.GetComponent<DigitalRainSymbol>();
                    symbol.averageSymbolSwitchTime = averageSymbolSwitchTime;
                    symbol.symbols = symbols;
                    symbol.maxTimeAlive = maxTimeAlive;
                }
            }
        }
        else
            timePast += Time.deltaTime;
    }
}
