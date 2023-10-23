using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    public float enemySpeed = 10f;
    private PlayerController player;
    private ChangeTexture changeTexture;
    public float rangeY = 2f;
    private ScoreManager score;
    private Vector3 offset = new Vector3(0.1f, 0.1f, 0.1f);
    public float pushForce=5f;
    public ParticleSystem hitParticles;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();                                                    //getting rigidbody component of enemy 
        player = GameObject.Find("Player").GetComponent<PlayerController>();                    //accessing the player script from enenmy script
        changeTexture = GameObject.Find("ChangeTexture").GetComponent<ChangeTexture>();         //accessing the texture changing script 
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();                   //accessing the scoremanager script 
        
    }

    // Update is called once per frame
    void Update()
    {
        KillEnemy();                                                                            //calling kill enemy function
        EnemyMovement();                                                                        //calling enemy movement function
    }
    // this fucntion will perform some functionilty when enemy hit and fall from platform
    private void KillEnemy()
    {
        if (transform.position.y < -rangeY)                                      //check if enemy y range value is less then -2
        {
            enemyRb.isKinematic = true;                                         //because we are using bound for camera in which we give reference of enemy thus we cant desrtroy it
                                                                                //therefore instead of destroying it in just turned off the mesh of the object and set iskinematic to true
            transform.GetComponent<MeshRenderer>().enabled = false;             //mean after the condition become true it will stuck at that position
            changeTexture.ChangeMaterial();                                     //when this happen it will change player texture or material
            score.Enemy();                                                      //it will call enemy funcion from score manager script to decrement the value of enemies
            gameObject.SetActive(false);                                        //and it self to off                             

        }
    }
    //this function perfrom the movement fucntionalty for our enemy
    private void EnemyMovement()
    {
        if (player.isGameOver != true && score.isWin != true)                                       //if any of the condition is true then below function will not perfrom 
        {
            Vector3 enemyDirection = (player.transform.position - transform.position).normalized;  //this vector calculate the difference between player and enemy
            transform.Translate(enemyDirection * enemySpeed * Time.deltaTime, Space.World);        //this will move enemy toward player direction 
            transform.rotation = Quaternion.LookRotation(enemyDirection);                          //this line will rotate enemy face toward the direction of movement
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScoreTrig"))                                              //below platform on negative y=-2 in create a plane and check it trigger 
        {                                                                                          //so when enemy hit with that collider then score will update;
            score.AddScore();                                                                      //call addscore function from score manager script
            player.transform.localScale += offset;                                                 //it will increase the scale of player by 0.1 on all axes each time enemy hit 
        }                                                                                          //hit the collider

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))                                             //check if enemy collide with player then it will apply force on it 
        {
            Rigidbody playerrb = collision.gameObject.GetComponent<Rigidbody>();                  //getting player rigidbody
            Vector3 pushAway = collision.gameObject.transform.position - transform.position;      //getting push direction 
            playerrb.AddForce(pushAway * pushForce, ForceMode.Impulse);                           //apply force in that direction
            hitParticles.Play()                                                                   //when enemy hit with player it play a hit particle
;        }
        else if (collision.gameObject.CompareTag("Enemy"))                                        //check if enemy are collide with each other 
        {
            Vector3 pushAway = collision.gameObject.transform.position - transform.position;      //getting push direction by getting both object positions
            enemyRb.AddForce(pushAway * pushForce, ForceMode.Impulse);                            //apply force in that direction
            hitParticles.Play();                                                                  //it is play hit particles
        }
    }
}
