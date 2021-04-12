using UnityEngine;
class PlayerScr : MonoBehaviour
{
    public Animator playerAnimator;
    public LayerMask groundLayer;
    public bool lockMov;
    [SerializeField] private float speed;
    private float horizontalMove;
    Vector2 groundCastStart;
    CapsuleCollider2D col;
    AudioSource textSound;
    LevelManager lm;
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        textSound = GetComponent<AudioSource>();
        lm = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        if (!lockMov) // use lockMov w/ Cinematics
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            horizontalMove = inputX * speed;
            if (inputX != 0) playerAnimator.SetBool("run", true);
            else playerAnimator.SetBool("run", false);
            if (inputX < 0) transform.localScale = new Vector2(-1, 1);
            if (inputX > 0) transform.localScale = new Vector2(1, 1);
            groundCastStart = new Vector2(transform.position.x, transform.position.y);
            if (!col.IsTouchingLayers(groundLayer)) // not grounded
            {
                RaycastHit2D hit = Physics2D.Raycast(groundCastStart, -Vector2.up, 1f, groundLayer);
                //Debug.DrawLine(groundCastStart, new Vector3(groundCastStart.x, groundCastStart.y - 1), Color.red, Time.deltaTime);
                if (hit.collider != null)
                {
                    //Debug.DrawLine(groundCastStart, new Vector3(groundCastStart.x, hit.point.y), Color.green, Time.deltaTime);
                    float groundPosY = -hit.distance;
                    transform.position = new Vector3(transform.position.x, transform.position.y + groundPosY); // snap to ground
                }
            }
            transform.Translate(new Vector2(horizontalMove, 0) * Time.deltaTime);
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (lm.canInteractWithPhone)
            {
                float rand = Random.Range(0.8f, 1.2f);
                textSound.pitch = rand;
                textSound.Play();
            }
        }

    }
    void FlipSprite(float direction)
    {
        // if (Mathf.Sign(playerSprite.transform.localScale.x) != Mathf.Sign(direction))
        // {
        //     playerSprite.transform.localScale = new Vector3(-playerSprite.transform.localScale.x,
        //         playerSprite.transform.localScale.y,
        //         playerSprite.transform.localScale.z);
        // }
    }
    public void LockPlayerMovement(bool b)
    {
        lockMov = b;
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}