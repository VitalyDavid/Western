using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffVO : MonoBehaviour
{
    public FMODUnity.EventReference sheriffVOSample;
    private FMOD.Studio.EventInstance sheriffInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sheriffInstance = FMODUnity.RuntimeManager.CreateInstance(sheriffVOSample);
            sheriffInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
            sheriffInstance.start();
            sheriffInstance.release();
        }
    }
}
