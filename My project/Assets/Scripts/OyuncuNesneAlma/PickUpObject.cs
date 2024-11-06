using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private Transform player;         // Oyuncunun pozisyonunu referans al�r
    [SerializeField] private Transform holdPosition;   // Nesnenin tutulaca�� pozisyon (kamera veya elin �n�nde bir nokta)

    private GameObject heldObject = null;              // Elimizde tuttu�umuz nesne

    private void Update()
    {
        // E tu�una bas�ld���nda, elimizde nesne yoksa bir nesneyi almay� dene
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                Pickup();
            }
        }

        // Q tu�una bas�ld���nda, elimizde bir nesne varsa onu b�rak
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject != null)
            {
                Drop();
            }
        }
    }

    private void Pickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // Kameradan bak�lan y�ne do�ru bir ray olu�tur
        RaycastHit hit;

        // Raycast ile �n�m�zdeki nesnelere bak
        if (Physics.Raycast(ray, out hit, 5f))  // 3f mesafe i�inde bak�yoruz, bu mesafeyi ayarlayabilirsiniz
        {
            if (hit.transform.CompareTag("Pickup"))  // E�er "Pickup" tagine sahip bir nesneye �arpt�ysa
            {
                heldObject = hit.transform.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;  // Nesneyi hareket ettirebilmek i�in kinematic yap
                heldObject.transform.position = holdPosition.position;    // Nesneyi tutma pozisyonuna getir
                heldObject.transform.parent = holdPosition;               // Nesneyi tutma pozisyonuna child olarak ekle
            }
        }
    }

    private void Drop()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;  // Nesnenin fizi�ini geri getir
        heldObject.transform.parent = null;                        // Nesnenin parentini s�f�rla
        heldObject = null;                                         // Elimizdeki nesneyi b�rak
    }
}
