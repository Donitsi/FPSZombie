using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public AudioClip[] playerHurt;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public GameObject deathFade;


    Animator anim;
    AudioSource playerAudio;
    //PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)]);

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        //playerMovement.enabled = false;
        //playerShooting.enabled = false;

        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        Image deathFadeImage = deathFade.GetComponent<Image>();
        Color fadeColor;
        Vector3 fallingVelocity = new Vector3(0f, -0.02f, 0f);

        deathFade.SetActive(true);

        while (true)
        {
            transform.position += fallingVelocity;

            fadeColor = deathFadeImage.color;
            fadeColor.a += 0.03f;
            deathFadeImage.color = fadeColor;

            if (fadeColor.a < 1f) yield return new WaitForSeconds(0.1f);
            else break;
        }

        // Vaihda ladattavaksi Sceneksi Game Over!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }


    //public void RestartLevel ()
    //{
    //    SceneManager.LoadScene (0);
    //}
}
