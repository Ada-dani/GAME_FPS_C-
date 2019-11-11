using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer3 : MonoBehaviour
{

    private Animator anim;
    public AudioClip ReloadSound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Walk", true);//Animator: create parameters bool(walk)
            anim.SetBool("Run", false);
        }

        if (!Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);
        }

        if (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
        //if(Input.GetKeyDown(KeyCode.R) && GetComponentInChildren<Shoot>().Ammo == 0 && GetComponentInChildren<Shoot>().Loader > 0)
        if (Input.GetKeyDown(KeyCode.R) && GetComponentInChildren<Shot2>().Loader > 0 && GetComponentInChildren<Shot2>().Ammo == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(ReloadSound);
            anim.SetTrigger("Reload");
        }
        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Sniper", true);
        }
        else
        {
            anim.SetBool("Sniper", false);
        }


        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            anim.SetTrigger("FireRetreatSniper");
        }

        if (Input.GetButton("Fire1") && !Input.GetKey(KeyCode.LeftShift) && !Input.GetButton("Fire2"))
        {
            anim.SetTrigger("FireRetreat");
        }
    }
}
