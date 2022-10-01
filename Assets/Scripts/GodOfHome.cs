using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodOfHome : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("pushed");
        SceneManager.LoadScene("Before");
    }
}
