using System.Collections.Generic;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    public int width = 10;

    public int height = 10;

    public GameObject gridTilePrefab;

    private readonly IDictionary<Vector2, GridTile> _tileDictionary = new Dictionary<Vector2, GridTile>();

    // Start is called before the first frame update
    private void Start()
    {
        var position = transform.position;
        var rotation = transform.rotation;
        var x = position.x - width / 2f;
        var y = position.y - height / 2f;
        var beginningX = x;

        for (var i = 0; i < height; i++)
        {
            for (var ii = 0; ii < width; ii++)
            {
                var tileGameObject = Instantiate(gridTilePrefab, new Vector3(x, y, position.z), rotation, transform);
                var tile = tileGameObject.GetComponent<GridTile>();
                _tileDictionary[new Vector2(x, y)] = tile;
                x++;
            }

            x = beginningX;
            y++;
        }

        foreach (var tilePair in _tileDictionary)
        {
            _tileDictionary.TryGetValue(new Vector2(tilePair.Key.x, tilePair.Key.y + 1), out var northTile);
            _tileDictionary.TryGetValue(new Vector2(tilePair.Key.x + 1, tilePair.Key.y), out var eastTile);
            _tileDictionary.TryGetValue(new Vector2(tilePair.Key.x, tilePair.Key.y - 1), out var southTile);
            _tileDictionary.TryGetValue(new Vector2(tilePair.Key.x - 1, tilePair.Key.y), out var westTile);

            tilePair.Value.Init(northTile, eastTile, southTile, westTile);
        }
    }
}
