using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections;


public class carscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button Q;

    //test variables


    public Button E;
    private bool qToUpshift;
    private bool eToUpshift;
    private Vector3 Velocity = new Vector3(0,0,0);
    private int gear = 1;
    private bool canstartdriving = false;

    private float start = -2347823;
    private float torque = 1000;

    private float time;

    private bool finished = false;
    public Text gearCounter;
    public Text speedText;
    public Text cpText;
    public Text timer;
    public Text finalcptime;
    public float cp1time;

    private bool canStart = false;

    public GameObject lastcheckpoint;
    

    private float Speed = 0;
    private float gravity = -20;

    private bool airborne;
    
    private ArrayList cplist = new ArrayList();
    
    Rigidbody rb;     

    private float Thrust = 200f;
    private float BrakeThrust = 50f; 

    private Vector3 CurrentPos = new Vector3(0,0,0);
    private Vector3 PreviousPos = new Vector3(0,0,0);
    // void Start()
    // // {
    // //     rb = GetComponent<Rigidbody>();
        
    // // }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Cones") {
            Velocity = new Vector3(0,0,0);

            Debug.Log("hi");

        }
       // Debug.Log("leo stop simping for elina");

        airborne = false;

        

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Checkpoint") {
            if (cplist.Contains(1)) {}
            else { cplist.Add(1);}
            lastcheckpoint = other.gameObject;
            if (cp1time == -2347823) {
                cp1time = Time.time-start;
                Debug.Log(cp1time);
            }
        }

        if (other.gameObject.tag == "Finishline") {
            if (cplist.Count == 1) {
                finished = true;
            }
        }
    }



    

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        timer.text = "0.0";
        lastcheckpoint = GameObject.Find("start_line");

        Button qUpshift = Q.GetComponent<Button>();
        Button eUpshift = E.GetComponent<Button>();
        qUpshift.onClick.AddListener(TaskOnClick1);
        eUpshift.onClick.AddListener(TaskOnClick2);

    }
    //fixedupdate ig
    void FixedUpdate()
    {
        if (canstartdriving == true) {
        if (start == -2347823 && Input.anyKey) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            start = Time.time;
            }
        }
        gearCounter.color = Color.red;
        if (airborne == false) {
            if ((Speed == 0) || (Mathf.Ceil(Speed * 100 / 80) <= gear && Speed*100 >= (gear-1) * 70)) {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                rb.AddForce(Vector3.Scale(transform.forward, new Vector3(1,0,1)) * Thrust);
                gearCounter.color = Color.green;
                }
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                rb.AddForce(-Vector3.Scale(transform.forward, new Vector3(1,0,1)) * BrakeThrust);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
        //     rb.AddTorque(transform.up * torque * horizontal);
                rb.AddTorque(new Vector3(0,-torque,0));
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            //   rb.AddTorque(-transform.up* torque* horizontal);
                rb.AddTorque(new Vector3(0,torque,0));
            }
                rb.AddForce(new Vector3(0,-20,0));
            }

        if (Input.GetKey(KeyCode.R)) {
            // transform.position = new Vector3(1887,55,6407);
            // transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            transform.position = lastcheckpoint.transform.position + new Vector3(-15,0,-75);
            transform.rotation = lastcheckpoint.transform.rotation * Quaternion.Euler(new Vector3(0,182,0));
            rb.linearVelocity = new Vector3(0,0,0);
        }


        if (airborne == true) {
            rb.AddForce(new Vector3(0,gravity,0));
        }
        
        CurrentPos = transform.position;

        Velocity.x = Mathf.Abs(CurrentPos.x - PreviousPos.x);
        Velocity.y = Mathf.Abs(CurrentPos.y - PreviousPos.y);
        Velocity.z = Mathf.Abs(CurrentPos.z - PreviousPos.z);

        Speed = Mathf.Sqrt(Mathf.Sqrt(Velocity.x*Velocity.x + Velocity.y*Velocity.y) + Velocity.z * Velocity.z);

        airborne = true;    

        PreviousPos = CurrentPos;

        speedText.text = "Speed: "+Mathf.Round(Speed*100).ToString();

        if (cplist.Count == 1) {
            cpText.text = "Checkpoint 1/1";
        }
        else if (cplist.Count == 0) {
            cpText.text = "Checkpoint 0/1";
        }
        else {
            cpText.text = "Checkpoint 0/1";
        }


        if (finished == true) {
            finalcptime.text = "Checkpoint 1: " + Math.Round(cp1time,2).ToString();
            Debug.Log(cp1time);
        }
        else if (finished == false) {
            time = Time.time;
        }
        if (start != -2347823) {
        timer.text = "Time Elapsed: "+Math.Round(time-start,2).ToString();
        }

        gearCounter.text = "Current Gear: "+gear.ToString();
        }


    //     floa t horizontal = Input.GetAxis("Horizontal");
    //     if (Input.GetKey(KeyCode.W)) {
    //         rb.AddForce(Vector3.Scale(transform.forward, new Vector3(1,0,1)) * m_Thrust);
    //     }
    //     if (Input.GetKey(KeyCode.S)) {
    //         rb.AddForce(-Vector3.Scale(transform.forward, new Vector3(1,0,1)) * m_Thrust);
    //     }
        
    //     if (Input.GetKey(KeyCode.A)) {
    //    //     rb.AddTorque(transform.up * torque * horizontal);
    //         rb.AddTorque(new Vector3(0,-torque,0));
    //     }

    //     if (Input.GetKey(KeyCode.D)) {
    //      //   rb.AddTorque(-transform.up* torque* horizontal);
    //      rb.AddTorque(new Vector3(0,torque,0));
    //     }
    }

    void TaskOnClick1(){
        Q.onClick.RemoveListener(TaskOnClick1);
        Q.gameObject.SetActive(false);
        E.onClick.RemoveListener(TaskOnClick1);
        E.gameObject.SetActive(false);
        canstartdriving=true;
        qToUpshift = true;
        eToUpshift = false;
    }

    void TaskOnClick2(){
        Q.onClick.RemoveListener(TaskOnClick1);
        Q.gameObject.SetActive(false);
        E.onClick.RemoveListener(TaskOnClick1);
        E.gameObject.SetActive(false);
        canstartdriving=true;
        eToUpshift = true;
        qToUpshift = false;
    }

    void Update() {
        if ((Mathf.Ceil(Speed*100 + 10)/80) > gear) {
            speedText.color = Color.green;
        }
        else { speedText.color = Color.red;}
        if (Input.GetKeyDown(KeyCode.E)) {
      //      Debug.Log(Mathf.Ceil(Speed*100 + 10)/80); //the + 10 is a buffer so you don't have to go EXACTLY 80
    //        if ((Mathf.Ceil(Speed*100 + 10)/80) > gear) {

                if (eToUpshift == true) {
                if (gear != 6) { 
                gear += 1;
                }
                }
                else { if (gear > 1) {gear -=1;}}
            }
     //   }
        if (Input.GetKeyDown(KeyCode.Q)) {
         
                if (qToUpshift == true) {
                if (gear != 6) { 
                gear += 1;
                //}
                }
                else { if (gear > 1) {gear -=1;}}
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            rb.MoveRotation(Quaternion.Euler(new Vector3(0,rb.rotation.eulerAngles.y,0))); //flips car over
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {

    //     float horizontal = Input.GetAxis("Horizontal");
    //     float vertical = Input.GetAxis("Vertical");

    // //    Accelerate();

    //     //character movement
    //     // rb.MovePosition(new Vector3(transform.position.x+Velocity.x,transform.position.y+Velocity.y,transform.position.z+Velocity.z));
    //     // if (Input.GetKey(KeyCode.W)) {
    //     //     Velocity = Velocity + Vector3.Scale(transform.forward, new Vector3((float)0.001,(float)0.001,(float)0.001));
    //     // }
    //     // if(Input.GetKey(KeyCode.A)) {
    //     //     rb.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y-1,transform.rotation.z);
    //     // }
    //     // if(Input.GetKey(KeyCode.D)) {
    //     //     rb.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y+1,transform.rotation.z);
    //     // }
    //     // if (Input.GetKey(KeyCode.S)) {
    //     //   // Debug.Log(Velocity);
    //     //     Velocity = Velocity + Vector3.Scale(transform.forward, new Vector3((float)-0.001,(float)-0.001,(float)-0.001));
    //     // }

        

    //     if (Input.GetKey(KeyCode.W)) {
    //         Accelerate();
    //     }
        
    //     if (Input.GetKey(KeyCode.S)) {
    //         Brake();
    //     }

    //     if (Input.GetKey(KeyCode.A)) {
    //         Steer();
    //     }



    //     // if (Input.GetKey(KeyCode.W)) {Velocity = Vector3.Scale(Velocity, new Vector3((float)0.999,(float)0.999,(float)0.999));}
    //     // else{ Velocity = Vector3.Scale(Velocity, new Vector3((float)0.992,1,(float)0.992));}
        
    //     // if (Velocity.x < (float)0.000001 && Velocity.x > (float)-0.000001 ) {
    //     //     Velocity = Vector3.Scale(Velocity, new Vector3(0,1,1));
    //     // }
    //     // if (Velocity.z < (float)0.000001 && Velocity.z > (float)-0.000001) {
    //     //     Velocity = Vector3.Scale(Velocity, new Vector3(1,1,0));
    //     // }
    //     // if (Velocity.y < (float)0.000001 && Velocity.y > (float)-0.000001) {
    //     //     Velocity = Vector3.Scale(Velocity, new Vector3(1,0,1));
    //     // }
        

    //    //Debug.Log(transform.forward);
    // }


    // public void GetKeyPress(Vector3 inputVector) {
    //     inputVector.normalize();

    //     input = inputVector; 
    // }


}
