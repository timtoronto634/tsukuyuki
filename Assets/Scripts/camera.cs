using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        float fov = Mathf.Min(120, 60 + target.localScale.x * 10);
        GetComponent<Camera>().fieldOfView = fov;
    }
}
