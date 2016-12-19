using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

	public Camera mainCamera;
    public Transform playerPosition;
    public PlayerController playerController;//Scripti, jossa void takeDamage(int) -metodi
    public Collider attackCollider;
    public Collider walkAttackCollider;

    private bool attacking = false;
    private int attackDamage = 2;
    private int walkAttackDamage = 1;

    // Animaattorin parametrien (walkParameter, attackParameter) arvot väliltä 0.0 -> 1.0
    // 0.1 jälkeen vastaava animaatio menee päälle.
    // Kävely ja hyökkäysanimaatiot voivat olla yhtäaikaa päällä jolloin animaatiot sekoitetaan (jos molemmat arvot < 1.0).
    private Animator animator;
    private float walkParameter = 0f;
    private float attackParameter = 0f;
    private bool dieParameter = false;

	private UnityEngine.AI.NavMeshAgent agent;

    IEnumerator attackCoroutine() {
        attacking = true;
        agent.Stop();

        // Aloittaa animaation
        attackParameter = 0.5f;

        // Collider päälle sopivassa kohdassa animaatiota
        yield return new WaitForSeconds(0.2f);

        if (walkParameter > 0.1) walkAttackCollider.enabled = true;
        else attackCollider.enabled = true;

        // Lopuksi collider ja animaatio pois päältä
        yield return new WaitForSeconds(0.7f);
        
        attackCollider.enabled = false;
        walkAttackCollider.enabled = false;
        attackParameter = 0f;
        
        agent.Resume();
        attacking = false;
    }

    IEnumerator dieCoroutine() {
        // NavMeshAgent ei saa päivittää translationia kun sitä säädetään koodista
        agent.updatePosition = false;
        agent.updateRotation = false;

        // Aloittaa animaation
        dieParameter = true;

        walkParameter = 0f;
        attackParameter = 0f;
        attackCollider.enabled = false;
        walkAttackCollider.enabled = false;

        // Maatuu sopivan ajan kuluttua
        yield return new WaitForSeconds(2f);

        float decayingHeight = transform.position.y - 0.31f;
        Vector3 decayingVelocity = Vector3.down * 0.02f;

        while (transform.position.y > decayingHeight)
        {
            transform.position += decayingVelocity * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    void Start () {
        animator = GetComponent<Animator>();
        attackCollider.enabled = false;
        walkAttackCollider.enabled = false;

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(playerPosition != null) agent.SetDestination(playerPosition.position);
    }

    void Update () {
        if (playerPosition != null) agent.SetDestination(playerPosition.position);

        //Kävelyanimaatiopäälle jos Zombie liikkuu
        if (agent.velocity.magnitude > 0.1f) walkParameter = 0.5f;
        else walkParameter = 0f;

        if(agent.remainingDistance < 2f && !attacking) attack();

        animator.SetFloat("Walk", walkParameter);
        animator.SetFloat("Attack", attackParameter);
        animator.SetBool("Die", dieParameter);
    }

    void OnTriggerEnter(Collider other) {
        // Pelaajan collider vs Zombien hyökkäys-collider
        if (other.tag.Equals("Player")) {
            if (walkParameter > 0.1f) playerController.takeDamage(walkAttackDamage);
            else playerController.takeDamage(attackDamage);
        }
    }

    public void attack() {
        if (!dieParameter) StartCoroutine(attackCoroutine());
    }

    public void die() {
        if (!dieParameter) StartCoroutine(dieCoroutine());
    }
}
