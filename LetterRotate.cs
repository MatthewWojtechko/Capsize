using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterRotate : MonoBehaviour
{
    public Animator anim;

    private float waitTime;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isFlipTime"))
            anim.SetBool("isFlipTime", false);    
    }

    public void beginFlip(float w, float s)
    {
        waitTime = w;
        speed = s;
        StartCoroutine(waitFlip());
    }

    IEnumerator waitFlip()
    {
        anim.SetBool("isFlipTime", false);
        anim.SetFloat("speed", speed);
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("isFlipTime", true);
        StartCoroutine(quitFlip(speed));
    }

    IEnumerator quitFlip(float s)
    {
        yield return new WaitForSeconds(Mathf.Abs(s));
        anim.SetBool("isFlipTime", false); 
    }
}
