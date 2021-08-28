using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isViewing = false;
    public GameObject cardImage;
    private Placer placer;

    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<Placer>();
    }

    public void Click()
    {
        placer.AssignUnit(gameObject);
        PointerExit();
    }

    public void EndDrag()
    {
        placer.OnClick();
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
