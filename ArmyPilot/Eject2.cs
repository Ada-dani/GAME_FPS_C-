using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject2 : MonoBehaviour
{

    private Animator Anim;

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
                Anim.SetBool("Eject", true);
            }
            else
            {
                Anim.SetBool("Eject", false);

            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Anim.SetBool("Eject", false);

        }

    }
}
