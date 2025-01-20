using UnityEngine;



public class carscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Velocity = new Vector3(0,0,0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Velocity;
        if (Input.GetKey(KeyCode.W)) {
            Debug.Log(Velocity);
            Velocity = Velocity + new Vector3(0,0,(float)0.001);
            
        }
        if(Input.GetKey(KeyCode.A)) {
            Debug.Log(transform.rotation);
            transform.Rotate(0,-1,0);
        }
        if(Input.GetKey(KeyCode.D)) {
            Debug.Log(transform.rotation);
            transform.Rotate(0,1,0);
        }
        if (Input.GetKey(KeyCode.S)) {
            Debug.Log(Velocity);
            Velocity = Velocity + new Vector3(0,0,(float)-0.001);
        }
        Velocity = Vector3.Scale(Velocity, new Vector3(1,1,(float)0.999));
    }
}
