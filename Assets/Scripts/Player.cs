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

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        grounded = Physics2D.Linecast(transform.position, ground.position, 1 << LayerMask.NameToLayer("Plataforma"));

        jumpTime = jumpDelay;
        jumped = false;

        animator.SetBool("jump", jumped);
        animator.SetBool("ground", !jumped);
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
            transform.Translate(Vector2.right * velocity * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Translate(Vector2.right * velocity * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
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
