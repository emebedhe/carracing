using UnityEngine;



public class carscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Velocity = new Vector3(0,0,0);
    private float interval = 0;
    public Vector3 savedvector = new Vector3(0,0,0);
    public float torque;
    
    Rigidbody rb;     

    public float m_Thrust = 20f;
    // void Start()
    // // {
    // //     rb = GetComponent<Rigidbody>();
        
    // // }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Cones") {
            Velocity = new Vector3(0,0,0);

            Debug.Log("hi");

        }
    }



    

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
    }
    //fixedupdate ig
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * m_Thrust);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-transform.forward * m_Thrust);
        }
        
        if (Input.GetKey(KeyCode.A)) {
       //     rb.AddTorque(transform.up * torque * horizontal);
            rb.AddTorque(new Vector3(0,-torque,0));
        }

        if (Input.GetKey(KeyCode.D)) {
         //   rb.AddTorque(-transform.up* torque* horizontal);
         rb.AddTorque(new Vector3(0,torque,0));

            Debug.Log(transform.up*-torque*horizontal);
            Debug.Log(transform.up * torque * horizontal);
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

    void Accelerate() {
        rb.AddForce(transform.up * m_Thrust);
    }

    void Brake() {
        rb.AddForce(rb.transform.forward * m_Thrust * -1);
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
