using UnityEngine;

public class ghostscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        // this.GetComponent<Renderer> ().material.color.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        // this.GetComponent<Renderer> ().material.color.a = 0.5f;
    }
}
