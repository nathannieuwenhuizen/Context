using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicScreen : MonoBehaviour
{
    [SerializeField]
    private float slideSpeed = 5;

    [SerializeField]
    private bool slidesWhenActive = false;

    private RectTransform rt;
    private Vector3 startPos;
    private Quaternion startRot;
    private Quaternion endRot;
    private float screenWidth = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        startPos = rt.position;
        startRot = rt.rotation;

        rt.Rotate(new Vector3(0, 0, 10));
        endRot = rt.rotation;
        rt.Rotate(new Vector3(0, 0, -10));

        if (slidesWhenActive)
        {
            SlideIn();
        }
    }

    public void SlideIn()
    {
        StopAllCoroutines();
        rt.Translate(new Vector2(-screenWidth, 0));
        rt.rotation = startRot;
        rt.Rotate(new Vector3(0, 0, -10));
        StartCoroutine(SlidingIn());
    }
    IEnumerator SlidingIn()
    {
        while (Mathf.Abs( rt.position.x - startPos.x ) > 10f )
        {
            rt.position = Vector3.Lerp(rt.position, startPos, Time.deltaTime * slideSpeed);
            rt.rotation = Quaternion.Slerp(rt.rotation, startRot, Time.deltaTime * slideSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ResetTransform();
    }
    public void SlideOut()
    {
        StopAllCoroutines();
        ResetTransform();
        StartCoroutine(SlidingOut());
    }
    public void ResetTransform()
    {
        if (rt == null)
        {
            rt = GetComponent<RectTransform>();
            startPos = rt.position;
            startRot = rt.rotation;

            rt.Rotate(new Vector3(0, 0, 10));
            endRot = rt.rotation;
            rt.Rotate(new Vector3(0, 0, -10));

        }
        rt.position = startPos;
        rt.rotation = startRot;
    }
    IEnumerator SlidingOut()
    {
        while (Mathf.Abs(rt.position.x - (startPos + new Vector3(screenWidth, 0)).x) > 10f)
        {
            rt.position = Vector3.Lerp(rt.position, startPos + new Vector3(screenWidth, 0), Time.deltaTime * slideSpeed);
            rt.rotation = Quaternion.Slerp(rt.rotation, endRot, Time.deltaTime * slideSpeed);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        rt.position = startPos + new Vector3(screenWidth, 0);
        gameObject.SetActive(false);
    }
}
