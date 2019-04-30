using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewBehaviourScript : MonoBehaviour
{
    private SnapGrid grid;
    private Rigidbody rb;
    
    private void Awake()
    {
        grid = FindObjectOfType<SnapGrid>();
        rb = transform.GetComponent<Rigidbody>();
    }

    private Vector3 offset;
    private Vector3 targetPosition;

    private void FindTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, new Vector3(0, 0.5f, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            targetPosition = ray.GetPoint(distance);
        }
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        FindTargetPosition();
        offset = targetPosition - transform.position;
    }

    private void OnMouseDrag()
    {
        FindTargetPosition();
        targetPosition -= offset;
        rb.velocity = (targetPosition - transform.position) * 10;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = true;

        bool canPlaceOnTarget = false;
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            canPlaceOnTarget |= hits[i].collider.gameObject == gameObject;
        }

        if (canPlaceOnTarget)
        {
            PlaceCubeNear(targetPosition);
        }
        else
        {
            PlaceCubeNear(transform.position);
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        transform.position = finalPosition;
    }
}
