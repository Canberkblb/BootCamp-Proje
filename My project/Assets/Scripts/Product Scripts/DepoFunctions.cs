using UnityEngine;

public class DepoFunctions : MonoBehaviour
{
    public GameObject depoCanvasPrefab;
    private GameObject depoCanvasInstance;
    public GameObject itemSpawnPoint;
    public GameObject gameUIPrefab;
    private GameObject gameUIInstance;
    private void Awake()
    {
        depoCanvasInstance = Instantiate(depoCanvasPrefab);
        UIFunctions uiFunctions = depoCanvasInstance.GetComponent<UIFunctions>();

        depoCanvasInstance.SetActive(false);

        gameUIInstance = Instantiate(gameUIPrefab);
        gameUIInstance.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            bool depoAktif = !depoCanvasInstance.activeSelf;
            if (depoAktif)
            {
                UIFunctions uiFunctions = depoCanvasInstance.GetComponent<UIFunctions>();
                uiFunctions.returnDesktop();
            }

            depoCanvasInstance.SetActive(depoAktif);

            gameUIInstance.SetActive(!depoAktif);

            if (depoAktif)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }
    }
}
