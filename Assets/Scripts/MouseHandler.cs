using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseHandler : MonoBehaviour
{
    private Placer placer;
    private Tilemap tilemap;
    private TileBase hoveringTile;
    private GameObject activeUnit;
    public Vector3Int hoveringPos;

    // Start is called before the first frame update
    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<Placer>();
        tilemap = GameObject.Find("Grid").transform.Find("Tilemap").GetComponent<Tilemap>();
        hoveringTile = null;
        activeUnit = null;
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
        bool flag = placer.TryToPlace(hoveringPos, hoveringTile);
        if(!flag)
        {
            if(activeUnit == null)
            {
                GameObject unit = TileManager.GetTileAt(hoveringPos).unit;
                if (unit == null)
                    return;
                AllyMove allyMove;
                if (unit.CompareTag("Ally") && unit.TryGetComponent<AllyMove>(out allyMove) && !allyMove.moving && allyMove.logic.CanMove())
                {
                    activeUnit = unit;
                    activeUnit.GetComponent<AllyMove>().OnClick();
                }
            } else
            {
                activeUnit.GetComponent<AllyMove>().OnClick();
                activeUnit = null;
            }
        } 
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
