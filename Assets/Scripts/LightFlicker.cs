using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
class LightFlicker : MonoBehaviour
{
    public new Light2D light;
    public new Light2D haloTop;
    public new Light2D haloFloor;
    bool isFlickering;
    float speed = 3;
    float light_minInt = .6f, light_maxInt = 1;
    float haloTop_minInt = .4f, haloTop_maxInt = .6f;
    float haloFloor_minInt = 1.5f, haloFloor_maxInt = 2;
    void Update()
    {
        if (!isFlickering)
        {
            light.intensity = Mathf.Clamp(Mathf.Sin(Time.time * speed) + 1 + light_minInt, 0, light_maxInt);
            haloTop.intensity = Mathf.Clamp(Mathf.Sin(Time.time * speed) + 1 + haloTop_minInt, 0, haloTop_maxInt);
            haloFloor.intensity = Mathf.Clamp(Mathf.Sin(Time.time * speed) + 1 + haloFloor_minInt, 0, haloFloor_maxInt);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {
        isFlickering = true;
        light.intensity = 0;
        haloTop.intensity = 0;
        haloFloor.intensity = 0;
        yield return new WaitForSeconds(.1f);
        isFlickering = false;
    }
}