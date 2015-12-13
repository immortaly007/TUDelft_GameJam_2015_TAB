using UnityEngine;
using System.Collections;

public class DigitalRainSymbol : MonoBehaviour {

    public Gradient gradient;
    private float timeAlive;
    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeAlive = 0.0f;
        spriteRenderer.color = gradient.Evaluate(0);
	}
	
	// Update is called once per frame
	void Update () {
        if ( timeAlive <= 3 )
            spriteRenderer.color = gradient.Evaluate( timeAlive / 4 );
        if ( timeAlive > 5)
            Destroy(spriteRenderer);
        if ( timeAlive > 3 )
            spriteRenderer.color = gradient.Evaluate( 3 / 4 );
         
        timeAlive = timeAlive + 1 * Time.deltaTime;
	}
}
