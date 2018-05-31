using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v2CameraScript : MonoBehaviour {

    [SerializeField] private new CharacterController camera = null;

    [SerializeField] private Transform  lookTarget = null;
    [SerializeField] private Transform  rootTransform = null;
    private Vector3                     lookTargetPosition = Vector3.zero,
                                        rootPosition = Vector3.zero;
    [SerializeField] private float      minDistance = 1f,
                                        avgDistance = 6f;

    [SerializeField] private float      cameraXSensitivity = 90,
                                        cameraYSensitivity = 90;

    private float verticalCameraAngle = 10;


    private bool autoFollowTarget = true;

    private Vector3 guideForward = Vector3.forward;

    private Vector3 lastLookAtPos = Vector3.zero;
    private Vector3 lastRootPosition = Vector3.zero;


    public Vector3 LookTargetPosition{
        get{
            return this.lookTargetPosition;
        }
        set{
            this.lookTargetPosition = value;
        }
    }

    public Vector3 RootPosition {
        get{
            return this.rootPosition;
        }
        set{
            this.rootPosition = value;
        }
    }

    public Transform LookTarget{
        get{
            return this.lookTarget;
        }
        set{
            this.lookTarget = value;
        }
    }

    public Transform RootTransform{
        get{
            return this.rootTransform;
        }
        set{
            this.rootTransform = value;
        }
    }



    private void LateUpdate() {

        float camXInp = Input.GetAxis("RX") * -1;
        float camYInp = Input.GetAxis("RY");

        //bool playerXInput = false;

        if(Mathf.Abs(camXInp) >= 0.2f){ 
            this.guideForward = Quaternion.Euler(0, camXInp * Time.smoothDeltaTime * this.cameraXSensitivity, 0) * this.guideForward;
            //playerXInput = true;
        }

        if(Mathf.Abs(camYInp) >= 0.2f){
            this.verticalCameraAngle = Mathf.Clamp(this.verticalCameraAngle + (camYInp * Time.smoothDeltaTime * this.cameraYSensitivity), -70, 70);
        }


        if(this.lookTarget != null){
            this.lookTargetPosition = this.lookTarget.position;
        }    

        if(this.rootTransform != null){
            this.rootPosition = this.rootTransform.position;
        }


        if(this.autoFollowTarget){

            Vector3 posWas = this.camera.transform.position;

            this.camera.transform.position = this.rootPosition;

            Vector3 camFromRootDir = -this.guideForward;
            camFromRootDir = Quaternion.Euler(Vector3.Cross(Vector3.up, this.guideForward) * this.verticalCameraAngle) * camFromRootDir;

            //this.camera.Move(Mathf.Clamp( (posWas - this.rootPosition).magnitude , this.minDistance + 0.01f, this.avgDistance) * camFromRootDir);

            this.camera.Move(Mathf.Clamp(Mathf.Lerp((posWas - this.rootPosition).magnitude, this.avgDistance, 0.5f), this.minDistance + 0.01f, this.avgDistance) * camFromRootDir);

            //this.camera.Move((posWas - (this.rootPosition)));

            if((this.camera.transform.position - this.rootPosition).magnitude > this.avgDistance) {
                this.camera.transform.position = this.rootPosition;
                //this.camera.Move((posWas - (this.rootPosition)).normalized * this.avgDistance);
                this.camera.Move(camFromRootDir * this.avgDistance);
            }

            Vector3 destPos = this.camera.transform.position;

            /*
            if((destPos - this.rootPosition).magnitude < this.minDistance){

                //Vector3 tmpGuideDir = (posWas - this.rootPosition).normalized;

                Vector3 tmpGuideDir = camFromRootDir;

                bool camPositionFound = false;

                for(int i=0; i<10; i++){

                    float sAngle = Vector3.SignedAngle(Vector3.ProjectOnPlane((this.lastLookAtPos - posWas), Vector3.up), Vector3.ProjectOnPlane((this.lookTargetPosition - posWas), Vector3.up), Vector3.up);
                    float angle =  sAngle < 0 ? -5 : 5;
                    tmpGuideDir = Quaternion.Euler(0, angle, 0) * tmpGuideDir;

                    this.camera.transform.position = this.rootPosition;

                    this.camera.Move(tmpGuideDir * this.avgDistance);

                    if((this.camera.transform.position - this.rootPosition).magnitude >= this.minDistance){
                        camPositionFound = true;
                        break;
                    }

                }

                if(!camPositionFound){
                    this.camera.transform.position = posWas;
                }

                destPos = this.camera.transform.position;

            }
            */

            this.camera.transform.position = Vector3.Lerp(posWas, destPos, 1.2f*Time.smoothDeltaTime /*0.35f*/);

            /*
            if(!playerXInput){
                Vector3 newDir = (this.rootPosition - this.lastRootPosition);
                if(newDir.magnitude > 0){ 
                    this.guideForward = Vector3.MoveTowards(this.guideForward, newDir.normalized, Time.smoothDeltaTime * 2f).normalized;
                }
            }
            */

        }


        Quaternion cameraRotationWas = this.camera.transform.rotation;

        this.camera.transform.LookAt(this.lookTargetPosition);
        this.lastLookAtPos = this.lookTargetPosition;
        this.lastRootPosition = this.rootPosition;

        this.camera.transform.rotation = Quaternion.Lerp(cameraRotationWas, this.camera.transform.rotation, 0.4f);

        /*
        if(!playerXInput){
            this.guideForward = Vector3.ProjectOnPlane((this.rootPosition - this.camera.transform.position).normalized, Vector3.up);
        }
        */  

    }



#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Color colorWas = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.rootPosition, this.rootPosition + this.guideForward);
        Gizmos.DrawWireSphere(this.rootPosition + this.guideForward, 0.02f);

        if(this.camera != null){ 
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(this.rootPosition, this.camera.transform.position);
        }

        Gizmos.color = colorWas;
    }
#endif

}
