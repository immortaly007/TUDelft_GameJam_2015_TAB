using UnityEngine;
using System.Collections;

public class MatrixPlatform : MonoBehaviour {
    private GameObject m_pivot;

    public GameObject Pivot
    {
        get { return m_pivot; }
    }

	// Use this for initialization
	void Start () {
        m_pivot = GetPivot();
        // Register ourselves to the platform manager
        MatrixPlatformManager.instance.Register(this);
    }

    private GameObject GetPivot()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            if (gameObject.transform.GetChild(i).name == "Pivot")
                return gameObject.transform.GetChild(i).gameObject;
        throw new System.Exception("Pivot for matrix plaform not found");
    }
}
