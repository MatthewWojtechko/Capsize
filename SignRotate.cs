using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes the sign flip whenever it is time and when the player is in range.
public class SignRotate : MonoBehaviour
{
    public Animator rotAnim;
    public GameObject FX;
    public bool readyToFlip;


    void Awake()
    {
        Messenger.AddListener(GameEvent.FINAL_PUZZLE, setReady);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.FINAL_PUZZLE, setReady);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setReady()
    {
        readyToFlip = true;
    }

    void OnTriggerEnter(Collider c)
    {
        if (readyToFlip && c.gameObject.tag == "Player")
        {
            rotAnim.SetBool("rotate", true);
            FX.SetActive(true);
            Messenger.Broadcast(GameEvent.SIGN_ROTATE);
            readyToFlip = false;
        }
    }
}
