using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodOfResult : MonoBehaviour
{
    public enum State {
        init,
        drumrole,
        resultfailed,
        finished
    }
    public State state;

    public GameObject drumroleImage;
    public AudioSource drumroleSound;

    public GameObject resultFailedImage;
    public AudioSource resultFailedSound;

    // Start is called before the first frame update
    void Start()
    {
        state = State.init;
    }

    void Update()
    {
        switch (state)
        {
            case State.init:
                drumroleImage.SetActive(false);
                resultFailedImage.SetActive(false);
                state = State.drumrole;
                break;
            case State.drumrole:
                if (!drumroleImage.GetComponent<gifPlayer>().isPlaying) {
                    drumroleImage.SetActive(true);
                    drumroleSound.Play();
                }
                if (drumroleImage.GetComponent<gifPlayer>().isFinished)
                {
                    if (FadeOut(drumroleSound))
                    {
                        drumroleImage.SetActive(false);
                        state = State.resultfailed;
                    }
                        
                }
                
                break;
            case State.resultfailed:
                if (!resultFailedImage.GetComponent<gifPlayer>().isPlaying)
                {
                    resultFailedImage.SetActive(true);
                    resultFailedSound.Play();
                }
                if (resultFailedImage.GetComponent<gifPlayer>().isFinished)
                {
                    if (FadeOut(resultFailedSound))
                    {
                        resultFailedImage.SetActive(false);
                        state = State.finished;
                    }
                }
                break;
            default:
                break;
        }
    }
    bool FadeOut(AudioSource audio)
    {
        if(audio.volume > 0)
        {
            audio.volume -= Time.deltaTime * 2;
            return false;
        }
        else
        {
            audio.Stop();
            return true;
        }
    }
}
