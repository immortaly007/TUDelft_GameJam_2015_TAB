using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DigitalRainSymbol : MonoBehaviour
{

    public Gradient gradient;
    public float averageSymbolSwitchTime = 10f;
    public List<char> symbols = new List<char>() { '0', '1' };
    public float maxTimeAlive = 3f;

    private float timeAlive;
    private TextMesh textMesh;

    // Use this for initialization
    public void Reset()
    {
        textMesh = GetComponent<TextMesh>();
        timeAlive = 0.0f;
        textMesh.color = gradient.Evaluate(0);
        // Randomly select a symbol
        float symbolProbability = 1f / (float)symbols.Count;
        char symbolChar = symbols[(int)(Random.value / symbolProbability)];
        textMesh.text = symbolChar.ToString();
    }

    void Start() { Reset(); }

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
                    int curSymbol = symbols.IndexOf(textMesh.text[0]);
                    if (symbol >= curSymbol) symbol -= 1;
                    if (symbol < 0) symbol = symbols.Count - 1;

                    // Change the symbol
                    textMesh.text = symbols[symbol].ToString();
                }
            }
            textMesh.color = gradient.Evaluate(timeAlive / maxTimeAlive);
        }
        else
            MatrixSymbolPool.instance.Put(this.gameObject);
        timeAlive += Time.deltaTime;
    }
}

