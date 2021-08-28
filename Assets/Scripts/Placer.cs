using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private bool isPlacing = false;
    private GameObject placingUnit;
    private GameObject placingCard;

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            UpdatePosition();
            if(Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
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
        if (isPlacing)
        {
            isPlacing = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
            {
                GameObject tileObject = hit.collider.gameObject;
                Tile tile = tileObject.GetComponent<Tile>();
                if (tile.CanPlace(placingUnit))
                {
                    placingUnit.transform.SetParent(tileObject.transform);
                    placingUnit.transform.localPosition = new Vector3(0, 0, 0);
                    placingUnit = null;
                    if(placingCard != null)
                    {
                        Destroy(placingCard);
                    }
                }
            }
            else
            {
                Destroy(placingUnit);
            }
        }
    }
}
