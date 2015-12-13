using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatrixSymbolPool : MonoBehaviour {
    public GameObject matrixSymbolPrefab;
    public int initialPoolSize = 1000;

    private Queue<GameObject> matrixSymbolPool = new Queue<GameObject>();

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
        symbol.transform.parent.SetParent(transform);
        symbol.transform.localPosition = Vector3.zero;
        matrixSymbolPool.Enqueue(symbol);
    }
}
