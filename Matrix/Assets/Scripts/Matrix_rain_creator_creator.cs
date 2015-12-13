using UnityEngine;
using System.Collections;

public class Matrix_rain_creator_creator : MonoBehaviour {

	// Use this for initialization
    private float timeToNext;
    private float timePast;
    private Vector3 position;
    public GameObject rain_creator;
    private float width;
    private float height;
    //private float length;
    
	void Start () {
        timeToNext =Random.Range(0f, 0.4f);
        timePast = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (timePast >= timeToNext)
        {
            width = this.transform.parent.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            height = this.transform.parent.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            position = new Vector3 (Random.Range(-width,width),Random.Range(-height,height),0);
            Instantiate(rain_creator, transform.position + position, Quaternion.identity);
            timeToNext = Random.Range(0f, 3f) / ( width * 2 );
            timePast = 0;
        }

        else
        {
            timePast += Time.deltaTime;
        }
        
	}
}
