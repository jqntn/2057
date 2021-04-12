using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Heart : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private bool beat = false;
    public bool shake; ////

    private float coolDown = 1.6f;
    private float timer = 0;
    private float delay = 0.4f;

    public AudioSource beat1;
    public AudioSource beat2;

    private static Heart instance;

    public static Heart Heartbeat
    {
        get { return instance;  }
    }

    public float Cooldown
    {
        get { return coolDown; }
        set { coolDown = value; }
    }

    public float Delay
    {
        get { return delay; }
        set { delay = value; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Init();
    }

    public void Init()
    {
        Cooldown = 1.6f;
        Delay = .4f;
        timer = Cooldown;
        Beat(true);
    }
    public void Init(float BeatCooldown, float BeatDelay)
    {
        Delay = BeatDelay;
        Cooldown = BeatCooldown;
        timer = Cooldown;
        Beat(true);
    }

    private void Update()
    {
        if (beat)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (timer > -1) // first Beat
                {
                    beat1.Play();
                    timer -= 1;
                }

                if (timer <= -1 - delay) // second Beat
                {
                    beat2.Play();
                    timer = coolDown;
                }
            }
        }
    }

    public void Beat(bool on)
    {
        beat = on;
    }
    /*
    private IEnumerator StopBeat()
    {
        Debug.Log("Boom.");
        yield return new WaitForSeconds(0.06f);
        shake = false; //
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }*/
}
