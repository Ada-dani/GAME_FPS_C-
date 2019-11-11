using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniper : MonoBehaviour {

    public Transform Target;
    public float LookAt = 30f;
    public float FireAt = 30f;
    public int Damage = 10;
    public float FireRate = 2f;
    public GameObject Projectil;
    public GameObject Eject;
    public AudioClip SoundFire;
    public int Force;
    public AudioClip SoundDead;

    private Animator Anim;
    private float NextFire;
    void Start()
    {
        Anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Verification A Distance between player and enemy
        if (Vector3.Distance(Target.position, transform.position) < LookAt)
        {
            SniperAim();
        }
        else
        {
            Anim.SetBool("Aim", false);
        }

        // verification shooting distance
        if (Vector3.Distance(Target.position, transform.position) < FireAt)
        {
            SniperFire();
        }


    }
    void SniperAim()
    {
        //Animation Aim
        Anim.SetBool("Aim", true);

        //Rotation to Target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.position - transform.position), 20 * Time.deltaTime);

        //Block Rotation Z
        if (Vector3.Distance(Target.position, transform.position) < 5)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }

    }
    void SniperFire()
    {
        //raycast
        //Debug.DrawLine(Eject.transform.position, transform.TransformDirection(Vector3.forward * 40));
        Debug.DrawRay(Eject.transform.position, transform.TransformDirection(Vector3.forward * 40));
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            RaycastHit hit;
            if(Physics.Raycast(Eject.transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if(hit.transform.tag == "Player")
                {
                    //Projectil
                    GetComponent<AudioSource>().PlayOneShot(SoundFire);
                    GameObject Bullet = Instantiate(Projectil, Eject.transform.position, Quaternion.identity) as GameObject;
                    Bullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * Force);
                }
            }
        }
    }
    public void SniperDead()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(SoundDead);
        Anim.SetTrigger("Dead");
        Destroy(gameObject, 2f);
    }
}

