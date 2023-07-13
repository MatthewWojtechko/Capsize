using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public Transform world;
    public GameObject player;
    public float speed = 30;
    public bool isFlipping;

    AudioSource audioSource;
    bool worldIsUpsideDown = false;
    Rigidbody playerRB;

    void Awake()
    {
        Messenger.AddListener(GameEvent.FLIP, FlipWorld);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRB = player.GetComponent<Rigidbody>();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.FLIP, FlipWorld);
    }

    public void FlipWorld()
    {
        if (!isFlipping)
        {
            audioSource.Play();
            Messenger.Broadcast(GameEvent.CAMERA_SHAKE);
            if (worldIsUpsideDown)
            {
                StartCoroutine(RotateWorldRightSideUp());
            }
            else
            {
                StartCoroutine(RotateWorldUpsideDown());
            }
        }
    }

    // Flips the world upside down
    IEnumerator RotateWorldUpsideDown()
    {
        isFlipping = true;
        playerRB.useGravity = false;
        Vector3 temp = player.transform.forward;

        float counter = 0;

        while (counter < 180)
        {
            counter += speed;
            world.RotateAround(player.transform.position, temp, speed);
            yield return new WaitForSeconds(0.01f);
        }
        playerRB.useGravity = true;
        worldIsUpsideDown = true;
        isFlipping = false;
    }

    // Flips the world rightside up
    IEnumerator RotateWorldRightSideUp()
    {
        isFlipping = true;
        playerRB.useGravity = false;
        Vector3 temp = player.transform.forward.normalized;
        float counter = 180;
        while (counter > 0)
        {
            counter -= speed;
            world.RotateAround(player.transform.position, -temp, speed);
            yield return new WaitForSeconds(0.01f);
        }
        playerRB.useGravity = true;
        worldIsUpsideDown = false;
        isFlipping = false;
    }

    // Resets values, after a short time so as to prevent flipping the world amid a flip.
    IEnumerator WaitFinishFlip()
    {
        yield return new WaitForSeconds(2);
        isFlipping = true;
    }
}
