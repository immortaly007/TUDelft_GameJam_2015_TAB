using UnityEngine;
using System.Collections;

public class RainSizeFixer : MonoBehaviour {
    public float characterHeight = 0.3f;
    void Update()
    {
        var matrixRain = GetComponentInChildren<MatrixRainCreator>();
        matrixRain.maxChainLength = (int)(transform.localScale.y / characterHeight);
    }
}
