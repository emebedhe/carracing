using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class carscript : MonoBehaviour
{
//CAR SETUP
    public GameObject ghost;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;
    public GameObject ghost5;

    private List<int> replaysdisplayed = new List<int>(){1,2,3,4,5};

    List<List<float>> replaymanager = new List<List<float>>();
    List<string> replay1 = new List<string>();
    List<string> replay2 = new List<string>();
    List<string> replay3 = new List<string>();
    List<string> replay4 = new List<string>();
    List<string> replay5 = new List<string>();


    public AudioSource accelplay;
    public AudioSource brakeplay;
    //private float replaylistlength = 0;
    List<float> finishtimes = new List<float>();
    private float replaytime;
    //private float currentreplayframe = 0;

    public bool createreplay = false;

    public GameObject stuntstartline;

    private int frametimer=0;
    private int replayframe;


    public bool replaystarted = false;
    private bool replaywritten = false;

    public GameObject FLMesh;
    public GameObject FRMesh;
    public GameObject FLTread;
    public GameObject FRTread;
    private ArrayList cplist = new ArrayList();

    public Collider grass;
    private string track = "track 1";

    public Text key1t;
    public Text key2t;

    public Text cptext;

    public Text timetext;
    public Text speedtext;

    private bool started = false;

    public float cp1time = -2347823;
    public float cp2time = -2347823;
    public float cp3time = -2347823;
    public float cp4time = -2347823;
    public int maxSpeed = 90;
    public int maxReverseSpeed = 45; 
    public int accelerationMultiplier = 2; 
    public int maxSteeringAngle = 27;
    public float steerlerpthinglol = 0.5f;
    public int brakeForce = 350; 
    public int decelerationMultiplier = 2; 
    public float grassPenalty = 7;
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
    public float respawnoffset;

    public Button Q;
    public Button E;

    public bool paused = false;

    public Button Play;

    private float currentframe;
    private float startframe;

// Start is called before the first frame update
void Start() {
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


    //REPLAY PROCESSING
    List<string> idk;
    StreamReader reader = new StreamReader("Assets/saves.txt");
    idk = new List<string>(reader.ReadToEnd().Split('\n'));
    reader.Close();
    List<string> saved5replays = new List<string>();

    // Process each line
    foreach (string item in idk)
    {
        string[] splitData = item.Split('|'); 
        if (splitData.Length > 1) 
        {
            finishtimes.Add(float.Parse(splitData[1]));
            saved5replays.Add(splitData[0]);
        }
    }
    Debug.Log(saved5replays[0].GetType());


    replay1 = saved5replays[0].Split(",").ToList();
    replay2 = saved5replays[1].Split(",").ToList();
    replay3 = saved5replays[2].Split(",").ToList();
    replay4 = saved5replays[3].Split(",").ToList();
    replay5 = saved5replays[4].Split(",").ToList();
    Debug.Log(replay1.Count());
    // replay2 = saved5replays[1];
    // replay3 = saved5replays[2];
    // replay4 = saved5replays[3];
    // replay5 = saved5replays[4];


    // Debug.Log(saved5replays[0]);
    // Debug.Log(finishtimes.Count());

    // foreach (string replay in saved5replays) {
    //     Debug.Log(replay);
    // }
    // foreach (float item in finishtimes) {
    //     Debug.Log(item);
    // }

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
                if (cp2time == -2347823) {
                    cp2time = Time.time-start;
                    Debug.Log(cp2time);
                }
            }
            if (lastcheckpoint.name == "Cp3") {
            if (cp3time == -2347823) {
                cp3time = Time.time-start;
                Debug.Log(cp3time);
            }
            }
            if (lastcheckpoint.name == "Cp4") {
            if (cp4time == -2347823) {
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
void FixedUpdate()
{   
    foreach (float item in replaysdisplayed) {
        Debug.Log(item);
    }
    frametimer+=1;
    if (replaysdisplayed.Contains(1)) {
    ghost.transform.position = new Vector3(float.Parse(replay1[(frametimer-1)*6]),float.Parse(replay1[(frametimer-1)*6+1]),float.Parse(replay1[(frametimer-1)*6+2]));
    ghost.transform.rotation = Quaternion.Euler(new Vector3( float.Parse(replay1[(frametimer-1)*6+3]) , float.Parse(replay1[(frametimer-1)*6+4]) , float.Parse(replay1[(frametimer-1)*6+5]) ) );
    }
    else {
        ghost.transform.position = new Vector3(0,-1000,0);
    }
    if (replaysdisplayed.Contains(2)) {
    ghost2.transform.position = new Vector3(float.Parse(replay2[(frametimer-1)*6]),float.Parse(replay2[(frametimer-1)*6+1]),float.Parse(replay2[(frametimer-1)*6+2]));
    ghost2.transform.rotation = Quaternion.Euler(new Vector3( float.Parse(replay2[(frametimer-1)*6+3]) , float.Parse(replay2[(frametimer-1)*6+4]) , float.Parse(replay2[(frametimer-1)*6+5]) ) );
    }
    else {
        ghost2.transform.position = new Vector3(0,-1000,0);
    }
    if (replaysdisplayed.Contains(3)) {
    ghost3.transform.position = new Vector3(float.Parse(replay3[(frametimer-1)*6]),float.Parse(replay3[(frametimer-1)*6+1]),float.Parse(replay3[(frametimer-1)*6+2]));
    ghost3.transform.rotation = Quaternion.Euler(new Vector3( float.Parse(replay3[(frametimer-1)*6+3]) , float.Parse(replay3[(frametimer-1)*6+4]) , float.Parse(replay3[(frametimer-1)*6+5]) ) );
    }
    else {
        ghost3.transform.position = new Vector3(0,-1000,0);
    }
    if (replaysdisplayed.Contains(4)) {
    ghost4.transform.position = new Vector3(float.Parse(replay4[(frametimer-1)*6]),float.Parse(replay4[(frametimer-1)*6+1]),float.Parse(replay4[(frametimer-1)*6+2]));
    ghost4.transform.rotation = Quaternion.Euler(new Vector3( float.Parse(replay4[(frametimer-1)*6+3]) , float.Parse(replay4[(frametimer-1)*6+4]) , float.Parse(replay4[(frametimer-1)*6+5]) ) );
    }
    else {
        ghost4.transform.position = new Vector3(0,-1000,0);
    }
    if (replaysdisplayed.Contains(5)) {
    ghost5.transform.position = new Vector3(float.Parse(replay5[(frametimer-1)*6]),float.Parse(replay5[(frametimer-1)*6+1]),float.Parse(replay5[(frametimer-1)*6+2]));
    ghost5.transform.rotation = Quaternion.Euler(new Vector3( float.Parse(replay5[(frametimer-1)*6+3]) , float.Parse(replay5[(frametimer-1)*6+4]) , float.Parse(replay5[(frametimer-1)*6+5]) ) );
    }
    else {
        ghost5.transform.position = new Vector3(0,-1000,0);
    }


    carSpeed = (2 * 3.14f * flc.radius * flc.rpm * 60) / 1000;
    localVelocityX = transform.InverseTransformDirection(rb.linearVelocity).x;
    localVelocityZ = transform.InverseTransformDirection(rb.linearVelocity).z;
    // replaylistlength = 0;

    string stringlist = "";
    //Debug.Log(finishtimes.Count());


    if (finishtimes.Count()<5||(Time.time-start) < finishtimes.Max()){ 
    //IF: Your time is quicker than the worst time in finishtimes OR finishtimes has less than 5 itmes

    if (finished && replaywritten == false) {
    //IF: Your replay hasn't been written yet AND you finished

    //replaymanager.Add(new List<float> {Time.time-start});

    cptext.text = cp1time.ToString() + "\n" + cp2time.ToString() + "\n" + cp3time.ToString() + "\n" + cp4time.ToString();
    replaywritten = true;
    //REPLAY HAS BEEN WRITTEN

    if (finishtimes.Count()==5){
        //IF: There are 5 times in the list
    
        string filePath = "Assets/saves.txt";
        string targetEnding = finishtimes.Max().ToString(); // Replace with the characters you're looking for

        // Read all lines into memory
        foreach (string line in File.ReadAllLines(filePath))
        {
            if (line.Split('|')[1] == targetEnding) {
                Debug.Log("hehe");
            }
            else {
                Debug.Log("ehehe");
            }
        }


        string[] lines = File.ReadAllLines(filePath);

        // Filter out lines that end with the target characters
        lines = lines.Where(line => !line.EndsWith(targetEnding)).ToArray();
        
        // Overwrite the file with the modified content
        File.WriteAllLines(filePath, lines);


    }

    List<float> stringlistlist = new List<float>();
    foreach (List<float> item in replaymanager) {
        foreach (float element in item) {
            stringlistlist.Add(element);
        }

    }
    stringlist = string.Join(", ",stringlistlist);


    if (finished) {
        using (StreamWriter sw = new StreamWriter("Assets/saves.txt",true))
        {
        sw.WriteLine(stringlist+"|"+(Math.Round(Time.time-start,4).ToString()));
        sw.Close();
        }
    }
    }
    }

    if (createreplay == true) {
        createreplay = false;
        ReplayCreation();
    }

    if (!finished) {
    replaymanager.Add(new List<float> {(float)Math.Round(transform.position.x,3),(float)Math.Round(transform.position.y,3),(float)Math.Round(transform.position.z,3),(float)Math.Round(transform.eulerAngles.x,3),(float)Math.Round(transform.eulerAngles.y,3),(float)Math.Round(transform.eulerAngles.z,3)});
    }
    // Debug.Log(replaylistlength);
    accelplay.mute = true;
    brakeplay.mute = true;
    if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        GoForward();
        if (start == -234567890) { start = Time.time; started = true; startframe = currentframe;}
        accelplay.mute = false;
    }
    if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        GoReverse();
        if (start == -234567890) { start = Time.time; started = true; startframe = currentframe;}
        brakeplay.mute = false;
    }
    if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
        TurnLeft();
        if (start == -234567890) { start = Time.time; started = true; startframe = currentframe;}
    }
    if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
        TurnRight();
        if (start == -234567890) { start = Time.time; started = true; startframe = currentframe;} 
    }

    if(Input.GetKey(KeyCode.Space)){
        CancelInvoke("DecelerateCar");
        deceleratingCar = false;
        Handbrake();
        brakeplay.mute = false;
    }

    if(Input.GetKeyUp(KeyCode.Space)){
        RecoverTraction();
    }

    if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))){
        ThrottleOff();
    }

    if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) && !Input.GetKey(KeyCode.Space) && !deceleratingCar){
        InvokeRepeating("DecelerateCar", 0f, 0.1f);
        deceleratingCar = true;
    }

    if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && steeringAxis != 0f && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
        ResetSteeringAngle();
    }
    if(Input.GetKey(KeyCode.Slash)){
        transform.position = new Vector3(532.7f,16.9f,226.9f);
        transform.rotation = Quaternion.Euler(0f,90f,0f);
        rb.linearVelocity = new Vector3(0,0,0);
        maxSpeed = 50000;
        track = "track 2";
    }

    if (Input.GetKey(KeyCode.R)) {
        ResetPosition();
        rb.linearVelocity = new Vector3(0,0,0);
        maxSpeed = 180;
        track = "track 1";
    }
    if (Input.GetKey(KeyCode.Backslash)) {
        rb.linearVelocity = new Vector3(0,0,0);
        maxSpeed = 180;
        track = "track 1";
        cplist = new ArrayList();
        start = Time.time; startframe = currentframe;
        lastcheckpoint = GameObject.Find("start_line");
        rb.transform.position = lastcheckpoint.transform.position;
        rb.transform.rotation = lastcheckpoint.transform.rotation;
        replaymanager = new List<List<float>>();
    }
    if (Input.GetKey(KeyCode.Semicolon)) {
        rb.linearVelocity = new Vector3(0,0,0);
        track = "track 3";
        maxSpeed = 50000;
        transform.rotation = stuntstartline.transform.rotation;
        transform.position = stuntstartline.transform.position;
    }

    if (finished == false) {
    timetext.text = (Mathf.Round((Time.time - start) * 100)/100).ToString();
    }


    speedtext.text = Mathf.Round(carSpeed).ToString();
    if (flc.GetGroundHit(out WheelHit hit)) {
        if (hit.collider == grass) {
            Debug.Log("On the grass!");
            maxSpeed = 180;
            rb.linearDamping = 0.015f * grassPenalty;
        }
        else if (track == "track 1") {
            maxSpeed = 180;
            rb.linearDamping = 0.015f;
        }
        else if (track == "track 2") {
            maxSpeed = 50000;
            rb.linearDamping = 0.015f;
        }
    }

    // if (replaystarted == true) {
    //     int index = (int)frametimer - (int)replayframe;
    //     Debug.Log(replaymanager[index][0]);
    //     // foreach (float replayitem in replaymanager[index]) {
        

    //     // }
    // }

    if (Input.GetKey(KeyCode.P) && replaystarted == false && finished == true) {
        replaystarted = true;
        replaytime = Time.time;
        replayframe = frametimer;
    }
    
    if (finished == true && replaystarted == true) {
        replaystarted=true;
        int index = (int)frametimer - (int)replayframe;
        transform.position = new Vector3(replaymanager[index][0],replaymanager[index][1],replaymanager[index][2]);
        transform.rotation = Quaternion.Euler(new Vector3(replaymanager[index][3],replaymanager[index][4],replaymanager[index][5]));
    }

    if (Input.GetKey(KeyCode.Alpha1)) {
        if (replaysdisplayed.Contains(1)) {
            replaysdisplayed.Remove(1);
        }
        else {
            replaysdisplayed.Add(1);
        }
    }

    if (Input.GetKey(KeyCode.Alpha2)) {
        if (replaysdisplayed.Contains(2)) {
            replaysdisplayed.Remove(2);
        }
        else {
            replaysdisplayed.Add(2);
        }
    }
    if (Input.GetKey(KeyCode.Alpha3)) {
        if (replaysdisplayed.Contains(3)) {
            replaysdisplayed.Remove(3);
        }
        else {
            replaysdisplayed.Add(3);
        }
    }
    if (Input.GetKey(KeyCode.Alpha4)) {
        if (replaysdisplayed.Contains(4)) {
            replaysdisplayed.Remove(4);
        }
        else {
            replaysdisplayed.Add(4);
        }
    }
    if (Input.GetKey(KeyCode.Alpha5)) {
        if (replaysdisplayed.Contains(5)) {
            replaysdisplayed.Remove(5);
        }
        else {
            replaysdisplayed.Add(5);
        }
    }


    AnimateWheelMesh();
    currentframe += 1;

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

public void AnimateWheelMesh(){
    FLMesh.transform.rotation = Quaternion.Euler(new Vector3(FLMesh.transform.rotation.x,90,FLMesh.transform.rotation.z));
    FRMesh.transform.rotation = Quaternion.Euler(new Vector3(FRMesh.transform.rotation.x,90,FRMesh.transform.rotation.z));
    
    Quaternion FLRot;
    Vector3 FLPos;
    flc.GetWorldPose(out FLPos, out FLRot);
    FLMesh.transform.rotation = FLRot;
    FLMesh.transform.position = FLPos;
    FLTread.transform.rotation = FLRot;
    FLTread.transform.position = FLPos;

    // FLMesh.transform.position = FLPos;
    FLMesh.transform.Rotate(new Vector3(0,0,90));
    FLTread.transform.Rotate(new Vector3(0,0,90));
    frc.GetWorldPose(out FLPos, out FLRot);
    // FRMesh.transform.rotation = FLRot;
    FRMesh.transform.rotation = FLRot;
    FRMesh.transform.position = FLPos;
    FRTread.transform.rotation = FLRot;
    FRTread.transform.position = FLPos;
    // FRMesh.transform.position = FLPos;
    FRMesh.transform.Rotate(new Vector3(0,0,90));
    FRTread.transform.Rotate(new Vector3(0,0,90));


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
    if (lastcheckpoint == GameObject.Find("start_line")) {
    rb.transform.position = lastcheckpoint.transform.position;
    rb.transform.rotation = lastcheckpoint.transform.rotation;
    }
    else {rb.transform.position = lastcheckpoint.transform.position + new Vector3(0,respawnoffset,0);
    rb.transform.rotation = lastcheckpoint.transform.rotation;}
    
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

public void ReplayCreation() 
    {
        string path = "Assets/saves.txt";
        StreamReader reader = new StreamReader(path); 
        string savefilestring = reader.ReadToEnd();
        Debug.Log(savefilestring);
        reader.Close();
    
    }

}
