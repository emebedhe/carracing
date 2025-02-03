using UnityEngine;



public class camerascript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public Vector3 offset;
    public Vector3 rotationoffset;

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,30,0) + Vector3.Scale(player.transform.forward, new Vector3(-85,-85,-85)) + offset; //+ new Vector3(10,5,-5);
      
        transform.rotation = player.transform.rotation;
        // transform.rotation = Quaternion.Euler(0,0,0);
        // transform.Rotate(rotationoffset);
      //  Debug.Log(transform.position.x-player.transform.position.x);
      //  Debug.Log(transform.position.z-player.transform.position.z); 
    
        //transform.Rotate(20,0,0);
    }

   

}
