using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyWalkingZombieController : MonoBehaviour
{

    public Transform target;
    private UnityEngine.AI.NavMeshAgent nav;
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "target")
        {
            Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
    }
}
