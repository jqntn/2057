using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource[] sources;
    public bool playOnce;
    private bool triggered = false;
    [Header("Reset")]
    public bool resetOnTrigger = false;
    public SoundTrigger[] resets;
    public float resetDelay;
    private float timer;
    [Header("Limit")]
    public int limitToPart;
    LevelManager lm;
    [Header("Fade In/Out")]
    public bool cinematicFade;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        if (limitToPart == 0)
            limitToPart = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && resetOnTrigger)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                foreach (SoundTrigger trigger in resets)
                {
                    trigger.Reset();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && lm.curPart < limitToPart)
        {
            if(!triggered)
            {
                foreach(AudioSource source in sources)
                {
                    if(source != null)
                        source.Play();
                }

                if(playOnce)
                    triggered = true;

                if(resetOnTrigger)
                {
                    if (resetDelay == 0)
                        timer = 0.01f;
                    else 
                        timer = resetDelay;
                }

                if(cinematicFade)
                {
                    SoundManager.sm.FadeIn();
                }
            }
        }
    }

    public void Reset()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
        triggered = false;
        timer = 0;
    }
}
