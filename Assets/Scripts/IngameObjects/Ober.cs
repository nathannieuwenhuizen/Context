using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ober : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Session: " + SessionData.CSESSION);
        if (SessionData.CSESSION != null)
        {
            Debug.Log("Session exists!");

            UpdateSprite(SessionData.CSESSION.character);
        }
    }

    public void UpdateSprite(int val) {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[val];
    }
}
