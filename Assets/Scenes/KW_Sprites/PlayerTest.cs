using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public GameObject blackHole;
    float move;
    float jumpForce = 8;
    float dashForce = 15;
    bool jump;
    bool grounded;
    bool dashInp;
    bool dashOrb = false;
    int dash = 1;
    float dashCD = 0;
    String direct = "Right";
    Vector2 respawn = new Vector2(0.0f, 0.0f);
    Vector2 moveVec;
    int cpNum = 0;

    float cTime = 0; //Coyote time
    int doubleJump = 1;
    Vector3 hell;

    LayerMask groundMask;
    Transform feet;
    Vector2 feetBox = new Vector2(0.5f, 0.15f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        feet = transform.Find("Feet");
        groundMask = LayerMask.GetMask("GROUND");
    }

    // Update is called once per frame
    void Update() {
        move = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapBox(feet.position, feetBox, 0, groundMask);
        
        if (dashCD > 0){dashCD -= Time.deltaTime;}
        if (dashCD >= 0.25){move = 0; rb.linearVelocityY = 0;}
        if (dashCD < 0.25 && dash == -1){dash = 0; rb.linearVelocityX = 0;}
        if (dashOrb && dash == 0) {dash = 1; dashOrb = false;}

        if (grounded) {
            cTime = 0.2f; doubleJump = 1; dash = 1; //rb.gravityScale = 1.0f;
        } else if (cTime > 0) {cTime -= Time.deltaTime;}

        if (Input.GetButtonDown("Jump")  && dashCD < 0.5f)
            if (grounded) {jump = true;}
            else if (cTime > 0 && rb.linearVelocityY < 0) {jump = true;}
            else if (doubleJump == 1) {jump = true; doubleJump = 0;} 

        if (Input.GetKeyDown(KeyCode.Space) && dash >= 1 && dashCD <= 0){
            dashInp = true;
        }

        var dir = blackHole.transform.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        //if (Input.GetKeyDown(KeyCode.F)) {Died();}
    }

    void FixedUpdate(){
        if (Mathf.Abs(rb.linearVelocityY) < 1.0f) {
            rb.gravityScale=0.75f; // hang time
        } else if (rb.linearVelocityY < 0.5f) {
            rb.gravityScale = 2.0f; // extra falling gravity
        }

        //limit down and up velocity
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -12, 8);

        if (move != 0){
            //rb.linearVelocityX = move * 5;
            //if (rb.linearVelocityX >= 0){direct = "Right"; sr.sprite = Right;}
            //else {direct = "Left"; sr.sprite = Left;}
            rb.linearVelocity = transform.right * move * 5;
        }

        if (jump){
            jump = false;
            rb.linearVelocityY = 0;
            rb.gravityScale = 1.0f;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (dashInp) {
            dashInp = false; dashCD = .5f; rb.linearVelocityX = 0.0f; rb.linearVelocityY = 0.0f; dash = -1;
            if (direct == "Right") {rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);}
            else {rb.AddForce(transform.right * dashForce * -1, ForceMode2D.Impulse);}
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        /*if (collision.gameObject.CompareTag("Checkpoint")){
            if (collision.GetComponent<Checkpoint>().checkNum > cpNum) {
                cpNum = collision.GetComponent<Checkpoint>().checkNum;
                respawn = new Vector2(collision.GetComponent<Checkpoint>().respawnX, collision.GetComponent<Checkpoint>().respawnY);
            }
        }
        if (collision.gameObject.CompareTag("Hurt")){Died();}*/
    }

    public void pRespawn() {
        transform.position = respawn;
    }
}
