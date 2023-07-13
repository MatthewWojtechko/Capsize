using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float amount = 0.1f;
    public float smallAmount = 0.1f;
    public float length = 1f;
    public float shortLength = 0.8f;
    public float longLength = 6;

    AudioSource audioSource;

    void Awake()
    {
        Messenger.AddListener(GameEvent.CAMERA_SHAKE, ShakeCamera);
        Messenger.AddListener(GameEvent.CAMERA_SHAKE_SHORT, ShakeCameraShort);
        Messenger.AddListener(GameEvent.CAMERA_SHAKE_LONG, ShakeCameraLong);
        Messenger.AddListener(GameEvent.CAMERA_SHAKE_SMALL, ShakeCameraSmall);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CAMERA_SHAKE, ShakeCamera);
        Messenger.RemoveListener(GameEvent.CAMERA_SHAKE_SHORT, ShakeCameraShort);
        Messenger.RemoveListener(GameEvent.CAMERA_SHAKE_LONG, ShakeCameraLong);
        Messenger.RemoveListener(GameEvent.CAMERA_SHAKE_SMALL, ShakeCameraSmall);
    }

    void ShakeCamera()
    {
        StartCoroutine(Shake(length, amount));
    }

    void ShakeCameraShort()
    {
        StartCoroutine(Shake(shortLength, amount));
    }

    void ShakeCameraLong()
    {
        StartCoroutine(Shake(longLength, amount));
    }

    void ShakeCameraSmall()
    {
        StartCoroutine(Shake(shortLength, smallAmount));
    }

    IEnumerator Shake(float _length, float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        int counter = 0;
        float tempAmount = _amount;
        Vector3 targetVector = new Vector3(Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount));

        while (counter < _length)
        {
            if (counter % 5 == 0)
            {
                if (counter % 10 == 0)
                {
                    targetVector = originalPos;
                }
                else
                {
                    targetVector = new Vector3(Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount));
                }
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetVector, 0.5f);
            yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();
            tempAmount *= 0.96f;
            counter++;
        }
        transform.localPosition = originalPos;
        audioSource.Stop();
    }
}
