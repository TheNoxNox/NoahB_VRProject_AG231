using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Animator Anim => GetComponent<Animator>();

    public NavMeshAgent Agent => GetComponent<NavMeshAgent>();

    public Collider MyCol => GetComponent<Collider>();

    public List<AudioClip> Growls = new List<AudioClip>();

    public AudioSource audioSrc => GetComponent<AudioSource>();

    public float deathTimer = 4f;

    public float growlCD = 4.2f;

    public float growlTimer;

    public bool isDead = false;

    private void Awake()
    {
        growlTimer = growlCD;
    }

    private void Update()
    {

        if (isDead)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Vector3 playerPos = new Vector3(Game.Instance.thePlayer.gameObject.transform.position.x, 0, Game.Instance.thePlayer.gameObject.transform.position.z);
            Vector3 myPos = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            if(Mathf.Abs(Vector3.Distance(myPos, playerPos)) <= 2f)
            {
                Agent.SetDestination(gameObject.transform.position);
                Anim.SetBool("Attacking", true);
                Anim.SetBool("Moving", false);
            }
            else
            {
                Agent.SetDestination(Game.Instance.thePlayer.gameObject.transform.position);
                Anim.SetBool("Attacking", false);
                Anim.SetBool("Moving", true);
            }

            if (!audioSrc.isPlaying)
            {
                growlTimer -= Time.deltaTime;
                if(growlTimer <= 0)
                {
                    Growl();
                }
            }
        }
    }

    public void Growl()
    {
        audioSrc.PlayOneShot(Growls[Random.Range(0, Growls.Count)]);
        growlTimer = growlCD + Random.Range(-1.2f, 1.2f);
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Anim.SetInteger("DeathType", Random.Range(0, 2));
            Anim.SetBool("Dead", true);
            Agent.isStopped = true;
            Game.Instance.RemoveZombie(this);
        }
        
    }

    public void DamagePlayer()
    {

    }
}
