using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnim : MonoBehaviour
{
    public float lowWaitTime;
    public float highWaitTime;
    public float waitTimeOffset;
    public float letterSpeed;
    public LetterRotate[] LetterScripts;

    private float currentWaitTime;
    private bool makeWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        currentWaitTime = 1;
        float speed = letterSpeed;
        int r = Random.Range(1, 3);
        if (r == 2)
            speed *= -1;
        float tempWait = 0;
        foreach (LetterRotate L in LetterScripts)
        {
            L.beginFlip(currentWaitTime + tempWait, speed);
            tempWait += waitTimeOffset;
        }
        StartCoroutine(waitForFinish());
    }

    // Update is called once per frame
    void Update()
    {
        if (makeWaitTime)
        {
            float tempWait = 0;
            float speed = letterSpeed;
            int r = Random.Range(1, 3);
            if (r == 2)
                speed *= -1;
            currentWaitTime = Random.Range(lowWaitTime, highWaitTime);

            foreach (LetterRotate L in LetterScripts)
            {
                L.beginFlip(currentWaitTime + tempWait, speed);
                tempWait += waitTimeOffset;
            }
            StartCoroutine(waitForFinish());
        }
    }

    IEnumerator waitForFinish()
    {
        makeWaitTime = false;
        yield return new WaitForSeconds(currentWaitTime + 2);
        makeWaitTime = true;
    }
}
