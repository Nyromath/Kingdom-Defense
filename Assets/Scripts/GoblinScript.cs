using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    [SerializeField] Vector3 Target;
    Animator GoblinAnimator;
    Rigidbody GoblinRB;
    double distanceToPlayerX;
    double distanceToPlayerZ;
    double distanceToPlayer;
    float attackDistance = 1;
    float GoblinSpeed = 1500;
    void Start()
    {
        GoblinRB = GetComponent<Rigidbody>();
        GoblinAnimator = GetComponent<Animator>();
        GoblinAnimator.SetInteger("battle", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);

        distanceToPlayerX = Math.Abs(Target.x - transform.position.x);
        distanceToPlayerZ = Math.Abs(Target.z - transform.position.z);
        distanceToPlayer = Math.Sqrt(Math.Pow(distanceToPlayerX, 2) + Math.Pow(distanceToPlayerZ, 2));

        if (distanceToPlayer >= attackDistance)
        {
            GoblinRB.velocity = transform.forward * GoblinSpeed * Time.deltaTime;
            GoblinAnimator.SetInteger("moving", 2);
        }
        else
        {
            GoblinRB.velocity *= 0;
            GoblinAnimator.SetInteger("moving", 0);
        }
    }
}
