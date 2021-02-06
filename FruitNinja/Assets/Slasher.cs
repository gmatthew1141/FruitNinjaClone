using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;

    private Collider2D col;

    public float minVelo = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = IsMoving();

        SetBladeToMouse();
    }

    private void SetBladeToMouse() {
        var mousePos = Input.mousePosition;
        mousePos.z = 5;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMoving() {
        Vector3 currMousePos = transform.position;

        float travelled = (lastMousePos - currMousePos).magnitude;

        lastMousePos = currMousePos;

        if (travelled > minVelo) {
            return true;
        }

        return false;
    }
}
