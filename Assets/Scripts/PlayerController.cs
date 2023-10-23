using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    private Rigidbody playerRb;
    public float rangeY = 3f;
    private ScoreManager score;
    public bool isGameOver = false;
    public float pushForce = 10f;
    public AudioSource playerAudio;
    public AudioClip collideSound;
    public ParticleSystem hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();                                       //getting rigidbody component for player
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();       //accessing score script
        playerAudio = GetComponent<AudioSource>();                                  //getting audio component
    }

    // Update is called once per frame
    void Update()
    {
        PlayerKill();                                                               //call player kill function
        PlayerMovement();                                                           //call player movement function
    }
    //fucntiom will call when it fall from the platform 
    private void PlayerKill()
    {
        if (transform.position.y < -rangeY)             //if player reach certain height after falling then
        {
            score.GameOver();                          //mar jaye ga hamara player
            isGameOver = true;                         //this will check it to true mean officially you are unable to complate this game.
        }
    }
    //this fucntion is responsible for player movement alhumdulillah
    private void PlayerMovement()
    {
        if (isGameOver != true && score.isWin != true)
        {
            float horizontal = Input.GetAxis("Horizontal");                                    //taking horizontal input
            float vertical = Input.GetAxis("Vertical");                                        //taking vertical input from keyboard
            Vector3 movePlayer = new Vector3(horizontal, 0f, vertical);                        //store those axis or input in a new vector3 
            if (movePlayer.magnitude > 0.1f)                                                   //check magnitude of the vector is condition true 
            {
                transform.Translate(movePlayer * playerSpeed * Time.deltaTime, Space.World);   //this will move player in the direction given by keyboard
                transform.rotation = Quaternion.LookRotation(movePlayer);                      //also rotate it the player in the direction of movement
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))                                          //check collision with enemy
        {
            Rigidbody enemyrb = collision.gameObject.GetComponent<Rigidbody>();                //taking enemy rigidbody
            Vector3 pushAway = collision.gameObject.transform.position - transform.position;   //get direction in which force will apply     
            enemyrb.AddForce(pushAway * pushForce, ForceMode.Impulse);                         //apply force in the calculte direction 
            playerAudio.PlayOneShot(collideSound, 1f);                                         //play hit sound
            hitParticle.Play();                                                                //spawn or play hit particle
        }

    }
}
