using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaloonDoor : MonoBehaviour
{
    public FMODUnity.EventReference doorSampleOpen;
    public FMODUnity.EventReference doorSampleClosed;
    private FMOD.Studio.EventInstance doorInstance;
    private FMOD.Studio.PLAYBACK_STATE pbState;
    
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorInstance.getPlaybackState(out pbState);
            if(pbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                doorInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                doorInstance.release();
            }
            doorInstance = FMODUnity.RuntimeManager.CreateInstance(doorSampleOpen);
            doorInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
            doorInstance.start();
            doorInstance.release();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorInstance.getPlaybackState(out pbState);
            if (pbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                doorInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                doorInstance.release();
            }
            doorInstance = FMODUnity.RuntimeManager.CreateInstance(doorSampleClosed );
            doorInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
            doorInstance.start();
            doorInstance.release();
        }
    }
}
