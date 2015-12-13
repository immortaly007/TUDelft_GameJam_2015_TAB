using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatrixSymbolPool : MonoBehaviour {
    public GameObject matrixSymbolPrefab;
    public Gradient gradient;
    public int initialPoolSize = 1000;

    private Queue<GameObject> matrixSymbolPool = new Queue<GameObject>();
    private Color[] gradientCache = new Color[255];

    public static MatrixSymbolPool instance;
    
    public MatrixSymbolPool()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Create();
        }

        // Create the gradient cache
        for (int i = 0; i < gradientCache.Length; i++)
        {
            float time = (float)i / (float)gradientCache.Length;
            gradientCache[i] = gradient.Evaluate(time);
        }
    }

    public Color EvaluateGradient(float t)
    {
        return gradientCache[(int)(t * (float)gradientCache.Length)];
    }

    private void Create()
    {
        var res = Instantiate(matrixSymbolPrefab);
        res.transform.parent = transform;
        Put(res);
    }

    public GameObject Get(Vector3 position)
    {
        if (matrixSymbolPool.Count == 0)
            Create();

        var symbol = matrixSymbolPool.Dequeue();
        symbol.transform.position = position;
        symbol.GetComponent<DigitalRainSymbol>().Reset();
        //symbol.SetActive(true);
        return symbol;
    }

    public void Put(GameObject symbol)
    {
        //symbol.SetActive(false);
        symbol.transform.SetParent(transform);
        symbol.transform.localPosition = Vector3.zero;
        matrixSymbolPool.Enqueue(symbol);
    }
}
