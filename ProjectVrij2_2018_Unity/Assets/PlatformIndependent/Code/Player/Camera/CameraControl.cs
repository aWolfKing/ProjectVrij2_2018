using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    #region input

    private float   inputRY = 0,
                    inputRX = 0;

    public void UpdateRYInput(){
        inputRY = PlayerInput.InputAction.CurrentValue;
    }	
    public void UpdateRXInput(){
        inputRX = PlayerInput.InputAction.CurrentValue;
    }

    #endregion

    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform cameraRoot = null;
    [SerializeField] private float  sensitivityX = 80,
                                    sensitivityY = 80,
                                    cameraMinAngle = -70,
                                    cameraMaxAngle = 70;
    private Quaternion rotation = Quaternion.identity;
    private float cameraYAngle = 0;
    private float cameraDistance = 6;
    private CharacterController cameraCharacterController = null;



    private void OnEnable() {
        cameraCharacterController = camera.GetComponent<CharacterController>();
        if(cameraCharacterController == null){
            var c = camera.gameObject.AddComponent<CharacterController>();
            c.height = 1f;
            cameraCharacterController = c;
        }
    }


    private void Update() {
        Vector3 cameraPositionWas = camera.transform.position;
        rotation *= Quaternion.Euler(Vector3.up * inputRX * sensitivityX * Time.smoothDeltaTime);
        //rotation *= Quaternion.Euler(PlayerMovement.Guide_Right * inputRY * sensitivityY * Time.smoothDeltaTime);
        cameraYAngle = Mathf.Clamp(cameraYAngle + inputRY * sensitivityY * Time.smoothDeltaTime, cameraMinAngle, cameraMaxAngle);

        Vector3 targetPos = cameraRoot.TransformPoint(Quaternion.Euler(Vector3.right * cameraYAngle) * Vector3.forward * -1 * cameraDistance);

        camera.transform.position = cameraRoot.position;
        cameraCharacterController.Move(targetPos - camera.transform.position);

        camera.transform.position = Vector3.Lerp(cameraPositionWas, camera.transform.position, 0.4f);
        //camera.transform.position = targetPos;

        //Quaternion cameraRotationWas = camera.transform.rotation;
        camera.transform.LookAt(cameraRoot);
        //camera.transform.rotation = Quaternion.Lerp(cameraRotationWas, camera.transform.rotation, 0.4f);

        PlayerMovement.SetGuideForward(Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up));
        cameraRoot.rotation = rotation;
    }



}
