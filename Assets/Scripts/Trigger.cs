using UnityEngine;
class Trigger : MonoBehaviour
{
    public bool isTriggered;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") isTriggered = true;
    }
}