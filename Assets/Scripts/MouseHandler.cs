using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseHandler : MonoBehaviour
{
    private Placer placer;
    private Tilemap tilemap;
    private TileBase hoveringTile;
    public Vector3Int hoveringPos;

    // Start is called before the first frame update
    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<Placer>();
        tilemap = GameObject.Find("Grid").transform.Find("Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placer.UpdatePosition(worldPoint);
        UpdateHoveringTile(worldPoint);
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && hoveringTile == null)
            placer.CancelPlace();
    }
    public void Click()
    {
        Debug.Log(hoveringPos);
        placer.OnClick(hoveringPos, hoveringTile);
    }

    bool UpdateHoveringTile(Vector2 worldPoint)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPoint) + new Vector3Int(-1, -1, 0);
        if (tilePos != hoveringPos)
            hoveringPos = tilePos;
        TileBase tileTemp = tilemap.GetTile(tilePos);
        if(tileTemp != hoveringTile)
        {
            hoveringTile = tileTemp;
            return true;
        }
        return false;
    }

    
}
