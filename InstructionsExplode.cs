using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsExplode : MonoBehaviour
{
    public GameObject explosion;
    public Renderer rend;
    private bool wPressed;
    private bool spacePressed;
    private bool explosionStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
            wPressed = true;
        if (Input.GetKey("space"))
            spacePressed = true;

        if (wPressed && spacePressed && !explosionStarted)
            StartCoroutine(waitExplode());
    }

    IEnumerator waitExplode()
    {
        explosionStarted = true;
        yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        explosion.SetActive(true);
        Messenger.Broadcast(GameEvent.CAMERA_SHAKE_SHORT);
        rend.enabled = false;
        this.enabled = false;
    }


}
