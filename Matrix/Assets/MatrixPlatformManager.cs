using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityStandardAssets._2D;

public class MatrixPlatformManager : MonoBehaviour {
    public Camera mainCam;
    public List<MatrixPlatform> matrixPlatforms = new List<MatrixPlatform>();
    public Canvas mainCanvas;
    public Vector2 onScreenOffset = new Vector2(10, 5);

    public GameObject dropperPrefab;
    public GameObject pivotIndicatorPrefab;
    public GameObject moveablePanelPrefab;

    // Animation information
    private bool animationBusy = false;
    private float animationTimestep = 0;
    private float animationLength = 0.5f;
    private Vector3 animationOldPosition;
    private Vector3 animationOldLocalScale;
    private Quaternion animationOldLocalRotation;

    private GameObject animationTarget;
    private MatrixModifier animationModifier;

	private bool moveThePlayer;

	public GameObject player;

    [Serializable]
    private class PlatformCatcherPair
    {
        public MatrixPlatform Platform { get; set; }
        public GameObject Catcher { get; set; }
        public GameObject PivotIndicator { get; set; }
        public List<GameObject> platformIndicators { get; set; }

        public PlatformCatcherPair() { }
        public PlatformCatcherPair(MatrixPlatform platform, GameObject catcher, GameObject pivotIndicator) { Platform = platform; Catcher = catcher; PivotIndicator = pivotIndicator; }
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
                Destroy(platformCatcher.PivotIndicator);
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
                dropperGO.transform.position = Vector3.zero;
                var pivotIndicatorGO = Instantiate(pivotIndicatorPrefab);
                pivotIndicatorGO.transform.SetParent(platform.transform);
                pivotIndicatorGO.transform.localPosition = Vector3.zero;
                pivotIndicatorGO.transform.localScale = Vector3.one;
                pivotIndicatorGO.transform.localRotation = Quaternion.identity;
                platformCatchers.Add(new PlatformCatcherPair(platform, dropperGO, pivotIndicatorGO));
                // TODO: Add an event listener to the platform to tell is of it has caught something.
            }
        }

        // Update the catcher positions
        foreach(var platformCatcher in platformCatchers)
        {
            Vector3 pos = GetScreenPos(platformCatcher.Platform.Pivot);
            var catcherTransform = platformCatcher.Catcher.GetComponent<RectTransform>();
            catcherTransform.position = new Vector3(pos.x, pos.y, 0);
			catcherTransform.localScale = new Vector3 (1, 1, 1);
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
		animationLength = 0.5f;

		if (animation is TranslationMatrixModifier) {
			TranslationMatrixModifier temp = (TranslationMatrixModifier) animation;
			float distanceTravel = temp.translation.magnitude;
			if (distanceTravel > 4) {
				animationLength = distanceTravel / 8;
			}
		}

		moveThePlayer = false;

		if (animation is TranslationMatrixModifier) {

			Debug.Log ("Checking for problems...");

			Transform[] ts = animationTarget.GetComponentsInChildren<Transform>();
			foreach (Transform t in ts)
			{
				bool doSomething = player.GetComponent<PlatformerCharacter2D> ().CheckIfGameObjectIsCollidingWithMe (t.gameObject);

				if (doSomething) {

					moveThePlayer = true;


				}

			}

		}
    }

    void DoAnimation()
    {
        MatrixModifier temp = animationModifier.Clone(); // get the animation
        float ratio = animationTimestep / animationLength;

        //Debug.Log("ratio " + ratio);

        if (ratio > 1)
        {
            ratio = 1;
            animationBusy = false;
        }
        temp.Tween(ratio);

        animationTarget.transform.localPosition = animationOldPosition;
        animationTarget.transform.localScale = animationOldLocalScale;
        animationTarget.transform.localRotation = animationOldLocalRotation;

		if (moveThePlayer) {
			MatrixModifier temp2 = animationModifier.Clone ();
			temp2.Tween (Time.deltaTime / animationLength);
			temp2.Apply (player.transform);

			Debug.Log ("MOVE DA PLAYER");
		}

        temp.Apply(animationTarget.transform);
        animationTimestep += Time.deltaTime;
        if (animationBusy == false)
        {
            Debug.Log("animation done");

			int ti = 0;
			Transform[] ts = animationTarget.GetComponentsInChildren<Transform>();



			Vector3[] newpos = new Vector3[ts.Length];
			Quaternion[] newrot = new Quaternion[ts.Length];
			Vector3[] newscale = new Vector3[ts.Length];

			foreach (Transform t in ts)
			{
				if (t == animationTarget.transform) continue;
				newpos[ti] = t.position;
				newscale [ti] = t.lossyScale;
				newrot [ti] = t.rotation;
				ti++;
			}

			animationTarget.transform.localPosition = new Vector3 (0, 0, 0);
			animationTarget.transform.localRotation = Quaternion.identity;
			animationTarget.transform.localScale = new Vector3 (1, 1, 1);

			ti = 0;
			foreach (Transform t in ts) {
				if (t == animationTarget.transform) continue;
				t.position = newpos [ti];
				t.localScale = newscale [ti];
				t.localRotation = newrot [ti];
				ti++;
			}

			/*

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

			if (animationModifier is ScalingMatrixModifier) {

				Vector3 temp = transform.lossyScale;

			}*/

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
        return pos.x < Screen.width + onScreenOffset.x && pos.x > -onScreenOffset.x && pos.y < Screen.height + onScreenOffset.y && pos.y > -onScreenOffset.y;
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
