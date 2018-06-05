using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private string  inp_vertical = "Vertical",
                    inp_horizontal = "Horizontal";

    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private new CharacterController camera = null;
    [SerializeField] private float  movementSpeed = 2f,
                                    dodgeSpeed = 3f;

    [SerializeField] private float  movementDeadzone = 0.15f,
                                    cameraDeadzone = 0.15f;

    [Header("Camera settings")]
    [SerializeField] private Transform  cameraRoot = null,
                                        cameraRootReference = null,
                                        cameraLookAt = null;
    [SerializeField] private float  maxCameraMoveSpeed = 5f,
                                    cameraSensitivity_Horizontal = 90,
                                    cameraSensitivity_Vertical = 90,
                                    cameraDistance = 4f;
    private Vector3 cameraMovement = Vector3.zero,
                    cameraRotation = Vector3.zero;


    [Header("Animation settings")]
    [SerializeField] private float  maxMovementAnimSpeed = 0.8f,
                                    minMovementAnimSpeed = 0.3f;


    private List<object> stunCauses = new List<object>();

    private Vector3 movement = Vector3.zero;

    private float   input_vertical = 0,
                    input_horizontal = 0,
                    input_cameraVertical = 0,
                    input_cameraHorizontal = 0;

    private int anim_isWalkingID = 0,
                anim_movementSpeed = 0,
                anim_dodge = 0,
                anim_heavyAttack = 0,
                anim_lightAttack = 0;

    private List<object> blockingDodge = new List<object>();

    private bool canAttack = true;

    [Header("Attack settings")]
    [SerializeField] private float  damage_heavyAttack = 50f,
                                    damage_lightAttack = 20f;
    [SerializeField] private Collider   hitbox_heavyAttack = null,
                                        hitbox_lightAttack = null,
                                        hitbox_interaction = null;
    [SerializeField] Transform aimTarget = null;



    private Vector3 MoveForward{
        get{
            return Vector3.ProjectOnPlane(this.camera.transform.forward, Vector3.up);
        }
    }

    public bool IsStunned{
        get{
            return this.stunCauses.Count > 0;
        }
    }

    public bool CanDodge{
        get{
            return this.blockingDodge.Count == 0;
        }
    }

    public float CurrentMovementSpeed{
        get{
            return this.movementSpeed;
        }
    }



    private void Awake() {
        this.anim_isWalkingID = Animator.StringToHash("isMoving");
        this.anim_movementSpeed = Animator.StringToHash("movementAnimSpeed");
        this.anim_dodge = Animator.StringToHash("Dodge");
        this.anim_heavyAttack = Animator.StringToHash("HeavyAttack");
        this.anim_lightAttack = Animator.StringToHash("LightAttack");
    }



    private void Update() {
        this.input_vertical = this.TestPassDeadzone(Input.GetAxis("LY"), this.movementDeadzone);
        this.input_horizontal = this.TestPassDeadzone(Input.GetAxis("LX"), this.movementDeadzone);
        this.input_cameraVertical = this.TestPassDeadzone(Input.GetAxis("RY"), this.cameraDeadzone);
        this.input_cameraHorizontal = this.TestPassDeadzone(Input.GetAxis("RX"), this.cameraDeadzone);
    }



    private void FixedUpdate() {

        Vector3 position_function_start = this.characterController.transform.position;

        ///Can walk if not stunned
        if(!this.IsStunned){

            Vector3 movementVector = this.MoveForward * this.input_vertical
                                    + Quaternion.Euler(Vector3.up * 90) * this.MoveForward * this.input_horizontal;
            Vector3 targetPosition = this.characterController.transform.position + movementVector * this.CurrentMovementSpeed;
            targetPosition = Vector3.SmoothDamp(this.characterController.transform.position, targetPosition, ref this.movement, 1f, this.CurrentMovementSpeed, Time.fixedDeltaTime);
            this.characterController.Move(targetPosition - this.characterController.transform.position);

            this.animator.SetBool(this.anim_isWalkingID, movementVector.magnitude > 0);
            this.animator.SetFloat(this.anim_movementSpeed, Mathf.Lerp(this.minMovementAnimSpeed, this.maxMovementAnimSpeed, movementVector.magnitude));

            if(movementVector.magnitude > 0 || this.aimTarget != null){
                if(this.aimTarget == null){ 
                    this.characterController.transform.rotation = Quaternion.Lerp(this.characterController.transform.rotation, Quaternion.LookRotation(movementVector.normalized, Vector3.up), Time.fixedDeltaTime * 3);
                }
                else{
                    this.characterController.transform.rotation = Quaternion.Lerp(this.characterController.transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(this.aimTarget.position - this.characterController.transform.position, Vector3.up), Vector3.up), Time.fixedDeltaTime * 3);
                }
            }

        }

    }



    private void LateUpdate() {

        this.cameraRoot.transform.Rotate(Vector3.up * this.input_cameraHorizontal * this.cameraSensitivity_Horizontal * -1 * Time.deltaTime, Space.World);

        Vector3 cameraPosWas = this.camera.transform.position;

        this.cameraRoot.position = this.cameraRootReference.position;

        this.camera.transform.position = this.cameraRoot.position;
        this.camera.Move(this.cameraRoot.forward * -1 * this.cameraDistance);

        Vector3 targetPosition = this.camera.transform.position;
        this.camera.transform.position = cameraPosWas;

        targetPosition = Vector3.SmoothDamp(cameraPosWas, targetPosition, ref this.cameraMovement, 0.5f, this.maxCameraMoveSpeed, Time.deltaTime);
        this.camera.Move(targetPosition - cameraPosWas);

        //Vector3 lookwasRot = this.camera.transform.rotation * Vector3.forward;

        this.camera.transform.LookAt(this.cameraLookAt.position);

        //Vector3 lookatRot = this.camera.transform.rotation * Vector3.forward;

        //this.camera.transform.rotation = Quaternion.LookRotation(Vector3.SmoothDamp(lookwasRot, lookatRot, ref this.cameraRotation, 0.3f, this.cameraSensitivity_Horizontal, Time.deltaTime));

    }



    private float TestPassDeadzone(float value, float deadzone){
        if(Mathf.Abs(value) >= deadzone){
            return value;
        }
        else{
            return 0f;
        }
    }



    public void Stun(float duration){
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration){
        object cause = new object();
        this.stunCauses.Add(cause);
        yield return new WaitForSeconds(duration);
        this.stunCauses.Remove(cause);
    }



    public void Dodge(){
        if(this.CanDodge){ 
            Vector3 direction = this.characterController.transform.forward * -1;
            if(this.movement.magnitude > 0.5f * Time.fixedDeltaTime){
                direction = this.movement.normalized;
            }
            StartCoroutine(DodgeCoroutine(direction));
        }
    }

    private IEnumerator DodgeCoroutine(Vector3 direction, float distance = 3){
        object dodgePrevent = new object();
        this.blockingDodge.Add(dodgePrevent);

        float duration = 1.26f;

        float endLag = 0.4f;


        this.Stun(duration + endLag);
        this.animator.Play(this.anim_dodge);

        float windup = 0.3f;
        yield return new WaitForSeconds(windup);

        var wait = new WaitForFixedUpdate();
        float t = duration - windup;

        Vector3 posWas = this.characterController.transform.position;
        this.characterController.Move(direction * distance);
        Vector3 targetPos = this.characterController.transform.position;
        this.characterController.transform.position = posWas;

        do {
            if(t <= 0){ break; }
            // || Vector3.Distance(this.characterController.transform.position, targetPos) < 0.015f

            this.characterController.Move(Vector3.SmoothDamp(this.characterController.transform.position, targetPos, ref this.movement, 1f, this.dodgeSpeed, Time.fixedDeltaTime) - this.characterController.transform.position);

            yield return wait;
            t -= Time.fixedDeltaTime;
        }
        while(true);

        this.movement *= 0;

        yield return new WaitForSeconds(endLag);

        this.blockingDodge.Remove(dodgePrevent);
    }



    public void HeavyAttack(){
        if(this.canAttack){
            StartCoroutine(HeavyAttackCoroutine());
        }
    }
    
    private IEnumerator HeavyAttackCoroutine(){
        this.canAttack = false;

        object dodgePrevent = new object();
        this.blockingDodge.Add(dodgePrevent);

        float duration = 2f;
        float windup = 1.15f;
        float endLag = 1.35f;

        this.Stun(duration + endLag);

        this.animator.Play(this.anim_heavyAttack);

        yield return new WaitForSeconds(windup);
        
        float t = duration - windup;

        Damager damager = new Damager(Damager.DamageCause.Player | Damager.DamageCause.Melee, this.damage_heavyAttack);
        

        do {
            if(t <= 0){ break; }

            Damager.Current = damager;
            HitboxString_basedDamagable.SendHit(this.hitbox_heavyAttack);

            yield return new WaitForFixedUpdate();
            t -= Time.fixedDeltaTime;
        }
        while(true);

        damager.InflictStoredDamage();


        yield return new WaitForSeconds(endLag);

        this.blockingDodge.Remove(dodgePrevent);

        this.canAttack = true;
    }
    


    public void LightAttack(){
        if(this.canAttack){
            StartCoroutine(LightAttackCoroutine());
        }
    }

    private IEnumerator LightAttackCoroutine(){
        this.canAttack = false;

        object dodgePrevent = new object();
        this.blockingDodge.Add(dodgePrevent);

        float duration = 0.9f;
        float windup = 0.4f;
        float endLag = 0.5f;

        this.Stun(duration + endLag);

        this.animator.Play(this.anim_lightAttack);

        yield return new WaitForSeconds(windup);


        float t = duration - windup;

        Damager damager = new Damager(Damager.DamageCause.Player | Damager.DamageCause.Melee, this.damage_lightAttack);


        do {
            if(t <= 0) { break; }

            Damager.Current = damager;
            HitboxString_basedDamagable.SendHit(this.hitbox_lightAttack);

            yield return new WaitForFixedUpdate();
            t -= Time.fixedDeltaTime;
        }
        while(true);

        damager.InflictStoredDamage();


        yield return new WaitForSeconds(endLag);

        this.blockingDodge.Remove(dodgePrevent);

        this.canAttack = true;
    }



    public void Interact(){
        Damager damager = new Damager(Damager.DamageCause.Interaction | Damager.DamageCause.Player, 0);
        Damager.Current = damager;
        HitboxString_basedDamagable.SendHit(this.hitbox_interaction);
        damager.ClearStoredDamage();
    }

}
