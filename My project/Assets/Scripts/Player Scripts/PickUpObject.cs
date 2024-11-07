using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform holdPosition;
    public float pickUpRange = 5f;
    public LayerMask pickUpLayer;
    public float maxTiltAngle = 45f;


    private GameObject heldObject;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject != null)
            {
                DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && heldObject != null && heldObject.CompareTag("Box"))
        {
            Debug.Log("Anim triggered");
            heldObject.GetComponent<Box>().ToggleBox();
        }

        if (heldObject != null && heldObject.CompareTag("Box"))
        {
            AdjustBoxRotation();
        }
    }

    private void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpRange, pickUpLayer))
        {
            if (hit.transform.CompareTag("Box"))
            {
                heldObject = hit.transform.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
                heldObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    private void DropObject()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.transform.SetParent(null);
        heldObject = null;
    }

    private void AdjustBoxRotation()
    {
        float pitch = Camera.main.transform.localEulerAngles.x;
        if (pitch > 180) pitch -= 360;

        float tilt = Mathf.Clamp(pitch, -maxTiltAngle, maxTiltAngle);

        heldObject.transform.localRotation = Quaternion.Euler(0, 90, tilt);
    }
}
