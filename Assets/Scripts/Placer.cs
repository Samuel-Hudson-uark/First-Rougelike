using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

public class Placer : MonoBehaviour
{
    private static Vector2 size = new Vector2(32, 32);
    private bool isPlacing = false;
    private GameObject placingCard;
    [SerializeField] private Tilemap tilemap;

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
    }

    public void AssignUnit(GameObject card)
    {
        placingCard = card;
        GetComponent<SpriteLibrary>().spriteLibraryAsset = placingCard.GetComponent<CardDisplay>().card.unit.GetComponent<SpriteLibrary>().spriteLibraryAsset;
        isPlacing = true;
    }

    public bool TryToPlace(Vector3Int pos, TileBase tile)
    {
        if(isPlacing)
        {
            if (tile != null)
            {

                if (Place(pos, placingCard.GetComponent<CardDisplay>().card.unit))
                {
                    isPlacing = false;
                }
                else
                {
                    Debug.Log("Unable to place unit.");
                    //Feedback or something idk
                }
            }
            return true;
        }
        return false;
    }

    public void CancelPlace()
    {
        placingCard = null;
        GetComponent<SpriteLibrary>().spriteLibraryAsset = null;
        GetComponent<SpriteRenderer>().sprite = null;
        isPlacing = false;
    }

    public bool Place(Vector3Int worldPoint, GameObject unit)
    {
        //Make tile store the placed unit in tileproperties
        if(TileManager.CanPlaceUnit(worldPoint, unit))
        {
            unit = Instantiate(unit, tilemap.GetCellCenterWorld(worldPoint) + new Vector3(0, 0.75f, 0), Quaternion.identity);
            TileManager.PlaceUnit(worldPoint, unit);
            unit.GetComponent<Movement>().Init(worldPoint);
            if (placingCard != null)
            {
                Destroy(placingCard);
            }
            CancelPlace();
            return true;
        }
        Destroy(unit);
        return false;
    }
}
