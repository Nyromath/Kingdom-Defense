using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    //declaring variables
    [SerializeField] Vector3 Target;
    Animator GoblinAnimator;
    Rigidbody GoblinRB;
    double distanceToPlayerX;
    double distanceToPlayerZ;
    double distanceToPlayer;
    float attackDistance = 1;
    float GoblinSpeed;
    float GoblinDamage;
    System.Random rnd;

    //initializing attacking event
    public delegate void AttackAction(float damage);
    public static event AttackAction attacked;
    void Start()
    {
        //initializing variables
        GoblinRB = GetComponent<Rigidbody>();
        GoblinAnimator = GetComponent<Animator>();
        GoblinAnimator.SetInteger("battle", 1);

        //randomizing speed and attack strength against castle
        rnd = new System.Random();
        GoblinSpeed = rnd.Next(150, 300);
        GoblinDamage = rnd.Next(1, 3);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //faces goblin towards castle
        transform.LookAt(Target);

        //determining if goblin is in attack range of castle using pythagoras
        distanceToPlayerX = Math.Abs(Target.x - transform.position.x);
        distanceToPlayerZ = Math.Abs(Target.z - transform.position.z);
        distanceToPlayer = Math.Sqrt(Math.Pow(distanceToPlayerX, 2) + Math.Pow(distanceToPlayerZ, 2));

        if (distanceToPlayer >= attackDistance)
        {
            //goblin moves toward castle if out of attack range
            GoblinRB.velocity = transform.forward * GoblinSpeed * Time.deltaTime;
            GoblinAnimator.SetInteger("moving", 2);
        }
        else
        {
            //goblin attacks castle if in attack range
            GoblinRB.velocity *= 0;
            GoblinAnimator.SetInteger("moving", 0);

            Attack();
            enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
    void Attack()
    {
        //plays attack animation and triggers attacked event
        GoblinAnimator.Play("attack1");
        
        if(attacked != null)
        {
            attacked(GoblinDamage);
        }
    }

    public void Death()
    {
        //plays death animation and destroys goblin when killed by Player
        enabled = false;
        gameObject.layer = 0;
        GoblinRB.velocity *= 0;
        GoblinAnimator.Play("death2");
        Destroy(gameObject, 1);
    }
}
