using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	// Use this for initialization
	public Transform targetTransform;

	private Vector3 offset = new Vector3(0, 10.52662f, -12.12619f);
	private float smoothing = 2;

	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate() {
		Vector3 targetPosition = targetTransform.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
		transform.LookAt(targetTransform);
	}


 }
