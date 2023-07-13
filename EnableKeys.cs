using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableKeys : MonoBehaviour
{
    public GameObject[] keys;
    public Material[] mat;
    public ParticleSystem[] partEffect;
    public AudioSource SFX;

    public Vector3 partScale;
    public Vector3 localPos;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.YELLOW_KEY, EnableYellowKey);
        Messenger.AddListener(GameEvent.ORANGE_KEY, EnableOrangeKey);
        Messenger.AddListener(GameEvent.RED_KEY, EnableRedKey);
        Messenger.AddListener(GameEvent.GREEN_KEY, EnableGreenKey);
        Messenger.AddListener(GameEvent.PINK_KEY, EnablePinkKey);
        Messenger.AddListener(GameEvent.BLUE_KEY, EnableBlueKey);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.YELLOW_KEY, EnableYellowKey);
        Messenger.RemoveListener(GameEvent.ORANGE_KEY, EnableOrangeKey);
        Messenger.RemoveListener(GameEvent.RED_KEY, EnableRedKey);
        Messenger.RemoveListener(GameEvent.GREEN_KEY, EnableGreenKey);
        Messenger.RemoveListener(GameEvent.PINK_KEY, EnablePinkKey);
        Messenger.RemoveListener(GameEvent.BLUE_KEY, EnableBlueKey);
    }


    void EnableYellowKey()
    {
        StartCoroutine(waitEffect(0));

    }
    void EnableOrangeKey()
    {
        StartCoroutine(waitEffect(1));

    }
    void EnableRedKey()
    {
        StartCoroutine(waitEffect(2));

    }
    void EnableGreenKey()
    {
        StartCoroutine(waitEffect(3));

    }
    void EnablePinkKey()
    {
        StartCoroutine(waitEffect(4));

    }
    void EnableBlueKey()
    {
        StartCoroutine(waitEffect(5));
    }

    IEnumerator waitEffect(int index)
    {
        yield return new WaitForSeconds(2);
        keys[index].GetComponent<MeshRenderer>().material = mat[index];
        ParticleSystem part = Instantiate(partEffect[index]);
        part.transform.parent = keys[index].transform;
        part.transform.localScale = partScale;
        part.transform.localPosition = localPos;
        SFX.Play();
    }
}
