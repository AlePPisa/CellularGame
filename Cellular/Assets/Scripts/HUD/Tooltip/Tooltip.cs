using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public VerticalLayoutGroup verticalLayoutGroup;
    private RectTransform _rectTransform;
    public int characterWrapLimit;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
            verticalLayoutGroup.padding.top = 8;
        }
        else
        {
            headerField.gameObject.SetActive(true);
            verticalLayoutGroup.padding.top = 5;
            headerField.text = header;
        }

        contentField.text = content;
    }
    private void Update()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
