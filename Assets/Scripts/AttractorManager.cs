using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorManager : MonoBehaviour
{
    [SerializeField] private GameObject attractorPrefab;
    [SerializeField] private float scaleFactor = 10f;

    private bool isDragging = false;

    private Vector3 attractorPos;
    private GameObject prevAttractor;
    private GameObject newAttractor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            NewAttractor();
        }
        if (isDragging)
        {
            UpdateRange();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ActivateAttractor();
        }
    }

    private void NewAttractor()
    {
        attractorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attractorPos.z = 0;
        newAttractor = Instantiate(attractorPrefab, attractorPos, Quaternion.identity);
    }

    private void UpdateRange()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        float dist = (mousePos - attractorPos).magnitude * scaleFactor;

        newAttractor.transform.localScale = Vector3.one * dist;
    }

    private void ActivateAttractor()
    {
        Destroy(prevAttractor);
        prevAttractor = newAttractor;
        prevAttractor.GetComponent<PointEffector2D>().enabled = true;
    }
}
