using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private static Vector2 size = new Vector2(32, 32);
    private bool isPlacing = false;
    private GameObject placingUnit;
    private GameObject placingCard;
    private GameObject hoveringTile;
    [SerializeField] private GameObject isoGridSpawner;
    private static Vector2 gridPos;

    private void Start()
    {
        gridPos = new Vector2(isoGridSpawner.transform.position.x, isoGridSpawner.transform.position.y+0.5f);
    }

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
        FindHoveringTile();
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
        if(hoveringTile != null && isPlacing)
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
        }
    }

    public bool Place(GameObject tileObject)
    {
        Tile tile = hoveringTile.GetComponent<Tile>();
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
        return flag;
    }

    private void FindHoveringTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
        {
            Vector2 hitPoint = hit.point;
            Vector2Int hitTileCoord = new Vector2Int(
                (int)((((hitPoint.y - gridPos.y) / 0.25f) - ((hitPoint.x - gridPos.x) / 0.5f)) / -2),
                (int)((((hitPoint.y-gridPos.y) / 0.25f) + ((hitPoint.x-gridPos.x) / 0.5f))/-2)
                );
            GameObject newTile = isoGridSpawner.GetComponent<iso_grid>().FindTile(hitTileCoord);
            if(newTile != null) {
                if(newTile != hoveringTile)
                {
                    NewHoveringTile(newTile);
                }
                return;
            }
        }
        if (hoveringTile != null)
        {
            hoveringTile.transform.position -= new Vector3(0, 0.1f, 0);
            hoveringTile = null;
        }
    }

    private void NewHoveringTile(GameObject newTile)
    {
        if(hoveringTile != null)
        {
            hoveringTile.transform.position -= new Vector3(0,0.1f,0);
        }
        hoveringTile = newTile;
        hoveringTile.transform.position += new Vector3(0, 0.1f, 0);
    }
}
