using UnityEngine;
using UnityEngine.UI;


public class camerascript : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public GameObject player1;
  public Transform player;
  public Vector3 offset;
  public Vector3 rotationoffset;
  public Rigidbody carbody;
  private Vector3 cartravelnormalised;
  private int cameraa = 1;
  private string track = "";

  public GameObject button1;
  public GameObject button2;

  public float lllll = -85f;

  buttonscript buttonscript2;
  buttonscript buttonscript1;

  public GameObject titletext;

  void Start()
  {

    buttonscript1 = button1.GetComponent<buttonscript>();
    buttonscript2 = button2.GetComponent<buttonscript>();

    cartravelnormalised = carbody.linearVelocity.normalized;
    transform.position = player.transform.position + new Vector3(0, 30, 0) + Vector3.Scale(cartravelnormalised, new Vector3(lllll, lllll, lllll)) + offset; //+ new Vector3(10,5,-5);
    transform.LookAt(player);
    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
    obj.transform.position = new Vector3(10000, 1000, 0);
    Material mat = new Material(Shader.Find("Unlit/Color"));
    mat.color = new Color(22 / 255f, 82 / 255f, 41 / 255f);
    obj.GetComponent<Renderer>().material = mat;
    obj.name = "obj";
  }

  // Update is called once per frame
  void Update()
  {
    if (player1.GetComponent<carscript>().track != "")
    {

      // transform.rotation = player.transform.rotation;
      // transform.rotation = Quaternion.Euler(0,0,0);
      transform.Rotate(rotationoffset);
      if (Input.GetKeyDown(KeyCode.C))
      {
        if (cameraa == 1)
        {
          cameraa = 2;
          offset = new Vector3(0, 2, 0);
        }
        else if (cameraa == 2)
        {
          cameraa = 1;
          offset = new Vector3(0, -25, 0);
        }
        // cameraa = 1;
        // offset = new Vector3(0,-25,0);
        // cameraa = 2;
        // offset = new Vector3(0,2,0);
      }
      if (cameraa == 1)
      {
        cartravelnormalised = carbody.linearVelocity.normalized;
        if (transform.InverseTransformDirection(carbody.linearVelocity).z >= 0.1f || transform.InverseTransformDirection(carbody.linearVelocity).z <= -0.1f)
        {// || player1.GetComponent<carscript>().replaystarted) {
          transform.position = player.transform.position + new Vector3(0, 30, 0) + Vector3.Scale(cartravelnormalised, new Vector3(lllll, lllll, lllll)) + offset; //+ new Vector3(10,5,-5);
          transform.LookAt(player);

        }
        else
        {
          transform.position = player.transform.position + new Vector3(0, 30, 0) + Vector3.Scale(player.transform.forward, new Vector3(lllll, lllll, lllll)) + offset;
          transform.LookAt(player);
        }
        //Debug.Log(transform.InverseTransformDirection(carbody.linearVelocity).z);
        // This if block detects whether or not the tab key is pressed. If so, it automatically looks from the front.
        if (Input.GetKey(KeyCode.Tab))
        {
          transform.position = Vector3.Scale(player.transform.position, new Vector3(1f, 1f, 1f)) + new Vector3(0, 30, 0) + Vector3.Scale(player.transform.forward, new Vector3(-lllll, -lllll, -lllll)) + offset;
          transform.LookAt(player);
          //Debug.Log("Original x");
          //Debug.Log(player.transform.forward.x);
          //Debug.Log("Changed x");
          //Debug.Log(Mathf.Abs(player.transform.forward.x)*-1);
        }
        try
        {
          if (player1.GetComponent<carscript>().replaystarted && !Input.GetKey(KeyCode.Tab))
          {
            transform.position = Vector3.Scale(player.transform.position, new Vector3(1f, 1f, 1f)) + new Vector3(0, 30, 0) + Vector3.Scale(player.transform.forward, new Vector3(lllll, lllll, lllll)) + offset;
            transform.LookAt(player);
          }
        }
        catch
        {
          transform.position = Vector3.Scale(player.transform.position, new Vector3(1f, 1f, 1f)) + new Vector3(0, 30, 0) + Vector3.Scale(player.transform.forward, new Vector3(lllll, lllll, lllll)) + offset;
          transform.LookAt(player);
        }

      }
      else
      {
        // transform.position = player.transform.position+offset+player.transform.forward.normalized;
        // transform.rotation = player.transform.rotation;
        // transform.Rotate(rotationoffset);
        transform.localPosition = new Vector3(0, 2, 1);
        transform.rotation = player.transform.rotation;

      }
      //  Debug.Log(transform.position.x-player.transform.position.x);
      //  Debug.Log(transform.position.z-player.transform.position.z); 

      //transform.Rotate(20,0,0);
    }

    else if (buttonscript1.voids == "track1")
    {

      LeoOnHover();
      titletext.gameObject.SetActive(false);
    }
    else if (buttonscript2.voids == "track2")
    {
      TobesOnHover();
      titletext.gameObject.SetActive(false);
    }

    else
    {
      transform.position = new Vector3(10000, 1001, 0);
      transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
      titletext.gameObject.SetActive(true);
    }
  }

  public void LeoOnHover()
  {
    transform.position = new Vector3(1955.995f, 5427.038f,1567.463f);
    track = "none";
  }

  public void TobesOnHover()
  {
    track = "none";
    transform.position = new Vector3(0f, 1926.731f, 0f);
    transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
  }

}
