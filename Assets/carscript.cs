using UnityEngine;



public class carscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Velocity = new Vector3(0,0,0);
    private float interval = 0;
    public Vector3 savedvector = new Vector3(0,0,0);
    void Start()
    {
        
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Cones") {
            Velocity = new Vector3(0,0,0);

            Debug.Log("hi");

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Velocity;
        
        if (Input.GetKey(KeyCode.W)) {
            Velocity = Velocity + Vector3.Scale(transform.forward, new Vector3((float)0.001,(float)0.001,(float)0.001));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Rotate(0,-1,0);
        }
        if(Input.GetKey(KeyCode.D)) {
            transform.Rotate(0,1,0);
        }
        if (Input.GetKey(KeyCode.S)) {
            Debug.Log(Velocity);
            Velocity = Velocity + Vector3.Scale(transform.forward, new Vector3((float)-0.001,(float)-0.001,(float)-0.001));
        }

        if (Input.GetKey(KeyCode.W)) {Velocity = Vector3.Scale(Velocity, new Vector3((float)0.999,1,(float)0.999));}
        else{ Velocity = Vector3.Scale(Velocity, new Vector3((float)0.992,1,(float)0.992));}
       
        if (interval > 1) {
            interval = 0;
            savedvector = transform.position;
        }
        interval += Time.deltaTime;
        
        // if (Velocity.x < (float)0.00001) {
        //     Velocity = Vector3.Scale(Velocity, new Vector3(0,1,1));
        // }
        // if (Velocity.z < (float)0.00001) {
        //     Velocity = Vector3.Scale(Velocity, new Vector3(1,1,0));
        // }
       // Debug.Log(transform.forward);
    }

    

}
