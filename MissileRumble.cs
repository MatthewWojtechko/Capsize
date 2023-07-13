using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// uses code from CameraShake class

public class MissileRumble : MonoBehaviour
{
    public float amount = 0.1f;
    
    void Start()
    {
        StartCoroutine(Shake(amount));
    }

    void Update()
    {

    }

    IEnumerator Shake(float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        int counter = 0;
        float tempAmount = _amount;
        Vector3 targetVector = new Vector3(Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount), Random.Range(-tempAmount, tempAmount));

        while (true)
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
            if (counter > 10)
                counter = 0;
        }
    }
}
