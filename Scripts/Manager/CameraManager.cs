using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager: BaseManager {
    public CameraManager(GameFacade facade) : base(facade)
    {

    }

    private GameObject cameraGo;
    private Animator cameraAnim;
    private FollowTarget followTarget;
    private Vector3 orginalPostion;
    private Vector3 orginalRotation;

    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        cameraAnim = cameraGo.GetComponent<Animator>();
        followTarget = cameraGo.GetComponent<FollowTarget>();
        orginalPostion = cameraGo.transform.position;
        orginalRotation = cameraGo.transform.eulerAngles;
    }

    public override void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FollowRole();
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    WakethroughScene();
        //}
    }

    public void FollowRole()
    {
        followTarget.targetTransform = facade.GetCurrentRoleGameObject().transform;
        cameraAnim.enabled = false;          
        orginalPostion = cameraGo.transform.position;
        orginalRotation = cameraGo.transform.eulerAngles;
        var targetQuaternion = Quaternion.LookRotation(followTarget.targetTransform.position - cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(targetQuaternion, 1f).OnComplete(() =>
        {
            followTarget.enabled = true;
        });
    }

    public void WakethroughScene()
    {
        followTarget.enabled = false;
        cameraGo.transform.DOMove(orginalPostion, 1f);
        cameraGo.transform.DORotate(orginalRotation, 1f);
        cameraGo.transform.DOMove(orginalPostion, 1f).OnComplete(() =>
        {
            cameraAnim.enabled = true;
        });

    }
}
