using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private static Vector2 size = new Vector2(32, 32);
    private bool isPlacing = false;
    private GameObject placingUnit;
    private GameObject placingCard;
    [SerializeField] private GridLayout gridLayout;

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
        Vector3Int tile = gridLayout.WorldToCell(worldPoint) + new Vector3Int(-1,-1,0);
        Debug.Log(tile);
        /*if(hoveringTile != null && isPlacing)
        {
            if(Place(hoveringTile))
            {
                isPlacing = false;
            } else
            {
                //Feedback or something idk
            }
        }
        else if(isPlacing)
        {
            Destroy(placingUnit);
            isPlacing = false;
        }*/
    }

    public bool Place(GameObject tileObject)
    {
        /*Tile tile = hoveringTile.GetComponent<Tile>();
        bool flag = tile.CanPass(placingUnit);
        if (flag)
        {
            placingUnit.transform.SetParent(tileObject.transform);
            placingUnit.transform.localPosition = new Vector3(0, 0.4f, 0);
            placingUnit.GetComponent<Movement>().Init();
            placingUnit = null;
            if (placingCard != null)
            {
                Destroy(placingCard);
            }
        }
        return flag;*/
        return false;
    }
}
