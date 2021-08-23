using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool canPlace = false;
    private bool isViewing = false;
    private Vector3 startPosition;
    private GameObject placingObject;
    public GameObject cardImage;

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
        placingObject = Instantiate(gameObject.GetComponent<CardDisplay>().card.placer, new Vector3(0,0,0), Quaternion.identity);
        placingObject.transform.SetParent(transform, false);
        isDragging = true;
        PointerExit();
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

    public void PointerEnter()
    {
        if (isDragging || isViewing) return;
        isViewing = true;
        cardImage.transform.LeanMoveLocalY(60, 0.25f);
        cardImage.transform.localScale = new Vector3(2, 2, 1);
        cardImage.layer = 7;
        foreach(Transform child in cardImage.transform)
        {
            child.gameObject.layer = 7;
        }
    }

    public void PointerExit()
    {
        if (!isViewing) return;
        isViewing = false;
        LeanTween.cancel(cardImage);
        cardImage.transform.position = transform.position;
        cardImage.transform.localScale = new Vector3(1, 1, 1);
        cardImage.layer = gameObject.layer;
        foreach (Transform child in cardImage.transform)
        {
            child.gameObject.layer = gameObject.layer;
        }
    }
}
