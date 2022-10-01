using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodOfBefore : MonoBehaviour
{
    public enum State {
        init,
        hyokkori,
        countdown
    }
    public State state;

    public GameObject hyokkoriImage;
    public GameObject hukidashi;
    public GameObject tsukutte;

    public GameObject countdownGif;
    public GameObject countdownAudio;
    // Start is called before the first frame update
    void Start()
    {
        state = State.init;
        played = false;
    }
    private bool played;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.init:
                state = State.hyokkori;
                break;
            case State.hyokkori:
                if (!played && !tsukutte.GetComponent<AudioSource>().isPlaying) {
                    hyokkoriImage.SetActive(true);
                    hukidashi.SetActive(true);
                    tsukutte.SetActive(true);
                    played = true;
                } else if (played &&  !tsukutte.GetComponent<AudioSource>().isPlaying) {
                    state = State.countdown;
                    hyokkoriImage.SetActive(false);
                    hukidashi.SetActive(false);
                }
                break;
            case State.countdown:
                countdownGif.SetActive(true);
                countdownGif.GetComponent<gifPlayer>().Play();
                countdownAudio.SetActive(true);
                break;
            default:
                break;
        }
    }
}
