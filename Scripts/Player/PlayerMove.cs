using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	// Use this for initialization
	private float speed = 3;
	public float forward = 0;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void FixedUpdate() {		
		
		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false)
			return;


		if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
		{
			transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);

			Vector3 lookRotation = new Vector3(h, 0, v);

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false)
				return;
			transform.rotation = Quaternion.LookRotation(lookRotation);
			float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
			forward = res;
			anim.SetFloat("Forward", res);
		}
	}
}
