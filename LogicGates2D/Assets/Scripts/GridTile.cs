using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private bool _hovered;
    private bool _highlighted;
    private SpriteRenderer _spriteRenderer;

    public Color baseColor = new Color(220, 210, 40, 0);

    [Range(0, 1)] public float hoveredTransparency = 0.3f;

    [Range(0, 1)] public float highlightedTransparency = 0.6f;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = baseColor;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void UpdateColor()
    {
        var transparency = _highlighted
            ? highlightedTransparency
            : _hovered
                ? hoveredTransparency
                : 0;
        _spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, transparency);
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            _highlighted = true;
        }
        else
        {
            _hovered = true;
        }
        UpdateColor();
    }

    private void OnMouseExit()
    {
        _hovered = false;
        UpdateColor();
    }

    private void OnMouseDown()
    {
        _highlighted = true;
        UpdateColor();
    }

    private void OnMouseUp()
    {
        _highlighted = false;
        UpdateColor();
    }
}
