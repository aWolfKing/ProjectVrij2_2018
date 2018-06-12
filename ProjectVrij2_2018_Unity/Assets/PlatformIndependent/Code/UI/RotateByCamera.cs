using System;
using System.Collections.Generic;
using UnityEngine;


public class RotateByCamera : MonoBehaviour{

    //[SerializeField] private new Camera camera = null;
    [SerializeField] private Transform transformToRotate = null;

    private void Update() {
        this.transformToRotate.forward = /*this.camera.transform.forward*/ PlayerControl.Camera.transform.forward;
    }

}

