using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gifPlayer : MonoBehaviour
{
    [SerializeField]
    RawImage image;

    public string Filename;

    public bool repeat = false;
    public bool autoPlay = false;
    private List<Texture2D> mFrames = new List<Texture2D>();
    private List<float> mFrameDelay = new List<float>();


    private int mCurFrame = 0;
    private float mTime = 0.0f;
    public bool isPlaying = false;
    public bool isFinished = false;

    void Start()
    {
        if (string.IsNullOrWhiteSpace(Filename))
        {
            return;
        }

        BetterStreamingAssets.Initialize();
        var bytes = BetterStreamingAssets.ReadAllBytes(Filename);
        using (var decoder = new MG.GIF.Decoder(bytes))
        {
            var img = decoder.NextImage();

            while (img != null)
            {
                mFrames.Add(img.CreateTexture());
                mFrameDelay.Add(img.Delay / 1000.0f);
                img = decoder.NextImage();
            }
        }

        image.texture = mFrames[0];

        if (autoPlay) Play();
    }
    void Update()
    {
        if (!isPlaying) return;
        if (mFrames == null)
        {
            return;
        }

        if(mCurFrame >= mFrames.Count -1 && !repeat)
        {
            isFinished = true;
            return;
        }

        mTime += Time.deltaTime;

        if (mTime >= mFrameDelay[mCurFrame])
        {
            mCurFrame = (mCurFrame + 1);
            mTime = 0.0f;

            image.texture = mFrames[mCurFrame];
        }
        if (mCurFrame >= mFrames.Count -1 && repeat)
        {
            mCurFrame = 0;
        }
    }
    [ContextMenu("Play")]
    public void Play()
    {
        isPlaying = true;
    }
}
