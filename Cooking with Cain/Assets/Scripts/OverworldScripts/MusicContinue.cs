using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicContinue : MonoBehaviour
{
    static List<MusicStopPoint> musicStopPoints = new List<MusicStopPoint>();
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        MusicStopPoint stopPoint = musicStopPoints.Find(msp => msp.audio == audioSource.clip);
        if (stopPoint != null)
        {
            audioSource.timeSamples = stopPoint.timeSamples;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MusicStopPoint stopPoint = musicStopPoints.Find(msp => msp.audio == audioSource.clip);

        if (stopPoint == null)
        {
            musicStopPoints.Add(new MusicStopPoint(audioSource.clip, audioSource.timeSamples));
        }
        else
        {
            stopPoint.timeSamples = audioSource.timeSamples;
        }
    }
}

public class MusicStopPoint
{
    public AudioClip audio;
    public int timeSamples;

    public MusicStopPoint(AudioClip audioSource, int ts)
    {
        audio = audioSource;
        timeSamples = ts;
    }
}