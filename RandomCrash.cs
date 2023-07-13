using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCrash : MonoBehaviour
{
    public float minWait;
    public float maxWait;
    public float minWaitInc;
    public float maxWaitInc;
    public Transform playerTrans;

    private bool currentlyWaiting;
    private AudioSource[] SFX;

    void Awake()
    {
        Messenger.AddListener(GameEvent.SIGN_ROTATE, increaseCrashes);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SIGN_ROTATE, increaseCrashes);
    }

    // Start is called before the first frame update
    void Start()
    {
        SFX = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyWaiting)
        {
            float w = Random.Range(minWait, maxWait);
            StartCoroutine(waitExplode(w));
        }
    }

    IEnumerator waitExplode(float w)
    {
        currentlyWaiting = true;
        yield return new WaitForSeconds(w);

        this.transform.position = new Vector3(playerTrans.position.x + Random.Range(0, 10f), playerTrans.position.y, playerTrans.position.z + Random.Range(0, 10f));

        SFX[Random.Range(0, 3)].Play();
        int exp = Random.Range(0, 3);
        if (exp == 0)
            Messenger.Broadcast(GameEvent.CAMERA_SHAKE_SHORT);
        else if (exp == 1)
            Messenger.Broadcast(GameEvent.CAMERA_SHAKE_LONG);
        else
            Messenger.Broadcast(GameEvent.CAMERA_SHAKE_SMALL);
        currentlyWaiting = false;
    }

    void increaseCrashes()
    {
        minWait = minWaitInc;
        maxWait = maxWaitInc;
    }
}
