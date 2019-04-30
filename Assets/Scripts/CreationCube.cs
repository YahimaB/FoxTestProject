using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationCube : MonoBehaviour
{
    public Material opaque;
    public Material transparent;

    public bool activated;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        SwapState();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && activated)
        {
            transform.parent.GetComponent<Creator>().CreatePuzzlePiece();
        }
    }

    public void SwapState()
    {
        activated = !activated;
        meshRenderer.material = activated ? opaque : transparent;
    }

}
