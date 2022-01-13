using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform bugHolder;
    public float rayDist;

    public bool grabbed;

    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, transform.right, rayDist);
        Debug.DrawRay(grabDetect.position, transform.right, Color.red);
        
        if(grabCheck.collider != null && (grabCheck.collider.CompareTag("bug") || grabCheck.collider.CompareTag("bee") || grabCheck.collider.CompareTag("bread")) && grabCheck.collider.gameObject.GetComponent<CircleCollider2D>().isTrigger == false)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.M))
            {
                grabCheck.collider.gameObject.transform.parent = bugHolder;
                grabCheck.collider.gameObject.transform.position = bugHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }

        if (grabbed == true && grabCheck.collider != null && (grabCheck.collider.CompareTag("bug") || grabCheck.collider.CompareTag("bee") || grabCheck.collider.CompareTag("bread")) && grabCheck.collider.gameObject.GetComponent<CircleCollider2D>().isTrigger == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.N))
            {
                grabbed = false;
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
