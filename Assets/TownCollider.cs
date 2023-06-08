using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCollider : MonoBehaviour
{
    public FMODUnity.EventReference windSample;
    private FMOD.Studio.EventInstance ambInstance;
    private float ambSwitch;

    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        ambSwitch = 0f;
        ambInstance = FMODUnity.RuntimeManager.CreateInstance(windSample);
        ambInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position));
        ambInstance.setParameterByName("OutdoorAmb", ambSwitch);
        ambInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        ambInstance.setParameterByName("CameraHeight", cameraObject.transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ambSwitch != 0f)
        {
            ambSwitch = 0f;
            ambInstance.setParameterByName("OutdoorAmb", ambSwitch);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && ambSwitch != 1f)
        {
            ambSwitch = 1f;
            ambInstance.setParameterByName("OutdoorAmb", ambSwitch);
        }
    }
}
