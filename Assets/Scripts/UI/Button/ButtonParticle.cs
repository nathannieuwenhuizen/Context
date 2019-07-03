using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParticle : MonoBehaviour
{

    [SerializeField]
    private Image particleImage;

    [SerializeField]
    private float fadeSpeed = 0.05f;

    [SerializeField]
    private float scaleSpeed = 0.1f;
    public void EmitParticle()
    {
        StopAllCoroutines();
        particleImage.gameObject.transform.localScale = new Vector3(1,1,1);
        particleImage.color = new Color(1, 1, 1, 1);
        StartCoroutine(Emitting());
    }
    IEnumerator Emitting ()
    {
        while (particleImage.color.a > 0)
        {
            particleImage.color = new Color(1,1,1, particleImage.color.a - fadeSpeed);
            float scalex = particleImage.gameObject.transform.localScale.x + scaleSpeed;
            float scaley = particleImage.gameObject.transform.localScale.y + scaleSpeed * 1.5f;
            particleImage.gameObject.transform.localScale = new Vector3(scalex, scaley);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
