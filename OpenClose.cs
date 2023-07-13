using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    //public enum OpenStatus {CLOSED, OPEN, CLOSING, OPENING};
    //public OpenStatus status = OpenStatus.CLOSED;
    public enum ColorType { BLUE, GREEN, PINK, RED, ORANGE, YELLOW, RED_ORANGE_YELLOW };
    public ColorType ColorCode;
    public bool isOpen;
    public int keyNum;
    public Animator animator;
    public float waitUnlockSec;
    public bool isUnlocked;
    public GameObject player;
    public float openDistance;
    [Tooltip("If final, should be RED")]
    public ParticleSystem barrierPart;
    [Tooltip("If final, should be YELLOW")]
    public ParticleSystem barrierPart2;
    [Tooltip("If final, should be ORANGE")]
    public ParticleSystem barrierPart3;
    public ParticleSystem openExpPart;
    private ParticleSystem barrierToUnlock;
    public GameObject finalPlatforms;
    public GameObject door1;
    public GameObject door2;
    public AudioSource endSFX1;
    public AudioSource endSFX2;

    public bool initialOpen;
    public bool dOpen;
    public bool dClose;

    AudioSource[] audioSources;
    // status: 0 = closed, 1 = open, 2 = closing, 3 = opening

    private void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        Renderer rend1 = door1.GetComponent<Renderer>();
        Renderer rend2 = door2.GetComponent<Renderer>();
        if (ColorCode == ColorType.BLUE)
        {
            Messenger.AddListener(GameEvent.BLUE_KEY, unlock);
            rend.material.color = new Color(0, 0, 1, 1);
            rend1.material.color = new Color(0.26f, 0.77f, 0.96f, 1);
            rend2.material.color = new Color(0.26f, 0.77f, 0.96f, 1);
        }
        else if (ColorCode == ColorType.GREEN)
        {
            Messenger.AddListener(GameEvent.GREEN_KEY, unlock);
            rend.material.color = new Color(0, 1, 0, 1);
            rend1.material.color = new Color(0.26f, 0.96f, 0.45f, 1);
            rend2.material.color = new Color(0.26f, 0.96f, 0.45f, 1);

        }
        else if (ColorCode == ColorType.PINK)
        {
            Messenger.AddListener(GameEvent.PINK_KEY, unlock);
            rend.material.color = new Color(0.86f, 0.26f, 0.96f, 1);
            rend1.material.color = new Color(0.94f, 0.61f, 1, 1);
            rend2.material.color = new Color(0.94f, 0.61f, 1, 1);

        }
        else if (ColorCode == ColorType.RED)
        {
            Messenger.AddListener(GameEvent.RED_KEY, unlock);
        }
        else if (ColorCode == ColorType.ORANGE)
        {
            Messenger.AddListener(GameEvent.ORANGE_KEY, unlock);
        }
        else if (ColorCode == ColorType.YELLOW)
        {
            Messenger.AddListener(GameEvent.YELLOW_KEY, unlock);
        }
        else if (ColorCode == ColorType.RED_ORANGE_YELLOW)
        {
            Messenger.AddListener(GameEvent.YELLOW_KEY, yellowUnlock);
            Messenger.AddListener(GameEvent.ORANGE_KEY, orangeUnlock);
            Messenger.AddListener(GameEvent.RED_KEY, redUnlock);
        }

        barrierToUnlock = barrierPart;
    }

    private void OnDestroy()
    {
        if (ColorCode == ColorType.BLUE)
        {
            Messenger.RemoveListener(GameEvent.BLUE_KEY, unlock);
        }
        else if (ColorCode == ColorType.GREEN)
        {
            Messenger.RemoveListener(GameEvent.GREEN_KEY, unlock);
        }
        else if (ColorCode == ColorType.PINK)
        {
            Messenger.RemoveListener(GameEvent.PINK_KEY, unlock);
        }
        else if (ColorCode == ColorType.RED)
        {
            Messenger.RemoveListener(GameEvent.RED_KEY, unlock);
        }
        else if (ColorCode == ColorType.ORANGE)
        {
            Messenger.RemoveListener(GameEvent.ORANGE_KEY, unlock);
        }
        else if (ColorCode == ColorType.YELLOW)
        {
            Messenger.RemoveListener(GameEvent.YELLOW_KEY, unlock);
        }
        else if (ColorCode == ColorType.RED_ORANGE_YELLOW)
        {
            Messenger.RemoveListener(GameEvent.YELLOW_KEY, yellowUnlock);
            Messenger.RemoveListener(GameEvent.ORANGE_KEY, orangeUnlock);
            Messenger.RemoveListener(GameEvent.RED_KEY, redUnlock);
        }
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (initialOpen)
        {
            if (!isOpen)
            {
                isOpen = true;
                open();
            }
            else if ((Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position) <= openDistance))
            {
                initialOpen = false;
            }
        }
        else if (isUnlocked)
        {
            if (Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position) <= openDistance)
            {
                if (!isOpen)
                    open();
            }
            else
            {
                if (isOpen)
                    close();
            }
        }

    }
    
    public void open()
    {
        audioSources[1].Play(); // door open
        isOpen = true;
        animator.SetBool("isOpen", true);
    }

    public void close()
    {
        audioSources[1].Play(); // door close
        isOpen = false;
        animator.SetBool("isOpen", false);
    }

    public void reLock()
    {
        isUnlocked = false;
        audioSources[2].Play();
        isOpen = false;
        animator.SetBool("isOpen", false);
        barrierPart.Play();
    }

    private void orangeUnlock()
    {
        barrierToUnlock = barrierPart3;
        unlock();
    }

    private void yellowUnlock()
    {
        barrierToUnlock = barrierPart2;
        unlock();
    }

    private void redUnlock()
    {
        barrierToUnlock = barrierPart;
        unlock();
    }

    public void unlock()
    {

        StartCoroutine(waitToUnlock());
    }

    IEnumerator waitToUnlock()
    {
        yield return new WaitForSeconds(waitUnlockSec);

        audioSources[0].Stop(); // barrier sound
        barrierToUnlock.Stop();
        openExpPart.Play();
        if (ColorCode == ColorType.RED_ORANGE_YELLOW)
        {
            if (!barrierPart.isEmitting && !barrierPart2.isEmitting && !barrierPart3.isEmitting)
            {
                isUnlocked = true;
                finalPlatforms.SetActive(true);//
                audioSources[2].Play();
                prepareEndgame();
            }
        }
        else
        {
            initialOpen = true;
            isUnlocked = true;
            audioSources[3].Play();
        }
    }


    void prepareEndgame()
    {
        Messenger.Broadcast(GameEvent.FINAL_PUZZLE);
        Messenger.Broadcast(GameEvent.CAMERA_SHAKE_LONG);
        endSFX1.Play();
        endSFX2.Play();
    }
}
