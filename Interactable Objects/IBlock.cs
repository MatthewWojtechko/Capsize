using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlock : Interactable
{
    AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            audioSources[0].Play();
        }
        else if (collision.collider.tag == "Glass")
        {
            audioSources[1].Play();
        }
    }
}
