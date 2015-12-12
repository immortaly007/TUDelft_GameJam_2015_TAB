using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MatrixPlatformManager : MonoBehaviour {
    public Camera mainCam;
    public List<GameObject> matrixPlatforms = new List<GameObject>();
    public Canvas mainCanvas;

    public GameObject dropperPrefab;

    private class PlatformCatcherPair
    {
        public GameObject Platform { get; set; }
        public GameObject Catcher { get; set; }

        public PlatformCatcherPair() { }
        public PlatformCatcherPair(GameObject platform, GameObject catcher) { Platform = platform; Catcher = catcher; }
    }


    private List<PlatformCatcherPair> platformCatchers = new List<PlatformCatcherPair>();

    public static MatrixPlatformManager instance;
    public MatrixPlatformManager()
    {
        if (instance != null) throw new InvalidOperationException("There can be only one MatrixPlatformManager");
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Remove catchers that are no longer needed
        for(int i = 0; i < platformCatchers.Count; i++)
        {
            var platformCatcher = platformCatchers[i];
            if (!OnScreen(platformCatcher.Platform))
            {
                Destroy(platformCatcher.Catcher);
                platformCatchers.RemoveAt(i);
                i--;
            }
        }

        // Find out if we need any new catchers
        foreach(var platform in matrixPlatforms)
        {
            if (OnScreen(platform) && !HasCatcherFor(platform))
            {
                var dropperGO = Instantiate(dropperPrefab);
                dropperGO.transform.SetParent(mainCanvas.transform);
                platformCatchers.Add(new PlatformCatcherPair(platform, dropperGO));
                // TODO: Add an event listener to the platform to tell is of it has caught something.
            }
        }

        foreach(var platformCatcher in platformCatchers)
        {
            Vector3 pos = GetScreenPos(platformCatcher.Platform);
            var catcherTransform = platformCatcher.Catcher.GetComponent<RectTransform>();
            catcherTransform.position = new Vector3(pos.x, pos.y, 0);
        }
	}

    Vector3 GetScreenPos(GameObject go)
    {
        return mainCam.WorldToScreenPoint(go.transform.position);
    }

    bool OnScreen(GameObject go)
    {
        Vector3 pos = GetScreenPos(go);
        return pos.x < Screen.width && pos.x > 0 && pos.y < Screen.height && pos.y > 0;
    }

    /// <summary>
    /// Returns true if there exists a catcher for this platform
    /// </summary>
    /// <param name="platform"></param>
    /// <returns></returns>
    bool HasCatcherFor(GameObject platform)
    {
        return platformCatchers.Any(p => p.Platform == platform);
    }

    /// <summary>
    /// This method gets called when a something is dropped on a catcher
    /// </summary>
    /// <param name="catcher"></param>
    public void Catch(GameObject catcher, GameObject caughtObject)
    {
        // Find out which platform the catcher belongs to
        var platform = platformCatchers.FirstOrDefault(p => p.Catcher == catcher);
        if (platform == null) return; // Can't find the platform
        Debug.Log("Something was dropped on me");
        platform.Platform.SetActive(false);
        // TODO: tell the inventory manager this object has been used
        
    }
}
