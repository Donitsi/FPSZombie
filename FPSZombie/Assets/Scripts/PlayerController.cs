using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private int health = 3;

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(-x, 0f, -z);
    }

    public void takeDamage(int damage) {
        Material material = GetComponent<Renderer>().material;
        health -= damage;
        if (health == 2) material.color = Color.yellow;
        if (health == 1) material.color = Color.red;
        if (health <= 0) Destroy(gameObject);
    }
}
