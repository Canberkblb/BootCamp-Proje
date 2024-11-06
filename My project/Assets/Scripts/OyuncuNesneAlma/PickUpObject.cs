using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private Transform player;         // Oyuncunun pozisyonunu referans alýr
    [SerializeField] private Transform holdPosition;   // Nesnenin tutulacaðý pozisyon (kamera veya elin önünde bir nokta)

    private GameObject heldObject = null;              // Elimizde tuttuðumuz nesne

    private void Update()
    {
        // E tuþuna basýldýðýnda, elimizde nesne yoksa bir nesneyi almayý dene
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                Pickup();
            }
        }

        // Q tuþuna basýldýðýnda, elimizde bir nesne varsa onu býrak
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
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // Kameradan bakýlan yöne doðru bir ray oluþtur
        RaycastHit hit;

        // Raycast ile önümüzdeki nesnelere bak
        if (Physics.Raycast(ray, out hit, 5f))  // 3f mesafe içinde bakýyoruz, bu mesafeyi ayarlayabilirsiniz
        {
            if (hit.transform.CompareTag("Pickup"))  // Eðer "Pickup" tagine sahip bir nesneye çarptýysa
            {
                heldObject = hit.transform.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;  // Nesneyi hareket ettirebilmek için kinematic yap
                heldObject.transform.position = holdPosition.position;    // Nesneyi tutma pozisyonuna getir
                heldObject.transform.parent = holdPosition;               // Nesneyi tutma pozisyonuna child olarak ekle
            }
        }
    }

    private void Drop()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;  // Nesnenin fiziðini geri getir
        heldObject.transform.parent = null;                        // Nesnenin parentini sýfýrla
        heldObject = null;                                         // Elimizdeki nesneyi býrak
    }
}
