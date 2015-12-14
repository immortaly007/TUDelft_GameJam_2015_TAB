using UnityEngine;
using System.Collections;

public class ScreenShakeManager : MonoBehaviour {
    public GameObject mainCamera;

    private float shake = 0;
    private float shakeAmount = 0.7f;

    public static ScreenShakeManager instance;
    public ScreenShakeManager() { instance = this; }

//    var shake : float = 0;
//var shakeAmount : float = 0.7;
//var decreaseFactor : float = 1.0;
 
//function Update()
//    {
//        if (shake > 0)
//        {
//            Camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
//            shake -= Time.deltaTime * decreaseFactor;

//        }
//        else
//        {
//            shake = 0.0;
//        }
//    }
    public void ScreenShake(float time, float intensity)
    {
        shake = time; shakeAmount = intensity;
    }

    void Update()
    {
        if (shake > 0)
        {
            mainCamera.transform.localPosition += Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime;

        }
        else
        {
            shake = 0;
        }
    }

}
