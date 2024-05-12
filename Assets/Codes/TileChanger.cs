using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;
public class TileChanger : MonoBehaviour
{
    public Tilemap tilemap; // Değiştirmek istediğiniz TileMap referansı
    public List<Sprite> newSprite; // Yeni sprite
    private int i;


    [Button]
    void ChangeAllSprites()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                TileBase tile = tilemap.GetTile(pos);
                if (tile != null)
                {
                    // Eğer tile varsa, sprite'ını değiştir.
                    tilemap.SetTile(pos, CreateTile(newSprite[i]));
                    i++;
                }
            }
        }
    }

    Tile CreateTile(Sprite sprite)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        return tile;
    }

}