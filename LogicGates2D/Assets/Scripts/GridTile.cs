using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private bool _wired;
    private bool _hovered;
    private Color _currentColor;
    private SpriteRenderer _spriteRenderer;

    private Color _noHoverTint = new Color(1f, 1f, 1f, 0);
    private Color _hoverTint;
    private Color _noHoverWire = new Color(1f, 1f, 1f, 1);
    private Color _hoverWire = new Color(220f / 255f, 210f / 255f, 40f / 255f, 1);

    public Sprite wireSprite;
    public Sprite baseSprite;

    public Color hoverTint = new Color(220f / 255f, 210f / 255f, 40f / 255f);

    [Range(0, 1)] public float hoveredTransparency = 0.3f;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log(_hoverWire);

        _hoverTint = new Color(hoverTint.r, hoverTint.g, hoverTint.b, hoveredTransparency);
        _spriteRenderer.color = _noHoverTint;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void UpdateColor()
    {
        if (_hovered)
        {
            if (_wired)
            {
                _spriteRenderer.color = _hoverWire;
            }
            else
            {
                _spriteRenderer.color = _hoverTint;
            }
        }
        else
        {
            if (_wired)
            {
                _spriteRenderer.color = _noHoverWire;
            }
            else
            {
                _spriteRenderer.color = _noHoverTint;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            _wired = !_wired;
            _spriteRenderer.sprite = wireSprite;
            Debug.Log("Hello World");
        }
        _hovered = true;
        UpdateColor();
    }

    private void OnMouseExit()
    {
        _hovered = false;
        UpdateColor();
    }

    private void OnMouseDown()
    {
        if (!_wired)
        {
            _wired = true;
            _spriteRenderer.sprite = wireSprite;
        }
        else
        {
            _wired = false;
            _spriteRenderer.sprite = baseSprite;
        }

        UpdateColor();
    }
}
