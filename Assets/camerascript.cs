using UnityEngine;



public class camerascript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public Vector3 offset;

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,10,0) + Vector3.Scale(player.transform.forward, new Vector3(-55,-55,-55));
      
        transform.rotation = player.transform.rotation;
        //transform.Rotate(20,0,0);
    }

}
