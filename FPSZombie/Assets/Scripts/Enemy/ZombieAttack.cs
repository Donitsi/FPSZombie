using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    CharacterController characterController;
    EnemyHealth enemyHealth;
    AudioSource enemyAudio;
    public AudioClip[] enemyAttack;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        characterController = player.GetComponent<CharacterController>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {

        anim.SetBool("Attack", true);
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        anim.SetBool("Attack", false);

        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            //anim.SetTrigger("PlayerDead");

            characterController.enabled = false;
        }
    }


    void Attack()
    {
        timer = 0f;

        enemyAudio.PlayOneShot(enemyAttack[Random.Range(0, enemyAttack.Length)]);

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
