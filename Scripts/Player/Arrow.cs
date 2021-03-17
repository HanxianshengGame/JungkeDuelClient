﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class Arrow : MonoBehaviour {

	// Use this for initialization
	private int speed = 5;
	private Rigidbody rid;
	public RoleType roleType;
	public GameObject explosionEffect;
	public bool isLocal = false;

	void Start () {
		rid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player")
        {
			GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
			if(isLocal)
            {
				bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
				if(isLocal != playerIsLocal)
                {
					GameFacade.Instance.SendAttack(Random.Range(10, 20));
                }
            }
        }
		else
        {
			GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }

		GameObject.Instantiate(explosionEffect, transform.position,
			transform.rotation);
		GameObject.Destroy(this.gameObject);
    }

}
