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
    [SerializeField] private TileManager tileManager;

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdatePosition(Vector2 target)
    {
        if(!isPlacing) { return; }
        placingUnit.transform.position = new Vector3(target.x, target.y+0.25f, transform.position.z);
    }

    public void AssignUnit(GameObject card)
    {
        placingUnit = Instantiate(card.GetComponent<CardDisplay>().card.unit, new Vector3(0, 0, 0), Quaternion.identity);
        placingCard = card;
        isPlacing = true;
    }

    public void OnClick(Vector3Int pos, TileBase tile)
    {
        if(isPlacing)
        {
            if (tile != null)
            {
                if (Place(pos, placingUnit))
                {
                    isPlacing = false;
                    placingUnit = null;
                }
                else
                {
                    Debug.Log("Unable to place unit.");
                    //Feedback or something idk
                }
            }
        }
    }

    public void CancelPlace()
    {
        Destroy(placingUnit);
        isPlacing = false;
    }

    public bool Place(Vector3Int worldPoint, GameObject unit)
    {
        //Make tile store the placed unit in tileproperties
        if(tileManager.PlaceUnit(worldPoint, unit))
        {
            unit.transform.position = tilemap.GetCellCenterWorld(worldPoint) + new Vector3(0, 0.75f, 0);
            unit.GetComponent<Movement>().Init(worldPoint);
            if (placingCard != null)
            {
                Destroy(placingCard);
            }
            return true;
        }
        return false;
    }
}
