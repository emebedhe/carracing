using UnityEngine;



public class camerascript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public Vector3 offset;
    public Vector3 rotationoffset;
    public Rigidbody carbody;
    private Vector3 cartravelnormalised;
    private int cameraa = 1;

    public float lllll = -85f;
    
    
    void Start()
    {
        cartravelnormalised = carbody.linearVelocity.normalized;
        transform.position = player.transform.position + new Vector3(0,30,0) + Vector3.Scale(cartravelnormalised, new Vector3(lllll,lllll,lllll)) + offset; //+ new Vector3(10,5,-5);
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {

        // transform.rotation = player.transform.rotation;
        // transform.rotation = Quaternion.Euler(0,0,0);
        transform.Rotate(rotationoffset);
        if (Input.GetKey(KeyCode.Alpha1)) {
          cameraa = 1;
          offset = new Vector3(0,-25,0);
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
          cameraa = 2;
          offset = new Vector3(0,2,0);
        }
        if (cameraa == 1) {
          cartravelnormalised = carbody.linearVelocity.normalized;
          if (transform.InverseTransformDirection(carbody.linearVelocity).z >= 0.1f || transform.InverseTransformDirection(carbody.linearVelocity).z <= -0.1f) {
          transform.position = player.transform.position + new Vector3(0,30,0) + Vector3.Scale(cartravelnormalised, new Vector3(lllll,lllll,lllll)) + offset; //+ new Vector3(10,5,-5);
          transform.LookAt(player);

          }
          else {
            transform.position = player.transform.position + new Vector3(0,30,0) + Vector3.Scale(player.transform.forward, new Vector3(lllll,lllll,lllll)) + offset;
            transform.LookAt(player);
          }
          Debug.Log(transform.InverseTransformDirection(carbody.linearVelocity).z);
        } else {
          transform.position = player.transform.position+offset+player.transform.forward.normalized;
          transform.rotation = player.transform.rotation;
          transform.Rotate(rotationoffset);

        }
      //  Debug.Log(transform.position.x-player.transform.position.x);
      //  Debug.Log(transform.position.z-player.transform.position.z); 
    
        //transform.Rotate(20,0,0);
    }
//hi
   

}
