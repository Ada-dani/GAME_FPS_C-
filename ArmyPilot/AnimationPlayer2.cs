using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer2 : MonoBehaviour
{

    private Animator anim;
    public AudioClip ReloadSound;
    private float NextFire;

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

        if (Input.GetKeyDown(KeyCode.R) && GetComponentInChildren<Shot2>().Loader > 0 && GetComponentInChildren<Shot2>().Ammo == 0)
        //if (Input.GetKeyDown(KeyCode.R) && GetComponentInChildren<Shot>().Loader > 0)
        {
            anim.SetTrigger("Reload");
            GetComponent<AudioSource>().PlayOneShot(ReloadSound);
        }

    }
}
