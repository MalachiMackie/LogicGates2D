using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    public int width = 10;

    public int height = 10;

    public GameObject gridTilePrefab;

    // Start is called before the first frame update
    private void Start()
    {
        var position = transform.position;
        var rotation = transform.rotation;
        var x = position.x - width / 2;
        var y = position.y - height / 2;
        var beginningX = x;

        for (var i = 0; i < height; i++)
        {
            for (var ii = 0; ii < width; ii++)
            {
                GameObject go = Instantiate(gridTilePrefab, new Vector3(x, y, position.z), rotation, transform);
                x++;
            }

            x = beginningX;
            y++;
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
