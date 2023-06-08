using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class BreathingSystem : MonoBehaviour
{
    public FMODUnity.EventReference breathEvent;
    private FMOD.Studio.EventInstance breathInstance;

    private vThirdPersonInput tpInput;
    private vThirdPersonController tpControl;

    // Start is called before the first frame update
    void Start()
    {
        tpInput = GetComponent<vThirdPersonInput>();
        tpControl = GetComponent<vThirdPersonController>();
        breathInstance = FMODUnity.RuntimeManager.CreateInstance(breathEvent);
        breathInstance.start();
        breathInstance.release();
        
    }

    // Update is called once per frame
    void Update()
    {
        breathInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
        breathInstance.setParameterByName("BreathType", tpInput.cc.inputMagnitude);
        breathInstance.setParameterByName("Stamina", tpControl.currentStamina);


        //Debug.Log(tpControl.currentStamina);
        
    }
}
