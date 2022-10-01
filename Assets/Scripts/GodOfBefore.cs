using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodOfBefore : MonoBehaviour
{
    public gifPlayer image;
    
    public AudioSource tsukutte;
    public AudioSource countdown;
    // Start is called before the first frame update
    void Start()
    {
        tsukutte.Play();
        image.Play();
        countdown.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
