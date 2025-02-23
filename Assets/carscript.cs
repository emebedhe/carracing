using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carscript : MonoBehaviour
{
//CAR SETUP

    private ArrayList cplist = new ArrayList();

    public Text key1t;
    public Text key2t;

    public float cp1time = -2347823;
    public float cp2time = -2347823;
    public float cp3time = -2347823;
    public float cp4time = -2347823;
    public int maxSpeed = 90;
    public int maxReverseSpeed = 45; 
    public int accelerationMultiplier = 2; 
    public int maxSteeringAngle = 27;
    public float steerlerpthinglol = 0.5f;

    public string key1 = "";
    public string key2 = "";

    public int brakeForce = 350; 
    public int decelerationMultiplier = 2; 
    public int handbrakeDriftMultiplier = 5; 
    public Vector3 centerofmass; 

    public WheelCollider flc;

    public WheelCollider frc;

    public WheelCollider rlc;

    public WheelCollider rrc;

    public float carSpeed; 
    public bool isDrifting; 
    public bool tractoin; 

    private string upshift;


    Rigidbody rb;
    float steeringAxis;
    float throttleAxis; 
    float driftingAxis;
    float localVelocityZ;
    float localVelocityX;
    bool deceleratingCar;
    WheelFrictionCurve FLwheelFriction;
    float FLWextremumSlip;
    WheelFrictionCurve FRwheelFriction;
    float FRWextremumSlip;
    WheelFrictionCurve RLwheelFriction;
    float RLWextremumSlip;
    WheelFrictionCurve RRwheelFriction;
    float RRWextremumSlip;

    public GameObject lastcheckpoint;
    public bool finished = false;

    private float start = -234567890;

// Start is called before the first frame update
void Start()
{
    rb = gameObject.GetComponent<Rigidbody>();
    rb.centerOfMass = centerofmass;
    FLwheelFriction = new WheelFrictionCurve ();
    FLwheelFriction.extremumSlip = flc.sidewaysFriction.extremumSlip;
    FLWextremumSlip = flc.sidewaysFriction.extremumSlip;
    FLwheelFriction.extremumValue = flc.sidewaysFriction.extremumValue;
    FLwheelFriction.asymptoteSlip = flc.sidewaysFriction.asymptoteSlip;
    FLwheelFriction.asymptoteValue = flc.sidewaysFriction.asymptoteValue;
    FLwheelFriction.stiffness = flc.sidewaysFriction.stiffness;
    FRwheelFriction = new WheelFrictionCurve ();
    FRwheelFriction.extremumSlip = frc.sidewaysFriction.extremumSlip;
    FRWextremumSlip = frc.sidewaysFriction.extremumSlip;
    FRwheelFriction.extremumValue = frc.sidewaysFriction.extremumValue;
    FRwheelFriction.asymptoteSlip = frc.sidewaysFriction.asymptoteSlip;
    FRwheelFriction.asymptoteValue = frc.sidewaysFriction.asymptoteValue;
    FRwheelFriction.stiffness = frc.sidewaysFriction.stiffness;
    RLwheelFriction = new WheelFrictionCurve ();
    RLwheelFriction.extremumSlip = rlc.sidewaysFriction.extremumSlip;
    RLWextremumSlip = rlc.sidewaysFriction.extremumSlip;
    RLwheelFriction.extremumValue = rlc.sidewaysFriction.extremumValue;
    RLwheelFriction.asymptoteSlip = rlc.sidewaysFriction.asymptoteSlip;
    RLwheelFriction.asymptoteValue = rlc.sidewaysFriction.asymptoteValue;
    RLwheelFriction.stiffness = rlc.sidewaysFriction.stiffness;
    RRwheelFriction = new WheelFrictionCurve ();
    RRwheelFriction.extremumSlip = rrc.sidewaysFriction.extremumSlip;
    RRWextremumSlip = rrc.sidewaysFriction.extremumSlip;
    RRwheelFriction.extremumValue = rrc.sidewaysFriction.extremumValue;
    RRwheelFriction.asymptoteSlip = rrc.sidewaysFriction.asymptoteSlip;
    RRwheelFriction.asymptoteValue = rrc.sidewaysFriction.asymptoteValue;
    RRwheelFriction.stiffness = rrc.sidewaysFriction.stiffness;

    lastcheckpoint = GameObject.Find("start_line");

    key1t.enabled = false;
    key2t.enabled = false;
}

void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Checkpoint") {
            if (cplist.Contains(other)) {}
            else { cplist.Add(other);}
            lastcheckpoint = other.gameObject;
            if (lastcheckpoint.name == "Cp1") {
            if (cp1time == -2347823) {
                cp1time = Time.time-start;
                Debug.Log(cp1time);
            }
            }
            if (lastcheckpoint.name == "Cp2") {
            Debug.Log("what the bruh");
            Debug.Log(cp2time);
            Debug.Log(cp2time==0);
                if (cp2time == 0) {
                    cp2time = Time.time-start;
                    Debug.Log(cp2time);
                }
            }
            if (lastcheckpoint.name == "Cp3") {
            if (cp3time == 0) {
                cp3time = Time.time-start;
                Debug.Log(cp3time);
            }
            }
            if (lastcheckpoint.name == "Cp4") {
            if (cp4time == 0) {
                cp4time = Time.time-start;
                Debug.Log(cp4time);
            }
            }
        }

        if (other.gameObject.tag == "Finishline") {
            if (cplist.Count == 4) {
                finished = true;
            }
        }
    }
// Update is called once per frame
void Update()
{
    carSpeed = (2 * 3.14f * flc.radius * flc.rpm * 60) / 1000;
    localVelocityX = transform.InverseTransformDirection(rb.linearVelocity).x;
    localVelocityZ = transform.InverseTransformDirection(rb.linearVelocity).z;
    

    if(Input.GetKey(KeyCode.W)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        GoForward();
        if (start == -234567890) { start = Time.time;}
    }
    if(Input.GetKey(KeyCode.S)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        GoReverse();
        if (start == -234567890) { start = Time.time;}
    }
    if(Input.GetKey(KeyCode.A)){
        TurnLeft();
        if (start == -234567890) { start = Time.time;}
    }
    if(Input.GetKey(KeyCode.D)){
        TurnRight();
        if (start == -234567890) { start = Time.time;}
    }

    if(Input.GetKey(KeyCode.Space)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        Handbrake();
    }

    if(Input.GetKeyUp(KeyCode.Space)){
        RecoverTraction();
    }

    if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))){
        ThrottleOff();
    }

    if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) && !deceleratingCar){
        InvokeRepeating("DecelerateCar", 0f, 0.1f);
        deceleratingCar = true;
    }

    if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && steeringAxis != 0f){
        ResetSteeringAngle();
    }
    // if(Input.GetKey(KeyCode.Alpha1)){
    //     transform.position = new Vector3(269.732452f,8.38261509f,61.4506454f);
    // }

    if (Input.GetKey(KeyCode.R)) {
        ResetPosition();
    }

    // if (Input.anyKeyDown && key1 == "")
    // {
    //     key1 = Input.inputString;
    //     key1t.enabled = false;
    //     key2t.enabled = true;
    // }
    // if (Input.anyKeyDown && key2 == "" && key1 != "") {
    //     key2 = Input.inputString;
    //     key2t.enabled = false;
    // }  

}
public void TurnLeft(){
    steeringAxis = steeringAxis - (Time.deltaTime * 10f * steerlerpthinglol);
    if(steeringAxis < -1f){
    steeringAxis = -1f;
    }
    var steeringAngle = steeringAxis * maxSteeringAngle;
    flc.steerAngle = Mathf.Lerp(flc.steerAngle, steeringAngle, steerlerpthinglol);
    frc.steerAngle = Mathf.Lerp(frc.steerAngle, steeringAngle, steerlerpthinglol);
}
public void TurnRight(){
    steeringAxis = steeringAxis + (Time.deltaTime * 10f * steerlerpthinglol);
    if(steeringAxis > 1f){
    steeringAxis = 1f;
    }
    var steeringAngle = steeringAxis * maxSteeringAngle;
    flc.steerAngle = Mathf.Lerp(flc.steerAngle, steeringAngle, steerlerpthinglol);
    frc.steerAngle = Mathf.Lerp(frc.steerAngle, steeringAngle, steerlerpthinglol);
}
public void ResetSteeringAngle(){
    if(steeringAxis < 0f){
    steeringAxis = steeringAxis + (Time.deltaTime * 10f * steerlerpthinglol);
    }else if(steeringAxis > 0f){
    steeringAxis = steeringAxis - (Time.deltaTime * 10f * steerlerpthinglol);
    }
    if(Mathf.Abs(flc.steerAngle) < 1f){
    steeringAxis = 0f;
    }
    var steeringAngle = steeringAxis * maxSteeringAngle;
    flc.steerAngle = Mathf.Lerp(flc.steerAngle, steeringAngle, steerlerpthinglol);
    frc.steerAngle = Mathf.Lerp(frc.steerAngle, steeringAngle, steerlerpthinglol);
}
public void GoForward(){
    if(Mathf.Abs(localVelocityX) > 2.5f){
    isDrifting = true;
    }else{
    isDrifting = false;
    }
    throttleAxis = throttleAxis + (Time.deltaTime * 3f);
    if(throttleAxis > 1f){
    throttleAxis = 1f;
    }
    if(localVelocityZ < -1f){ //so that we dont go backwards
    Brakes();
    }else{
    if(Mathf.RoundToInt(carSpeed) < maxSpeed){
        flc.brakeTorque = 0;
        flc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        frc.brakeTorque = 0;
        frc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rlc.brakeTorque = 0;
        rlc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rrc.brakeTorque = 0;
        rrc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
    }else {
            flc.motorTorque = 0;
            frc.motorTorque = 0;
        rlc.motorTorque = 0;
            rrc.motorTorque = 0;
        }
    }
}

public void GoReverse(){
    if(Mathf.Abs(localVelocityX) > 2.5f){
    isDrifting = true;
    }else{
    isDrifting = false;
    }
    throttleAxis = throttleAxis - (Time.deltaTime * 3f);
    if(throttleAxis < -1f){
    throttleAxis = -1f;
    }
    if(localVelocityZ > 1f){
    Brakes();
    }else{
    if(Mathf.Abs(Mathf.RoundToInt(carSpeed)) < maxReverseSpeed){
        flc.brakeTorque = 0;
        flc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        frc.brakeTorque = 0;
        frc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rlc.brakeTorque = 0;
        rlc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rrc.brakeTorque = 0;
        rrc.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
    }else {
            flc.motorTorque = 0;
            frc.motorTorque = 0;
        rlc.motorTorque = 0;
            rrc.motorTorque = 0;
        }
    }
}

public void ThrottleOff(){
    flc.motorTorque = 0;
    frc.motorTorque = 0;
    rlc.motorTorque = 0;
    rrc.motorTorque = 0;
}
//runs if w, s, or space is not down
public void DecelerateCar(){
    if(Mathf.Abs(localVelocityX) > 2.5f){
    isDrifting = true;
    
    }else{
    isDrifting = false;
    }
    if(throttleAxis != 0f){
    if(throttleAxis > 0f){
        throttleAxis = throttleAxis - (Time.deltaTime * 10f);
    }else if(throttleAxis < 0f){
        throttleAxis = throttleAxis + (Time.deltaTime * 10f);
    }
    if(Mathf.Abs(throttleAxis) < 0.15f){
        throttleAxis = 0f;
    }
    }
    rb.linearVelocity = rb.linearVelocity * (1f / (1f + (0.025f * decelerationMultiplier)));
    flc.motorTorque = 0;
    frc.motorTorque = 0;
    rlc.motorTorque = 0;
    rrc.motorTorque = 0;
    if(rb.linearVelocity.magnitude < 0.25f){
    rb.linearVelocity = Vector3.zero;
    CancelInvoke("DecelerateCar");
    }
}

public void Brakes(){
    flc.brakeTorque = brakeForce;
    frc.brakeTorque = brakeForce;
    rlc.brakeTorque = brakeForce;
    rrc.brakeTorque = brakeForce;
}
public void Handbrake(){
    CancelInvoke("RecoverTraction");
    driftingAxis = driftingAxis + (Time.deltaTime);
    float secureStartingPoint = driftingAxis * FLWextremumSlip * handbrakeDriftMultiplier;

    if(secureStartingPoint < FLWextremumSlip){
    driftingAxis = FLWextremumSlip / (FLWextremumSlip * handbrakeDriftMultiplier);
    }
    if(driftingAxis > 1f){
    driftingAxis = 1f;
    }
    if(Mathf.Abs(localVelocityX) > 2.5f){
    isDrifting = true;
    }else{
    isDrifting = false;
    }
    if(driftingAxis < 1f){
    FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    flc.sidewaysFriction = FLwheelFriction;

    FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    frc.sidewaysFriction = FRwheelFriction;

    RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    rlc.sidewaysFriction = RLwheelFriction;

    RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    rrc.sidewaysFriction = RRwheelFriction;
    }
    tractoin = true;


}

public void ResetPosition() {
    rb.transform.position = lastcheckpoint.transform.position;
    rb.transform.rotation = lastcheckpoint.transform.rotation;
    
}

public void RecoverTraction(){
    tractoin = false;
    driftingAxis = driftingAxis - (Time.deltaTime / 1.5f);
    if(driftingAxis < 0f){
    driftingAxis = 0f;
    }
    if(FLwheelFriction.extremumSlip > FLWextremumSlip){
    FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    flc.sidewaysFriction = FLwheelFriction;

    FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    frc.sidewaysFriction = FRwheelFriction;

    RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    rlc.sidewaysFriction = RLwheelFriction;

    RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
    rrc.sidewaysFriction = RRwheelFriction;

    Invoke("RecoverTraction", Time.deltaTime);

    }else if (FLwheelFriction.extremumSlip < FLWextremumSlip){
    FLwheelFriction.extremumSlip = FLWextremumSlip;
    flc.sidewaysFriction = FLwheelFriction;

    FRwheelFriction.extremumSlip = FRWextremumSlip;
    frc.sidewaysFriction = FRwheelFriction;

    RLwheelFriction.extremumSlip = RLWextremumSlip;
    rlc.sidewaysFriction = RLwheelFriction;

    RRwheelFriction.extremumSlip = RRWextremumSlip;
    rrc.sidewaysFriction = RRwheelFriction;

    driftingAxis = 0f;
    }
}

}
