using UnityEngine;

public class Player : MonoBehaviour {
    
    public float velocity; //2f

    public Transform ground;

    private Animator animator;
    private Rigidbody2D rb2d;

    private bool grounded;
    public float force; //250

    public float jumpDelay; //0.4f
    private float jumpTime;
    private bool jumped;

	private Vector3 position;
	private Vector2 angle;

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        grounded = Physics2D.Linecast(transform.position, ground.position, 1 << LayerMask.NameToLayer("Plataforma"));

        jumpTime = jumpDelay;
        jumped = false;

        animator.SetBool("jump", jumped);
        animator.SetBool("ground", !jumped);

		position = transform.position;
		angle = new Vector2 (transform.eulerAngles.x, transform.eulerAngles.y);
    }
	
	// Update is called once per frame
	void Update () {
        Movimentar();
    }

    void Movimentar() {
        grounded = Physics2D.Linecast(transform.position, ground.position, 1 << LayerMask.NameToLayer("Plataforma"));

        animator.SetFloat("movement", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("walk", Input.GetAxisRaw("Horizontal") != 0);
        
        if (Input.GetAxisRaw("Horizontal") > 0) {
			position = transform.position;
			position.x += velocity * Time.deltaTime;
			transform.position = position;

			angle.Set(0, 0);
			transform.eulerAngles = angle;
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
			position = transform.position;
			position.x -= velocity * Time.deltaTime;
			transform.position = position;

			angle.Set(0, 180);
			transform.eulerAngles = angle;
        }

        if (Input.GetButtonDown("Jump") && grounded && !jumped) {
            rb2d.AddForce(transform.up * force);
            jumpTime = jumpDelay;
            jumped = true;
            animator.SetBool("jump", jumped);
            animator.SetBool("ground", !jumped);
        }

        jumpTime -= Time.deltaTime;

        if (jumpTime <= 0 && grounded && jumped) {
            jumped = false;
            animator.SetBool("ground", !jumped);
            animator.SetBool("jump", jumped);
        } 
    }
}
