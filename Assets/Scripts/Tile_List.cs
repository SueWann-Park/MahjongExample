using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_List : Tile_Button
{
    public Tile_Button origin;

    protected override void OnClick()
    {
        origin.count++;
        origin.transform.position = origin.startPos;
        GameMaster.RemoveObjectFromList(gameObject);
        Destroy(gameObject);
    }
}
