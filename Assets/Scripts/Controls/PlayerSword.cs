using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public AudioSource audioSrc;

    private void OnTriggerEnter(Collider other)
    {
        Zombie zomb = other.gameObject.GetComponent<Zombie>();
        if (zomb)
        {
            audioSrc.Play();
            zomb.Die();
        }
    }
}
