using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region input

    private float   input_LY = 0,
                    input_LX = 0;

    public void UpdateLYInput(){
        input_LY = PlayerInput.InputAction.CurrentValue;
    }    
    public void UpdateLXInput(){
        input_LX = PlayerInput.InputAction.CurrentValue;
    }

    #endregion

    private static PlayerMovement _this = null;

    [SerializeField] private CharacterController characterController = null;
    //[SerializeField] private float movementSpeed = 2f;
    [SerializeField] private AnimationCurve movementSpeed = new AnimationCurve(new Keyframe(0, 3), new Keyframe(1, 5));
    private Vector3 position = Vector3.zero;
    private Vector3 bodyDirection = Vector3.forward;
    private Vector3 guide_forward = Vector3.forward;
    public static Vector3 Guide_Right{
        get{
            return Quaternion.Euler(Vector3.up * 90) * _this.guide_forward;
        }
    }
    public static Vector3 Guide_Forward{
        get{
            return _this.guide_forward;
        }
    }

    [SerializeField] private AnimationCurve noMovementDodgeCurve = new AnimationCurve(),
                                            movementDodgecurve = new AnimationCurve();

    /// <summary>
    /// Object that block the player from controlling.
    /// </summary>
    private List<object> blockingMovement = new List<object>();

    private bool dodgeButton = false;
    private float   dodgeButtonHoldTime = 0f,
                    dodgeButtonReleaseTime = 0f;
    private bool isRecordingDodgeReleaseTime = false;



    private void OnEnable() {
        _this = this;
    }

    private void Update() {
        if(dodgeButton){
            dodgeButtonHoldTime += Time.smoothDeltaTime;
        }
        else{
            dodgeButtonHoldTime = 0;
        }
    }

    private void FixedUpdate() {
        //Vector3 movementVector = guide_forward * input_LY * movementSpeed * Time.fixedDeltaTime + Guide_Right * input_LX * movementSpeed * Time.fixedDeltaTime;
        float runInp = (dodgeButtonHoldTime - 0.15f) * (1f / (1f - 0.15f));

        Vector3 movementVector = guide_forward * input_LY * movementSpeed.Evaluate(runInp) * Time.fixedDeltaTime + Guide_Right * input_LX * movementSpeed.Evaluate(runInp) * Time.fixedDeltaTime;
        if (blockingMovement.Count == 0){ 
            position += movementVector;
        }
        CollisionFlags flags = characterController.Move(position - characterController.transform.position);
        position = characterController.transform.position;
        bodyDirection = movementVector.normalized;

        if(bodyDirection != Vector3.zero){ 
            characterController.transform.rotation = Quaternion.RotateTowards(characterController.transform.rotation,Quaternion.LookRotation(bodyDirection, Vector3.up), 360f * Time.fixedDeltaTime);
        }
    }



    public static void SetGuideForward(Vector3 forward){
        _this.guide_forward = forward;
    }


    public void OnDodgeButtonPressed(){
        MonoBehaviour.print("dodge button pressed");
        dodgeButton = true;

        /*
        Vector3 movementVector = (guide_forward * input_LY + Guide_Right * input_LX).normalized;
        if (movementVector.magnitude <= 0.3f){
            StartCoroutine(noMovementDodge());
        }
        else{
            StartCoroutine(movementDodge(movementVector));
        }
        */

    }
    public void OnDodgeButtonReleased(){
        MonoBehaviour.print("dodge button released");

        {

            if(dodgeButtonHoldTime <= 0.15f){
                Dodge();
            }

        }

        dodgeButton = false;
    }



    private void Dodge(){
        Vector3 movementVector = (guide_forward * input_LY + Guide_Right * input_LX);
        if(movementVector.magnitude <= 0.3f){
            StartCoroutine(noMovementDodge());
        }
        else {
            StartCoroutine(movementDodge(movementVector));
        }
    }



    private IEnumerator noMovementDodge(){

        //MonoBehaviour.print("noMovementDodge");

        if(!blockingMovement.Contains("noMovementDodge")){ 

            blockingMovement.Add("noMovementDodge");
            float duration = noMovementDodgeCurve.keys[noMovementDodgeCurve.length - 1].time;
            float f = 0;
            do{
                position -= guide_forward * noMovementDodgeCurve.Evaluate(f) * Time.fixedDeltaTime;
                if(f == duration){ break; }
                f = Mathf.Clamp(f + Time.fixedDeltaTime, 0, duration);
                yield return new WaitForFixedUpdate();
            }
            while(true);
            blockingMovement.Remove("noMovementDodge");

        }

    }

    private IEnumerator movementDodge(Vector3 direction){

        //MonoBehaviour.print("movementDodge");
        
        if(!blockingMovement.Contains("movementDodge")){ 

            blockingMovement.Add("movementDodge");
            float duration = movementDodgecurve.keys[movementDodgecurve.length - 1].time;
            float f = 0;
            do {
                position += direction * movementDodgecurve.Evaluate(f) * Time.fixedDeltaTime;
                if (f == duration) { break; }
                f = Mathf.Clamp(f + Time.fixedDeltaTime, 0, duration);
                yield return new WaitForFixedUpdate();
            }
            while (true);
            blockingMovement.Remove("movementDodge");

        }

    }



    #region debug
    #if UNITY_EDITOR

    private void OnDrawGizmos() {
        Color colWas = Gizmos.color;

        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(position, 0.1f);
        }

        Gizmos.color = colWas;
    }

    #endif
    #endregion


}
