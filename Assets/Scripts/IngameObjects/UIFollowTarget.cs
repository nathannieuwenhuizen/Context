using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform PositionToFollow;
    [SerializeField] public Canvas canvas;

    private RectTransform rTransform;
    private void Start()
    {
        rTransform = GetComponent<RectTransform>();

        float tileWidth = (float)PositionToFollow.gameObject.GetComponent<SpriteRenderer>().size.x;
        float tileHeight = (float)PositionToFollow.gameObject.GetComponent<SpriteRenderer>().size.y;
        //Debug.Log(tileWidth + " | " + tileHeight);
        //Debug.Log(worldToUISpace(canvas, new Vector2(tileWidth, tileHeight)));
        rTransform.sizeDelta = new Vector2(tileWidth * 1000, tileHeight * 1000);
    }
    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
    // Update is called once per frame
    void Update()
    {
        rTransform.position = worldToUISpace(canvas, PositionToFollow.position);
    }
    public Transform Target
    {
        get { return PositionToFollow; }
        set { PositionToFollow = value; }
    }
    public string Text
    {
        get { return GetComponent<Text>().text; }
        set { GetComponent<Text>().text = value; }
    }
}
