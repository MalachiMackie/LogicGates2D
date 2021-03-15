using UnityEngine;
using UnityEngine.Serialization;

public class GridTile : MonoBehaviour
{
    private bool _wired;
    private bool _hovered;
    private Color _currentColor;
    private SpriteRenderer _spriteRenderer;

    private readonly Color _noHoverTint = new Color(1f, 1f, 1f, 0);
    private readonly Color _noHoverWire = new Color(1f, 1f, 1f, 1);
    private readonly Color _hoverWire = new Color(220f / 255f, 210f / 255f, 40f / 255f, 1);
    private Color _hoverTint;

    private GridTile _northNeighbour;
    private GridTile _eastNeighbour;
    private GridTile _southNeighbour;
    private GridTile _westNeighbour;

    private Color CurrentColor
    {
        set
        {
            _currentColor = value;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _currentColor;
            }
        }
    }


    [Header("Sprites")]
    public Sprite wireNoConnection;
    public Sprite wireOneConnection;
    public Sprite wireTwoConnectionLine;
    public Sprite wireTwoConnectionCorner;
    public Sprite wireThreeConnection;
    public Sprite wireFourConnection;
    public Sprite noWire;

    [Header("Hover")]
    public Color hoverTint = new Color(220f / 255f, 210f / 255f, 40f / 255f);
    [Range(0, 1)] public float hoverTransparency = 0.3f;

    public void Init(GridTile northNeighbour,
        GridTile eastNeighbour,
        GridTile southNeighbour,
        GridTile westNeighbour)
    {
        _northNeighbour = northNeighbour;
        _eastNeighbour = eastNeighbour;
        _southNeighbour = southNeighbour;
        _westNeighbour = westNeighbour;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _hoverTint = new Color(hoverTint.r, hoverTint.g, hoverTint.b, hoverTransparency);
        Debug.Log(_hoverTint);
        CurrentColor = _noHoverTint;
    }

    /// <summary>
    /// Updates the color of the sprite based on the current wire state
    /// </summary>
    private void UpdateColor()
    {
        CurrentColor = _hovered switch
        {
            true when _wired => _hoverWire,
            true when !_wired => _hoverTint,
            false when _wired => _noHoverWire,
            false when !_wired => _noHoverTint,
            _ => new Color()
        };
    }

    private void OnMouseEnter()
    {
        _hovered = true;
        if (Input.GetMouseButton(0))
        {
            _wired = !_wired;
            UpdateWire();
        }
        else
        {
            UpdateColor();
        }
    }

    private void OnMouseExit()
    {
        _hovered = false;
        UpdateColor();
    }

    private void OnMouseDown()
    {
        _wired = !_wired;

        UpdateWire();
    }

    /// <summary>
    /// Updates the sprite and color to reflect the current wire state
    /// </summary>
    /// <param name="propagate">whether to notify neighbour wires of change</param>
    private void UpdateWire(bool propagate = true)
    {
        UpdateColor();
        if (propagate)
        {
            _northNeighbour.UpdateWire(false);
            _eastNeighbour.UpdateWire(false);
            _southNeighbour.UpdateWire(false);
            _westNeighbour.UpdateWire(false);
        }
        
        if (!_wired)
        {
            _spriteRenderer.sprite = noWire;
            return;
        }

        _spriteRenderer.sprite = wireNoConnection;

        var north = _northNeighbour is {_wired: true};
        var east = _eastNeighbour is {_wired: true};
        var south = _southNeighbour is {_wired: true};
        var west = _westNeighbour is {_wired: true};

        var connections = (north ? 1 : 0)
                          + (east ? 1 : 0)
                          + (south ? 1 : 0)
                          + (west ? 1 : 0);

        switch (connections)
        {
            case 0:
                _spriteRenderer.sprite = wireNoConnection;
                break;
            case 1:
            {
                _spriteRenderer.sprite = wireOneConnection;
                if (north)
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
                else if (east)
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                else if (south)
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
                else
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
                break;
            }
            case 2:
            {
                if (north && south)
                {
                    _spriteRenderer.sprite = wireTwoConnectionLine;
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0 ,0 , 90));
                }
                else if (east && west)
                {
                    _spriteRenderer.sprite = wireTwoConnectionLine;
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0 ,0 , 0));
                }
                else
                {
                    _spriteRenderer.sprite = wireTwoConnectionCorner;
                    switch (north)
                    {
                        case true when east:
                            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
                            break;
                        case true when west:
                            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case false when east:
                            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
                            break;
                        case false when west:
                            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
                            break;
                    }
                }
                break;
            }
            case 3:
                _spriteRenderer.sprite = wireThreeConnection;
                if (_northNeighbour is {_wired: false})
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
                else if (_eastNeighbour is {_wired: false})
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
                else if (_southNeighbour is {_wired: false})
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                else
                    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
                break;
            case 4:
                _spriteRenderer.sprite = wireFourConnection;
                break;

        }
    }
}
