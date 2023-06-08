using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellSound : MonoBehaviour
{
    public FMODUnity.EventReference bellEvent;
    private FMOD.Studio.EventInstance bellInstance;

    // Start is called before the first frame update
    void Start()
    {
        bellInstance = FMODUnity.RuntimeManager.CreateInstance(bellEvent);
        bellInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
        bellInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
