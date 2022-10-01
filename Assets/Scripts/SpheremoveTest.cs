using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheremoveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axis = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.GetComponent<Rigidbody>().AddForce(axis.x, 0, axis.y);
    }
}
