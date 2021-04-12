using UnityEngine;
class FollowX : MonoBehaviour
{
    public Transform player;
    float playerOffsetPos;

    public Transform trigger_Min;
    public Transform pendu_Max;

    void Update()
    {
        playerOffsetPos = player.transform.position.x + 3;

        if (player.transform.position.x >= -1 && player.transform.position.x <= 67)
        {
            if (Vector2.Distance(player.position, pendu_Max.position) <= Vector2.Distance(trigger_Min.position, pendu_Max.position))
            {
                float ratio = Mathf.InverseLerp(0, Vector2.Distance(trigger_Min.position, pendu_Max.position), Vector2.Distance(trigger_Min.position, player.position));
                transform.position = Vector2.Lerp(new Vector2(playerOffsetPos, 0), pendu_Max.position, ratio);

                Camera.main.orthographicSize = Mathf.Lerp(5, 3.5f, ratio);
            }
            else
                transform.position = new Vector2(playerOffsetPos, 0);
        }
    }
}