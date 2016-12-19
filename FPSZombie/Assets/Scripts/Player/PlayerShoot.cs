using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public int gunDamage = 40;
    public float timeBetweenBullets = 0.15f;
    public float weaponRange = 100f;
    public float hitForce = 0.05f;
    public Transform gunEnd;

    Camera fpsCam;
    WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    AudioSource gunAudio;
    LineRenderer laserLine;
    //ParticleSystem hitParticles;
    ParticleSystem gunParticles; 
    float nextFire;
    int shootableMask;
    float timer;


    // Use this for initialization
    void Start () {

        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        shootableMask = LayerMask.GetMask("Shootable");
        //hitParticles = GetComponent<ParticleSystem>();
        gunParticles = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime; 

        if (Input.GetButtonDown("Fire1") && timer > nextFire)
        {
            

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);    // second position

                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

                if(health != null)
                {
                    health.TakeDamage(gunDamage, hit.point);
                }
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
	}

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        //hitParticles.Play();
        gunParticles.Play();
    }
}
