using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningAnimation : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("IsOpen", false);
        }
    }

    void DoorOpenSound()
    {
        if(anim.GetBool("IsOpen") == true)
        {
            Debug.Log("SoundOpen");
        }
    }

    void DoorClosingSound()
    {
        if (anim.GetBool("IsOpen") == false)
        {
            Debug.Log("SoundClosing");
        }
    }
}
