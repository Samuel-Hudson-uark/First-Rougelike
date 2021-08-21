using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool canPlace = false;
    public GameObject hand;
    private Vector3 startPosition;
    private GameObject placingObject;

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            placingObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y+50);
        }
    }

    public void StartDrag()
    {
        placingObject = Instantiate(transform.GetComponent<CardDisplay>().card.unit, new Vector3(0,0,0), Quaternion.identity);
        placingObject.transform.SetParent(transform, false);
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        if(canPlace)
        {

        } else
        {
            Destroy(placingObject);
        }
    }
}
