using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    [SerializeField] private AudioSource bg_Music;
    float fadeVolume = .05f;
    float baseVolume;
    float fadeTimer = -1;
    bool fading = false;
    float increment;

    private void Awake()
    {
        if (sm == null)
            sm = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (bg_Music != null && fadeTimer >= 0)
        {
            if (fadeTimer >= 7)
                StartCoroutine(FadeOut(1.75f));
            else
                fadeTimer += Time.deltaTime;
        }

        if (fading && bg_Music.volume < baseVolume)
        {
            bg_Music.volume += increment * Time.deltaTime;
        }
    }

    public void FadeIn()
    {
        baseVolume = bg_Music.volume;
        bg_Music.volume = fadeVolume;
        fadeTimer = 0;
    }

    private IEnumerator FadeOut(float t)
    {
        Debug.Log("Fading Out.");
        increment = (baseVolume - bg_Music.volume) / t;
        fadeTimer = -1;
        fading = true;

        Heart.Heartbeat.Init();

        Debug.Log("! Faded.");
        yield return new WaitForSeconds(t);
        fading = false;
    }
}
