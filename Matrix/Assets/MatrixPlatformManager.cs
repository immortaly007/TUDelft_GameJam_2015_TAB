using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MatrixPlatformManager : MonoBehaviour {
    public Camera mainCam;
    public List<MatrixPlatform> matrixPlatforms = new List<MatrixPlatform>();
    public Canvas mainCanvas;

    public GameObject dropperPrefab;

    // Animation information
    private bool animationBusy = false;
    private float animationTimestep = 0;
    private float animationLength = 0.5f;
    private Vector3 animationOldPosition;
    private Vector3 animationOldLocalScale;
    private Quaternion animationOldLocalRotation;

    private GameObject animationTarget;
    private MatrixModifier animationModifier;

    private class PlatformCatcherPair
    {
        public MatrixPlatform Platform { get; set; }
        public GameObject Catcher { get; set; }

        public PlatformCatcherPair() { }
        public PlatformCatcherPair(MatrixPlatform platform, GameObject catcher) { Platform = platform; Catcher = catcher; }
    }


    private List<PlatformCatcherPair> platformCatchers = new List<PlatformCatcherPair>();

    public static MatrixPlatformManager instance;
    public MatrixPlatformManager()
    {
        //if (instance != null) throw new InvalidOperationException("There can be only one MatrixPlatformManager");
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
            if (!OnScreen(platformCatcher.Platform.Pivot))
            {
                Destroy(platformCatcher.Catcher);
                platformCatchers.RemoveAt(i);
                i--;
            }
        }

        // Find out if we need any new catchers
        foreach(var platform in matrixPlatforms)
        {
            if (OnScreen(platform.Pivot) && !HasCatcherFor(platform))
            {
                var dropperGO = Instantiate(dropperPrefab);
                dropperGO.transform.SetParent(mainCanvas.transform);
                platformCatchers.Add(new PlatformCatcherPair(platform, dropperGO));
                // TODO: Add an event listener to the platform to tell is of it has caught something.
            }
        }

        // Update the catcher positions
        foreach(var platformCatcher in platformCatchers)
        {
            Vector3 pos = GetScreenPos(platformCatcher.Platform.Pivot);
            var catcherTransform = platformCatcher.Catcher.GetComponent<RectTransform>();
            catcherTransform.position = new Vector3(pos.x, pos.y, 0);
        }

        // animate current thingy
        if (animationBusy) DoAnimation();

    }

    void StartAnimation(GameObject target, MatrixModifier animation)
    {
        animationTarget = target;

        animationModifier = animation;
        animationOldPosition = target.transform.localPosition;
        animationOldLocalScale = target.transform.localScale;
        animationOldLocalRotation = target.transform.localRotation;
        animationBusy = true;
        animationTimestep = 0.0f;
    }

    void DoAnimation()
    {
        MatrixModifier temp = animationModifier.Clone(); // get the animation
        float ratio = animationTimestep / animationLength;

        Debug.Log("ratio " + ratio);

        if (ratio > 1)
        {
            ratio = 1;
            animationBusy = false;
        }
        temp.Tween(ratio);

        animationTarget.transform.localPosition = animationOldPosition;
        animationTarget.transform.localScale = animationOldLocalScale;
        animationTarget.transform.localRotation = animationOldLocalRotation;

        temp.Apply(animationTarget.transform);
        animationTimestep += Time.deltaTime;
        if (animationBusy == false)
        {
            Debug.Log("animation done");

            if (animationModifier is TranslationMatrixModifier)
            {
                Vector3 movement = animationTarget.transform.localPosition - animationOldPosition;
                animationTarget.transform.localPosition = animationOldPosition;

                Transform[] ts = animationTarget.GetComponentsInChildren<Transform>();

                foreach (Transform t in ts)
                {
                    if (t == animationTarget.transform) continue;
                    t.position += movement;
                }

            }

            animationModifier = null;
            animationTarget = null;


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
    bool HasCatcherFor(MatrixPlatform platform)
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
        var platformCatcher = platformCatchers.FirstOrDefault(p => p.Catcher == catcher);
        if (platformCatcher == null) return; // Can't find the platform
        Debug.Log("Something was dropped on me");

        // Ask the inventory what modifier is on the used button
        var matrixModifier = InventoryManager.instance.GetMatrixModifierForButton(caughtObject);
        if (matrixModifier == null) return;

        Debug.Log("starting animation");
        // Apply the modifier
        InventoryManager.instance.Consume(matrixModifier);
        StartAnimation(platformCatcher.Platform.Pivot, matrixModifier);
        // TODO: tell the inventory manager this object has been used

    }

    public void Register(MatrixPlatform platform)
    {
        if (platform == null) return;
        matrixPlatforms.Add(platform);
    }
}
