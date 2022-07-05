using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    bool isHovering = false;
    Vector3 original_position;

    private void Start()
    {
        original_position = transform.position;
    }

    public void PointerEnter()
    {
        if (isHovering) return;
        isHovering = true;
        transform.LeanMoveLocalY(1, 0.25f);
    }

    public void PointerExit()
    {
        if (!isHovering) return;
        isHovering = false;
        LeanTween.cancel(gameObject);
        transform.position = original_position;
    }
}
