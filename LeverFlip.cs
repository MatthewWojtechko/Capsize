using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverFlip : MonoBehaviour
{
    public float speed;
    public GameObject pivot;
    public float rotationEnd = 13;
    public bool endLevel = false;

    private bool flipped = false;
    private bool isFlipping = false;
    private AudioSource audioSource;
    private float target;
    private float currentMovement;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pivot.transform.localEulerAngles = new Vector3(0, 0, rotationEnd);
    }

    // Call to flip the lever, resulting in lever movement and the world to flip.
    public void Flip()
    {
        if (!isFlipping)
        {
            StartCoroutine(FlipMotion());
            audioSource.Play();
        }
    }

    IEnumerator FlipMotion()
    {
        target = flipped ? rotationEnd : -rotationEnd;
        flipped = !flipped;
        isFlipping = true;
        while (true)
        {
            float currentRot = pivot.transform.localEulerAngles.z;
            currentRot = (currentRot > 180) ? currentRot - 360 : currentRot; // get negative version if applicable
            currentMovement = speed * Time.deltaTime;

            if (currentRot > target)
                currentMovement *= -1;
            if ((target < 0 && currentRot + currentMovement <= target) || (target > 0 && currentRot + currentMovement >= target))
            {
                if (endLevel)
                {
                    Messenger.Broadcast(GameEvent.END_LEVEL);
                    break;
                }
                else
                {
                    pivot.transform.localEulerAngles = new Vector3(0, 0, target);
                    Messenger.Broadcast(GameEvent.FLIP);
                    break;
                }
            }
            else
            {
                pivot.transform.localEulerAngles = new Vector3(0, 0, currentRot + currentMovement);
            }
            yield return new WaitForSeconds(0.01f);
        }
        isFlipping = false;
    }
}