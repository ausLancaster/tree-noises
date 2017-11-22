using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextHighlighter : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Color highlightColor;
    public float displacement;

    private Color regularColor;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        regularColor = text.color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = highlightColor;
        transform.Translate(new Vector3(displacement, displacement, 0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = regularColor;
        transform.Translate(new Vector3(-displacement, -displacement, 0));
    }
}
