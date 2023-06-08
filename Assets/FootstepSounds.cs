using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class FootstepSounds : MonoBehaviour
{
    public FMODUnity.EventReference walkEvent;
    //public FMODUnity.EventReference runEvent;
    private FMOD.Studio.EventInstance locomotion;
    private float surfaceType;
    private float locomotionType;

    public LayerMask lm;
    RaycastHit rh;
    public GameObject leftFoot, rightFoot;
    private GameObject footObject;
    private string footUsed;


    private vThirdPersonInput tpInput;
    private vThirdPersonController tpControl;
    private vAnimatorMoveSender moveSender;

    // Start is called before the first frame update
    void Start()
    {
        tpInput = GetComponent<vThirdPersonInput>();
        tpControl = GetComponent<vThirdPersonController>();
        moveSender = GetComponent<vAnimatorMoveSender>();
    }

    // Update is called once per frame
    void Update()
    {
        if(footObject != null)
        {
            Debug.DrawRay(footObject.transform.position, Vector3.down * rh.distance, Color.red);
        }
            
    }

    void FootstepSound()
    {
            CheckSurface();
            locomotion = FMODUnity.RuntimeManager.CreateInstance(walkEvent);
            locomotion.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(footObject.transform.position));
            locomotion.setParameterByName("Surface", surfaceType);
            locomotion.setParameterByName("LocomotionType", locomotionType);
            locomotion.start();
            locomotion.release();

    }

    public void CheckSurface()
    {
        
        
        //RaycastHit rh;
        if(Physics.Raycast(footObject.transform.position, Vector3.down, out rh, 0.3f, lm))
        {
            Debug.DrawRay(footObject.transform.position, Vector3.down * rh.distance, Color.red);
            if (rh.collider.tag == "Sand") surfaceType = 1;
            else if (rh.collider.tag == "Gravel") surfaceType = 0f;
            else if (rh.collider.tag == "Wood") surfaceType = 2f;
            Debug.Log(rh.collider.tag);
        }
        //Debug.DrawRay(gameObject.transform.position, Vector3.down, 0.3f);
    }

    public void JumpAnim(string foot)
    {
        if (foot == "Left") footObject = leftFoot;
        else footObject = rightFoot;
        locomotionType = 3f;
        FootstepSound();
    }


    public void LandAnim(string foot)
    {
        if (foot == "Left") footObject = leftFoot;
        else footObject = rightFoot;
        locomotionType = 4f;
        FootstepSound();
        Debug.Log("Land");
    }

    public void CrouchAnim(string foot)
    {
        if (foot == "Left") footObject = leftFoot;
        else footObject = rightFoot;

        if (tpInput.cc.inputMagnitude > 0.1f)
        {
            locomotionType = 2f;
            FootstepSound();
        }
    }

    public void Walk(string foot)
    {
        if(foot == "Left") footObject = leftFoot;
        else footObject = rightFoot;


        if (tpInput.cc.inputMagnitude > 0.1f)
        {
            if (!tpControl.isSprinting)
            {
                locomotionType = 0f;
                FootstepSound();
            }
            else
            {
                locomotionType = 1f;
                FootstepSound();
            }

        }

    }

}
