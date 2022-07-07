using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placer : MonoBehaviour
{
    private static Vector2 size = new Vector2(32, 32);
    private bool isPlacing = false;
    private GameObject placingUnit;
    private GameObject placingCard;
    [SerializeField] private Tilemap tilemap;

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            UpdatePosition();
        }
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    private void UpdatePosition()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        target.y += 0.25f;
        placingUnit.transform.position = target;
    }

    public void AssignUnit(GameObject card)
    {
        placingUnit = Instantiate(card.GetComponent<CardDisplay>().card.unit, new Vector3(0, 0, 0), Quaternion.identity);
        placingCard = card;
        isPlacing = true;
        UpdatePosition();
    }

    public void OnClick()
    {
        Vector2 posOnScreen = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(posOnScreen);
        Vector3Int tilePos = tilemap.WorldToCell(worldPoint) + new Vector3Int(-1,-1,0);
        TileBase tile = tilemap.GetTile(tilePos);
        if(isPlacing)
        {
            if (tile != null)
            {
                if (Place(tile, tilemap.GetCellCenterWorld(tilePos), placingUnit))
                {
                    isPlacing = false;
                    placingUnit = null;
                }
                else
                {
                    //Feedback or something idk
                }
            }
            else
            {
                Destroy(placingUnit);
                isPlacing = false;
            }

        }
        
    }

    public bool Place(TileBase tile, Vector3 worldPoint, GameObject unit)
    {
        //Make tile store the placed unit in tileproperties
        unit.transform.position = worldPoint + new Vector3(0, 0.75f, 0);
        unit.GetComponent<Movement>().Init();
        if (placingCard != null)
        {
            Destroy(placingCard);
        }
        return true;
    }
}
