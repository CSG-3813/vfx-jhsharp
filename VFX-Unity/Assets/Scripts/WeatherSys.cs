/***
 * Author: Jacob Sharp
 * Created: 11/2/22
 * Modified:
 * Description: Controls weather effects
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeatherSys : MonoBehaviour
{
    public GameObject rainObj;
    ParticleSystem rainPS;

    public float rainTime = 10;
    float rainTimer;
    bool startTime;

    public AudioMixerSnapshot rainMixer;
    public AudioMixerSnapshot sunnyMixer;
    AudioSource audioSrc;

    bool isRaining;
    public bool IsRaining { get { return isRaining; } }

    public Volume rainVolume;
    float lerpValue;
    float lerpDuration = 10;
    float transitionTime;

    private void Start()
    {
        rainPS = rainObj.GetComponent<ParticleSystem>();
        audioSrc = rainObj.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (startTime)
        {
            if (rainTimer > 0)
            {
                rainTimer -= Time.deltaTime;
                TintSky();
            }
            else
            {
                EndRain();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered rain");

        if (other.tag == "Player")
        {
            if (!startTime)
            {
                rainTimer = rainTime;
                startTime = true;
                isRaining = true;
                rainPS.Play();
                audioSrc.Play();
                rainMixer.TransitionTo(2.0f);
            }
        }
    }

    void EndRain()
    {
        startTime = false;
        isRaining = false;
        rainPS.Stop();
        audioSrc.Stop();
        sunnyMixer.TransitionTo(2.0f);
    }

    void TintSky()
    {
        if (transitionTime < lerpDuration)
        {
            lerpValue = Mathf.Lerp(0, 1, transitionTime/lerpDuration);
            transitionTime += Time.deltaTime;
            rainVolume.weight = lerpValue;
        }
    }
}
