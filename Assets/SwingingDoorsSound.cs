using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingDoorsSound : MonoBehaviour
{
    public HingeJoint leftHinge;
    public HingeJoint rightHinge;
    public GameObject leftDoor, rightDoor;
    private float previousLeftDoorRotation;
    private float previousRightDoorRotation;
    private bool playedOpenSound = false;
    private bool playedCloseSound = false;
    private FMOD.Studio.EventInstance openSoundEmitter;
    private FMOD.Studio.EventInstance closeSoundEmitter;
    public FMODUnity.EventReference openDoorEvent, closeDoorEvent;
    private float speed;
    private GameObject character;

    private void Start()
    {
        // Set initial rotation values for previous door position
        previousLeftDoorRotation = leftDoor.transform.localRotation.eulerAngles.y;
        previousRightDoorRotation = rightDoor.transform.localRotation.eulerAngles.y;
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Debug.Log(speed);
        // Check if the character is touching the left door
        if (leftDoor.GetComponent<BoxCollider>().bounds.Contains(character.transform.position))
        {
            // Determine the direction that the character is moving the door
            float direction = Mathf.Sign(leftHinge.velocity * character.transform.forward.z);

            // Create FMOD instance and attach to left door
            openSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(openDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(openSoundEmitter, leftDoor.GetComponent<Transform>(), leftDoor.GetComponent<Rigidbody>());

            // Set door speed and direction parameters on FMOD event instance
            speed = Mathf.Abs(leftHinge.velocity);
            openSoundEmitter.setParameterByName("speed", speed);
            openSoundEmitter.setParameterByName("direction", direction);

            // Play sound and release instance
            openSoundEmitter.start();
            openSoundEmitter.release();
        }

        // Check if the character is touching the right door
        if (rightDoor.GetComponent<BoxCollider>().bounds.Contains(character.transform.position))
        {
            // Determine the direction that the character is moving the door
            float direction = Mathf.Sign(rightHinge.velocity * character.transform.forward.z);

            // Create FMOD instance and attach to right door
            openSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(openDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(openSoundEmitter, rightDoor.GetComponent<Transform>(), rightDoor.GetComponent<Rigidbody>());

            // Set door speed and direction parameters on FMOD event instance
            speed = Mathf.Abs(rightHinge.velocity);
            openSoundEmitter.setParameterByName("speed", speed);
            openSoundEmitter.setParameterByName("direction", direction);

            // Play sound and release instance
            openSoundEmitter.start();
            openSoundEmitter.release();
        }

        // Check if the left door is opening
        if (leftHinge.angle > 0.1f && !playedOpenSound && leftHinge.velocity < 0)
        {
            // Create FMOD instance and attach to left door
            openSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(openDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(openSoundEmitter, leftDoor.GetComponent<Transform>(), leftDoor.GetComponent<Rigidbody>());
            // Set door speed parameter on FMOD event instance
            speed = Mathf.Abs(leftHinge.velocity);
            openSoundEmitter.setParameterByName("speed", speed);
            // Play sound and release instance
            openSoundEmitter.start();
            openSoundEmitter.release();
            playedOpenSound = true;
            playedCloseSound = false;
        }

        // Check if the left door is closing
        if (leftHinge.angle < -0.1f && !playedCloseSound && leftHinge.velocity > 0)
        {
            // Create FMOD instance and attach to left door
            closeSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(closeDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(closeSoundEmitter, leftDoor.GetComponent<Transform>(), leftDoor.GetComponent<Rigidbody>());
            // Set door speed parameter on FMOD event instance
            speed = Mathf.Abs(leftHinge.velocity);
            closeSoundEmitter.setParameterByName("speed", speed);
            // Play sound and release instance
            closeSoundEmitter.start();
            closeSoundEmitter.release();
            playedCloseSound = true;
            playedOpenSound = false;
        }

        // Check if the right door is opening
        if (rightHinge.angle < -0.1f && !playedOpenSound && rightHinge.velocity > 0)
        {
            // Create FMOD instance and attach to right door
            openSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(openDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(openSoundEmitter, rightDoor.GetComponent<Transform>(), rightDoor.GetComponent<Rigidbody>());
            // Set door speed parameter on FMOD event instance
            speed = Mathf.Abs(rightHinge.velocity);
            openSoundEmitter.setParameterByName("speed", speed);
            // Play sound and release instance
            openSoundEmitter.start();
            openSoundEmitter.release();
            playedOpenSound = true;
            playedCloseSound = false;
        }

        // Check if the right door is closing
        if (rightHinge.angle > 0.1f && !playedCloseSound && rightHinge.velocity < 0)
        {
            // Create FMOD instance and attach to right door
            closeSoundEmitter = FMODUnity.RuntimeManager.CreateInstance(closeDoorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(closeSoundEmitter, rightDoor.GetComponent<Transform>(), rightDoor.GetComponent<Rigidbody>());
            // Set door speed parameter on FMOD event instance
            speed = Mathf.Abs(rightHinge.velocity);
            closeSoundEmitter.setParameterByName("speed", speed);
            // Play sound and release instance
            closeSoundEmitter.start();
            closeSoundEmitter.release();
            playedCloseSound = true;
            playedOpenSound = false;
        }

        // Reset played sound flags when the doors stop moving
        if (Mathf.Abs(leftHinge.velocity) < 0.1f && Mathf.Abs(rightHinge.velocity) < 0.1f)
        {
            playedOpenSound = false;
            playedCloseSound = false;
        }

        // Check if the left door is moving forward or backward
        float newLeftDoorRotation = leftDoor.transform.localRotation.eulerAngles.y;
        if (newLeftDoorRotation > previousLeftDoorRotation)
        {
            // Left door is moving forward
            Debug.Log("Left door is moving forward");
        }
        else if (newLeftDoorRotation < previousLeftDoorRotation)
        {
            // Left door is moving backward
            Debug.Log("Left door is moving backward");
        }
        previousLeftDoorRotation = newLeftDoorRotation;

        // Check if the right door is moving forward or backward
        float newRightDoorRotation = rightDoor.transform.localRotation.eulerAngles.y;
        if (newRightDoorRotation > previousRightDoorRotation)
        {
            // Right door is moving forward
            Debug.Log("Right door is moving forward");
        }
        else if (newRightDoorRotation < previousRightDoorRotation)
        {
            // Right door is moving backward
            Debug.Log("Right door is moving backward");
        }
        previousRightDoorRotation = newRightDoorRotation;
    }
}