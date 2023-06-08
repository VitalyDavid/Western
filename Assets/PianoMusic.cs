using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoMusic : MonoBehaviour
{
    public FMODUnity.EventReference pianoSample;
    private FMOD.Studio.EventInstance pianoInstance;

    // Start is called before the first frame update
    void Start()
    {
        pianoInstance = FMODUnity.RuntimeManager.CreateInstance(pianoSample);
        //pianoInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(pianoInstance, gameObject.GetComponent<Transform>(), gameObject.GetComponent<Rigidbody>());
        pianoInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        pianoInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
}
