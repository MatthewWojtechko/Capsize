using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoTransition : MonoBehaviour
{
    public float waitSeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitTransition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitTransition()
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene("Menu");
    }
}
