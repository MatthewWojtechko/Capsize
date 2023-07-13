using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public bool smallShake = false;
    public bool destroyThisOnTrigger = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (smallShake)
            {
                Messenger.Broadcast(GameEvent.CAMERA_SHAKE_SMALL);
            }
            else
            {
                Messenger.Broadcast(GameEvent.CAMERA_SHAKE_SHORT);
            }
            if (destroyThisOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
