using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Vector3 rotation;

    void Start()
    {
        //Carscript carscript = Game
        transform.rotation = Quaternion.Euler(0, 90, 90);
        rotation = Vector3.zero;
    }

    void Update()
    {
        rotation.x += 1;
        transform.Rotate(rotation);
    }
}
