using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LayerMask grappleLayer2;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float timeBtwShooting;
   
    private DistanceJoint2D grappleLine;
    public bool releaseGrapple = false;
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private float delay;

    public bool visualDelay = false;
    private FrameFill display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<FrameFill>();
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        delay = timeBtwShooting;
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (visualDelay)
        {
            display.fillFrame(1.0f -(delay / timeBtwShooting));
        }

        if (Input.GetKeyDown("w") && delay > timeBtwShooting)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer | grappleLayer2
            );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }
            delay = 0;
        }

        if (Input.GetKeyUp("w") || releaseGrapple)
        {
            joint.enabled = false;
            rope.enabled = false;
            releaseGrapple = false;
        }

        if (rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, grappleLength);
    }
}
