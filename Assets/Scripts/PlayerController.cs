using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Atributos publicos
    [SerializeField] private float movement;
    [SerializeField] private float dash;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;

    // Atributos privados
    private Rigidbody2D rb;
    private bool isDashing;
    private bool seeRight = true;
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement(){
        float horisontalMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horisontalMovement * movement, rb.velocity.y);
        Orientation(horisontalMovement);
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
}
