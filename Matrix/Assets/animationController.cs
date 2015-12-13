using UnityEngine;
using System.Collections;

public class animationController : MonoBehaviour {

	public Animator anim;
	public Rigidbody body;
	int jumpHash = Animator.StringToHash("jump");
	int groundedHash = Animator.StringToHash("grounded");
	//int runStateHash = Animator.StringToHash("Base Layer.Run");
	//float swiftness = 0;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetTrigger(groundedHash);
	}
	
	// Update is called once per frame
	void Update () {


	 	
		//transform.position.x += move * Time.deltaTime;
		//transform.position.x += move;

		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);

		//if (Input.GetKeyDown (KeyCode.Space) ) {
			//anim.SetTrigger(jumpHash);
			//transform.position += transform.position + new Vector3(0,0,2f);		
		//}

	}
}
