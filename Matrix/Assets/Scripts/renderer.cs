using UnityEngine;
using System.Collections;

public class renderer : MonoBehaviour {

    public Gradient gradient;
    private float timeAlive;
    private TextMesh textMesh;

    // Use this for initialization
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        timeAlive = 0.0f;
        textMesh.color = gradient.Evaluate(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive <= 3)
            textMesh.color = gradient.Evaluate(timeAlive / 4f);
        if (timeAlive > 5)
            Destroy(gameObject);
        if (timeAlive > 3)
            textMesh.color = gradient.Evaluate( 3f / 4f);

        timeAlive = timeAlive + 1 * Time.deltaTime;
    }
}
