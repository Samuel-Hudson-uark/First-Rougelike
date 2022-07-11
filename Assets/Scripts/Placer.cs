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
        SpriteLibraryAsset unitSpriteLibrary = placingCard.GetComponent<CardDisplay>().card.unit.GetComponent<SpriteLibrary>().spriteLibraryAsset;
        GetComponent<SpriteLibrary>().spriteLibraryAsset = unitSpriteLibrary;
        isPlacing = true;
    }

    public bool TryToPlace(Vector3Int pos, TileBase tile)
    {
        if(isPlacing)
        {
            if (tile != null)
            {

                if (Place(pos, placingCard.GetComponent<CardDisplay>().card.unit))
                    return true;
                else
                {
                    Debug.Log("Unable to place unit.");
                    //Feedback or something idk
                }
            }
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
        //Make sure you can afford unit
        int price = unit.GetComponent<UnitLogic>().Card.cost;
        if(TurnManager.allyMana.CanAfford(price) && TileManager.CanPlaceUnit(worldPoint, unit))
        {
            TurnManager.allyMana.SpendMana(price);
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
