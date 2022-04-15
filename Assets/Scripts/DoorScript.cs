using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //declaring variables
    [SerializeField] GameObject goblin;
    [SerializeField] Vector3 goblinStart;
    Quaternion goblinRotation;
    System.Random rnd = new System.Random();
    float delay;

    void Start()
    {
        goblinRotation = Quaternion.Euler(0, 0, 0);

        //spawns first goblin after 1 second
        Invoke("Spawn", 1);
    }

    void Spawn()
    {
        //spawns a goblin at correct position
        Instantiate(goblin, goblinStart, goblinRotation);

        //spawns another goblin after randomly generated delay
        delay = rnd.Next(3, 10);
        Invoke("Spawn", delay);
    }
}
