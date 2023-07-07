using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Shiba Parent;

    void Start()
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(transform.position, 15);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //change to if sees sus activity
            if (hit.collider.gameObject.name == "Card")
            {
                Parent.AddSuspicion(0.01f);
                Mathf.Clamp(Parent.GetSuspicion(), 0, 1);
            }

            //check if catches player helping them
        }
    }
}

