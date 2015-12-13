using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DigitalRainSymbol : MonoBehaviour
{

    public Gradient gradient;
    public float averageSymbolSwitchTime = 10f;
    public List<Sprite> symbols = new List<Sprite>();
    public float maxTimeAlive = 3f;

    private float timeAlive;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    public void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeAlive = 0.0f;
        spriteRenderer.color = gradient.Evaluate(0);
        // Randomly select a symbol
        float symbolProbability = 1f / (float)symbols.Count;
        var symbolChar = symbols[(int)(Random.value / symbolProbability)];
        spriteRenderer.sprite = symbolChar;
    }

    void Start() {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // With some random change, change the symbol
        if (timeAlive <= maxTimeAlive)
        {
            if (averageSymbolSwitchTime > 0)
            {
                float probability = (Time.deltaTime * 2f) / (averageSymbolSwitchTime);
                if (Random.value < probability)
                {
                    float charProb = 1f / (float)(symbols.Count - 1);
                    int symbol = (int)(Random.value / charProb);
                    // Make sure the current symbol can't be selected again
                    int curSymbol = symbols.IndexOf(spriteRenderer.sprite);
                    if (symbol >= curSymbol) symbol -= 1;
                    if (symbol < 0) symbol = symbols.Count - 1;

                    // Change the symbol
                    spriteRenderer.sprite = symbols[symbol];
                }
            }
            //textMesh.color = gradient.Evaluate(timeAlive / maxTimeAlive);
            spriteRenderer.color = MatrixSymbolPool.instance.EvaluateGradient(timeAlive / maxTimeAlive);
        }
        else
            MatrixSymbolPool.instance.Put(this.gameObject);
        timeAlive += Time.deltaTime;
    }
}

