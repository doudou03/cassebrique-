using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{ 
      public Rigidbody2D rb;
      public bool inplay;
      public Transform paddle;
      public float speed;
      public Transform explosion;
      public GameManager gm;
      public Transform powerup;
      AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource>  ();

    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver){
            return;
        }
    if(!inplay){
        transform.position = paddle.position;
    }

    if(Input.GetButtonDown ("Jump") && !inplay) {
        inplay = true;
        rb.AddForce (Vector2.up * speed);
    }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bottom")) {
            Debug.Log("Ball hit the bottom screen");
            rb.velocity = Vector2.zero; 
            inplay = false;
            gm.UpdateLives (-1);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.transform.CompareTag ("brick")){
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript> ();
            if (brickScript.hitsToBreak > 1){
                brickScript.BreakBrick();
            }
            else{

            int randChance = Random.Range(1,101);
            if (randChance < 10){
                Instantiate(powerup,other.transform.position,other.transform.rotation);
            }

           Transform newExplosion = Instantiate(explosion,other.transform.position,other.transform.rotation);
           Destroy(newExplosion.gameObject,2.5f);
            
            gm.UpdateScore (brickScript.points);
            gm.UpdateNumberOfBricks();
            Destroy(other.gameObject);
            }
            audio.Play ();
        }
    }

}
