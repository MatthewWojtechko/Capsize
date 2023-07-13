using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject World;
    Transform cam;
    Interactable objectToInteractWith;
    List<Interactable> objectsToInteractWith;
    GameObject currentBlock;
    AudioSource[] audioSources;
    Rigidbody blockRB;
    Rigidbody thisRigidBody;
    FirstPersonAIO firstPersonAIO;

    void Start()
    {
        cam = Camera.main.transform;
        objectsToInteractWith = new List<Interactable>();
        audioSources = GetComponents<AudioSource>();
        thisRigidBody = GetComponent<Rigidbody>();
        firstPersonAIO = GetComponent<FirstPersonAIO>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objectToInteractWith != null)
            {
                if (objectToInteractWith.tag == "Block")
                {
                    if (currentBlock == null)
                    {
                        firstPersonAIO.isHoldingBlock = true;
                        if (!audioSources[3].isPlaying)
                        {
                            audioSources[3].Play();
                        }
                        currentBlock = objectToInteractWith.gameObject;
                        currentBlock.transform.parent = transform;
                        currentBlock.transform.localPosition = new Vector3(0, 0, 1.5f);
                        blockRB = currentBlock.GetComponent<Rigidbody>();
                        blockRB.useGravity = false;
                    }
                    else
                    {
                        firstPersonAIO.isHoldingBlock = false;
                        audioSources[3].Stop();
                        currentBlock.transform.parent = World.transform;
                        blockRB.useGravity = true;// = false;
                        blockRB = null;
                        currentBlock = null;
                    }
                }
                else if (objectToInteractWith.tag == "Lever")
                {
                    objectToInteractWith.GetComponent<LeverFlip>().Flip();
                }
                else if (objectToInteractWith.tag == "Pickup")
                {
                    // This destroys the pickup (removing it from the list and nullifying objectToInteractWith
                    // and increments the pickup count in GameMaster
                    Messenger.Broadcast(GameEvent.PICK_UP);
                    objectToInteractWith.Destroy();
                    objectsToInteractWith.Remove(objectToInteractWith);
                    objectToInteractWith = null;
                }
            }
            else // object to interact with is null
            {
                if(currentBlock != null) // This take care of the edge case where the block trigger was exited, but current block is not null
                {
                    firstPersonAIO.isHoldingBlock = false;
                    audioSources[3].Stop();
                    currentBlock.transform.parent = World.transform;
                    blockRB.useGravity = true;// = false;
                    blockRB = null;
                    currentBlock = null;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (blockRB != null)
        {
            currentBlock.transform.localPosition = Vector3.Lerp(currentBlock.transform.localPosition, new Vector3(0, 0, 1.5f), 0.05f);
            blockRB.velocity = thisRigidBody.velocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block" && currentBlock == null)
        {
            objectToInteractWith?.UnFocused();
            objectToInteractWith = other.GetComponent<Interactable>();
            objectToInteractWith.Focused();
            objectsToInteractWith.Add(objectToInteractWith);
        }
        else if (other.tag == "Lever" && currentBlock == null)
        {
            objectToInteractWith?.UnFocused();
            objectToInteractWith = other.GetComponent<Interactable>();
            objectToInteractWith.Focused();
            objectsToInteractWith.Add(objectToInteractWith);
        }
        else if (other.tag == "Pickup" && currentBlock == null)
        {
            objectToInteractWith?.UnFocused();
            objectToInteractWith = other.GetComponent<Interactable>();
            objectToInteractWith.Focused();
            objectsToInteractWith.Add(objectToInteractWith);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            objectsToInteractWith.Remove(other.GetComponent<Interactable>());
            if (objectsToInteractWith.Count <= 0)
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = null;
            }
            else
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = objectsToInteractWith[0];
                objectToInteractWith.Focused();
            }
        }
        else if (other.tag == "Lever")
        {
            objectsToInteractWith.Remove(other.GetComponent<Interactable>());
            if (objectsToInteractWith.Count <= 0)
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = null;
            }
            else
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = objectsToInteractWith[0];
                objectToInteractWith.Focused();
            }
        }
        else if (other.tag == "Pickup")
        {
            objectsToInteractWith.Remove(other.GetComponent<Interactable>());
            if (objectsToInteractWith.Count <= 0)
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = null;
            }
            else
            {
                objectToInteractWith?.UnFocused();
                objectToInteractWith = objectsToInteractWith[0];
                objectToInteractWith.Focused();
            }
        }
    }
}
