using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public AudioSource audioSrc;

    public float shotCD = 0.75f;

    public float shotTimer;

    public bool shotOnCooldown = false;

    public GameObject muzzleflash;

    public float muzzleTimer = 0.2f;

    public LayerMask mask;

    private void Awake()
    {
        shotTimer = shotCD;
    }

    public void Shoot()
    {
        if (!shotOnCooldown)
        {
            muzzleflash.SetActive(true);
            audioSrc.Play();

            RaycastHit hit;

            if(Physics.Raycast(muzzleflash.transform.position, transform.TransformDirection(Vector3.right), out hit, 500f, mask))
            {
                Debug.Log(hit.collider.gameObject.name);

                Zombie zomb = hit.collider.gameObject.GetComponent<Zombie>();
                if (zomb && !zomb.isDead)
                {
                    zomb.Die();
                }
            }
        }
    }

    void Update()
    {
        if (muzzleflash.activeInHierarchy)
        {
            muzzleTimer -= Time.deltaTime;
            if(muzzleTimer <= 0)
            {
                muzzleflash.SetActive(false);
                muzzleTimer = 0.2f;
            }
        }

        if (shotOnCooldown)
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0)
            {
                shotOnCooldown = false;
                shotTimer = shotCD;
            }
        }
    }
}
