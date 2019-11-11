using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject3 : MonoBehaviour
{

    private Animator Anim;
    private float NextFire;
    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && !Input.GetKey(KeyCode.LeftAlt))
        {

            if (GetComponentInParent<Shot2>().Ammo > 0)
            {
                if (Time.time > NextFire)
                {
                    Anim.SetBool("Eject", true);
                    NextFire = Time.time + GetComponentInParent<Shot>().FireRate;
                }
                else
                {
                    Anim.SetBool("Eject", false);
                }

            }
        }
    }
}

