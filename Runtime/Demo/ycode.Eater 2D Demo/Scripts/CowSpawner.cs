using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ycode.Eater;

public class CowSpawner : MonoBehaviour
{
    public Eater CowPrefab;

    void Start()
    {
        if (CowPrefab != null)
        {
            var cow = Instantiate(CowPrefab);
            cow.transform.position = transform.position;
        }
    }
}
