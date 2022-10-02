using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodOfInGame : MonoBehaviour
{
    public GameObject owari;
    private float timepast;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        timepast = 0;
        finished = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        timepast = timepast + Time.deltaTime;
        if (!finished && timepast > 15) {
            owari.SetActive(true);
            finished = true;
        }
        if (finished && timepast > 16) {
            SceneManager.LoadScene("Result");
        }
    }
}
