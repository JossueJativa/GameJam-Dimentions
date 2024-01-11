using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Atributos publicos
    [SerializeField] private float movement;
    [SerializeField] private float dash;
    [SerializeField] private float dashTime;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    // Atributos privados
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canMove = true;
    private bool seeRight = true;
    private float gravityScale;
    private BoxCollider2D boxCollider2D;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravityScale = rb.gravityScale;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove == true){
            Movement();
        }
        Jump();
    }

    private void Movement(){
        float horisontalMovement = Input.GetAxis("Horizontal");

        // if(horisontalMovement != 0){
        //     anim.SetBool("isRunning", true);
        // } else {
        //     anim.SetBool("isRunning", false);
        // }

        rb.velocity = new Vector2(horisontalMovement * movement, rb.velocity.y);
        Orientation(horisontalMovement);

        if (Input.GetKeyDown(KeyCode.LeftShift)){
            StartCoroutine(Dash());
        }
    }

    private void Orientation(float horientation){
        if ( seeRight == true && horientation < 0 || seeRight == false && horientation > 0){
            seeRight = !seeRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider2D.bounds.center, 
            new Vector2(boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y), 
            0f, 
            Vector2.down, 
            0.2f, 
            groundLayer
        );

        return raycastHit2D.collider != null;
    }

    private IEnumerator Dash(){
        canMove = false;
        isDashing = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dash * transform.localScale.x, 0);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        canMove = true;
        isDashing = false;
        rb.gravityScale = gravityScale;
        trailRenderer.emitting = false;
    }
}
