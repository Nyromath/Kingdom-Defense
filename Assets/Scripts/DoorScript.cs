using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject goblin;
    [SerializeField] Vector3 goblinStart;
    Quaternion goblinRotation;
    void Start()
    {
        goblinRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        InvokeRepeating("Spawn", 1, 5000);
    }

    void Spawn()
    {
            Instantiate(goblin, goblinStart, goblinRotation);
    }
}
