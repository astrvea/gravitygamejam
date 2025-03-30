using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class PlayerTest : MonoBehaviour
{
    public List<GameObject> hearts;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public GameObject blackHole;
    public TMP_Text scoreTxt;
    float move;
    float dashForce = 15;
    bool jump;
    bool grounded;
    bool dashInp;
    bool dashOrb = false;
    int dash = 1;
    float dashCD = 0;
    String direct = "Right";
    Vector2 respawn = new Vector2(0.0f, 0.0f);
    int cpNum = 0;

    public float spdMLT;
    public float jumpForce = 6;
    public int playNum;
    public int score = 0;
    public int health = 5;
    float iFrames = 0;

    float cTime = 0; //Coyote time
    int doubleJump = 1;
    Vector3 hell;

    LayerMask groundMask;
    Transform feet;
    Vector2 feetBox = new Vector2(0.5f, 0.35f);
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
        //Is the player standing on something?
        grounded = Physics2D.OverlapBox(feet.position, feetBox, 0, groundMask);
        
        //Dash stuff, ignore unless we want the dash later.
        if (dashCD > 0){dashCD -= Time.deltaTime;}
        if (dashCD >= 0.25){move = 0; rb.linearVelocityY = 0;}
        if (dashCD < 0.25 && dash == -1){dash = 0; rb.linearVelocityX = 0;}
        if (dashOrb && dash == 0) {dash = 1; dashOrb = false;}

        //Coyote frames; The player keeps the ability to ground jump for a few frames after walking off a ledge
        if (grounded) {
            cTime = 0.2f; doubleJump = 1; dash = 1; //rb.gravityScale = 1.0f;
        } else if (cTime > 0) {cTime -= Time.deltaTime;}

        //Jump
        if (Input.GetButtonDown("Jump")  && dashCD < 0.5f)
            if (grounded) {jump = true;}
            else if (cTime > 0 && rb.linearVelocityY < 0) {jump = true;}
            else if (doubleJump == 1) {jump = true; doubleJump = 0;} 

        /*if (Input.GetKeyDown(KeyCode.Space) && dash >= 1 && dashCD <= 0){
            dashInp = true;
        }*/

        //Rotates the pplayer's feet towards the black hole
        var dir = blackHole.transform.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        //Test death
        //if (Input.GetKeyDown(KeyCode.F)) {Died();}
    }

    void FixedUpdate(){
        /*if (Mathf.Abs(rb.linearVelocityY) < 1.0f) {
            rb.gravityScale=0.75f; // hang time
        } else if (rb.linearVelocityY < 0.5f) {
            rb.gravityScale = 2.0f; // extra falling gravity
        }*/

        //limit down and up velocity
        //rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -12, 8);

        //Left + Right movement
        if (move != 0){
            rb.AddForce(transform.right * move * spdMLT);
        }

        //Jump
        if (jump){
            jump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        //More dash stuff: Ignore unless we want jumping later.
        if (dashInp) {
            dashInp = false; dashCD = .5f; rb.linearVelocityX = 0.0f; rb.linearVelocityY = 0.0f; dash = -1;
            if (direct == "Right") {rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);}
            else {rb.AddForce(transform.right * dashForce * -1, ForceMode2D.Impulse);}
        }
        if (iFrames > 0) {
            iFrames -= Time.fixedDeltaTime;
            Debug.Log(iFrames);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        /*if (collision.gameObject.CompareTag("Hurt")){Died();}*/
        if (collision.gameObject.CompareTag("Token")) {
            score += 1;
            scoreUp();
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PAIN")) {
            if (iFrames <= 0){
            iFrames = 1.5f;
            health -= 1;
            DrawHearts();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PAIN")) {
            if (iFrames <= 0){
            iFrames = 1.5f;
            health -= 1;
            DrawHearts();
            }
        }
    }

    void DrawHearts() {
        for (int i = 0; i < hearts.Count; i++) {
            hearts[i].SetActive(false);
        }

        for (int i = 0; i < health; i++) {
            hearts[i].SetActive(true);
        }
    }

    void scoreUp(){
        scoreTxt.text = "P" + playNum.ToString() + " Score: " + score.ToString();
    }

    public void pRespawn() {
        transform.position = respawn;
    }
}
