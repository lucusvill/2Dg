using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] float horizontalInput;
    public float speed;
    public float pown_jump;
    [SerializeField] float time_ro;
    [SerializeField] float speed_ro;
    private Animator anim;
    private bool grounded;
    [SerializeField] bool canjump;
    private bool falled;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        time_ro += Time.deltaTime;
        MovePlayer();
        Debug.Log("Ground :" + grounded);
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput == 0 && canjump == false)
        {
            Debug.Log("gg");
            anim.Play("idle");
        }
        else if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
            //transform.localScale = Vector3.Lerp(new Vector3(transform.localScale.x,1,1),new Vector3(1,1,1),time_ro / speed_ro);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.localScale = Vector3.Lerp(new Vector3(transform.localScale.x,1,1),new Vector3(-1,1,1),time_ro / speed_ro);
        }
        else
        {
            time_ro = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space)&& grounded)
        {
            canjump = true;
            Jump();
            
            anim.SetBool("grounded", false);
            anim.SetBool("jump1", true);
           // if (!grounded)
             
        }

        anim.SetBool("walk", horizontalInput != 0);
        
    }

    private void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        anim.Play("jump");
        anim.SetTrigger("jump");
        body.velocity = new Vector2(body.velocity.x, pown_jump);
        
        
        grounded = false;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canjump = false;
            grounded = true;
            anim.SetBool("grounded", grounded);
        }
        else 
        {

            grounded = false;
            anim.SetBool("fall",grounded);

        }
    }



}
