using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject glow;
    public GameObject explosion;
    public GameObject[] doorOpenEffects;
    public Vector3 doorPositions;
    public enum Color { BLUE, GREEN, PINK, RED, ORANGE, YELLOW };
    public Color KeyColor;
    protected bool badPos = false;


    public virtual void Focused()
    {
        glow.SetActive(true);
    }

    public virtual void UnFocused()
    {
        glow.SetActive(false);
    }

    private void unlockDoors()
    {
        // effect toward door
        foreach (GameObject G in doorOpenEffects)
        {
            G.SetActive(true);
        }
        // boradcast
        if (KeyColor == Color.BLUE)
        {
            Messenger.Broadcast(GameEvent.BLUE_KEY);
        }
        else if (KeyColor == Color.GREEN)
        {
            Messenger.Broadcast(GameEvent.GREEN_KEY);
        }
        else if (KeyColor == Color.PINK)
        {
            Messenger.Broadcast(GameEvent.PINK_KEY);
        }
        else if (KeyColor == Color.RED)
        {
            Messenger.Broadcast(GameEvent.RED_KEY);
        }
        else if (KeyColor == Color.ORANGE)
        {
            Messenger.Broadcast(GameEvent.ORANGE_KEY);
        }
        else if (KeyColor == Color.YELLOW)
        {
            Messenger.Broadcast(GameEvent.YELLOW_KEY);
        }
    }
    public void Destroy()
    {
        unlockDoors();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
