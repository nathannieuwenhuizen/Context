using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinningBottle : MonoBehaviour
{
    private RectTransform rt;
    // Start is called before the first frame update
    private float speed = 7f;
    private float maxSpeed = 25f;
    private float cspeed = 0f;
    private AudioSource audios;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        audios = GetComponent<AudioSource>();
    }
    private IEnumerator SlowDown()
    {
        audios.volume = cspeed / maxSpeed * 0.2f;
        float waitTime = Random.value * 1f;
        yield return new WaitForSeconds(waitTime);
        while (cspeed > 0)
        {
            cspeed -= .1f;
            audios.volume = cspeed / maxSpeed * 0.2f;
            yield return new WaitForFixedUpdate();
        }
        audios.Stop();
    }

    public void Spin()
    {
        if (!audios.isPlaying)
        {
            audios.Play();
        }
        StopAllCoroutines();
        cspeed += speed;
        Handheld.Vibrate();

        cspeed = Mathf.Min(cspeed, maxSpeed);
        StartCoroutine(SlowDown());
    }
    // Update is called once per frame
    void Update()
    {
        if (cspeed > 0)
        {
            rt.transform.Rotate(new Vector3(0, 0, cspeed));
        }
    }
}
