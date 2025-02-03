using UnityEngine;
using UnityEngine.UI;



public class carscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Velocity = new Vector3(0,0,0);
    public float torque;

    public Text speedText;

    public float Speed = 0;
    public float gravity;

    private bool airborne;
    
    
    Rigidbody rb;     

    public float Thrust = 20f;
    public float BrakeThrust = 40f; 

    public Vector3 CurrentPos = new Vector3(0,0,0);
    public Vector3 PreviousPos = new Vector3(0,0,0);
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

    void OnColliderStay(Collider other) {
        
    }



    

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
    }
    //fixedupdate ig
    void FixedUpdate()
    {
        if (airborne == false) {
            if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.Scale(transform.forward, new Vector3(1,0,1)) * Thrust);
        }
        if (Input.GetKey(KeyCode.S)) {

            rb.AddForce(-Vector3.Scale(transform.forward, new Vector3(1,0,1)) * BrakeThrust);
        }
        
        if (Input.GetKey(KeyCode.A)) {
       //     rb.AddTorque(transform.up * torque * horizontal);
            rb.AddTorque(new Vector3(0,-torque,0));
        }

        if (Input.GetKey(KeyCode.D)) {
         //   rb.AddTorque(-transform.up* torque* horizontal);
            rb.AddTorque(new Vector3(0,torque,0));
        }
            rb.AddForce(new Vector3(0,-20,0));
        }

        if (Input.GetKey(KeyCode.R)) {
            transform.position = new Vector3(1887,55,6407);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }


        if (airborne == true) {
            rb.AddForce(new Vector3(0,gravity,0));
        }
        
        CurrentPos = transform.position;

        Velocity.x = Mathf.Abs(CurrentPos.x - PreviousPos.x);
        Velocity.y = Mathf.Abs(CurrentPos.y - PreviousPos.y);
        Velocity.z = Mathf.Abs(CurrentPos.z - PreviousPos.z);

        Debug.Log(Speed);
        Speed = Mathf.Sqrt(Mathf.Sqrt(Velocity.x*Velocity.x + Velocity.y*Velocity.y) + Velocity.z * Velocity.z);

        airborne = true;    

        PreviousPos = CurrentPos;

        speedText.text = Mathf.Round(Speed*100).ToString();
        


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

    void Accelerate() {
        rb.AddForce(transform.up * Thrust);
    }

    void Brake() {
        rb.AddForce(rb.transform.forward * Thrust * -1);
    }

    void Steer() {
        float horizontal = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * torque * horizontal);
    }

    // public void GetKeyPress(Vector3 inputVector) {
    //     inputVector.normalize();

    //     input = inputVector; 
    // }


}
