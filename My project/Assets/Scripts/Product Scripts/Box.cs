using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class Box : MonoBehaviour
{
    public List<ProductSO> containedProducts;
    public Transform[] productPositions;
    public Animator animator;
    private PlayerMovement playerMovement;

    private ProductSO productReference;

    private bool isOpening = false;
    private bool isClosing = false;
    private bool isOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    public void InitializeProducts(List<ProductSO> products)
    {
        containedProducts = products;
        if (productPositions.Length < containedProducts.Count)
        {
            Debug.LogWarning("Not enough positions for all products");
            return;
        }

        for (int i = 0; i < containedProducts.Count; i++)
        {
            GameObject spawnedProduct = Instantiate(containedProducts[i].prefab, productPositions[i].position, productPositions[i].rotation, productPositions[i]);
            spawnedProduct.GetComponent<Rigidbody>().isKinematic = true;
            spawnedProduct.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }

    public void SetProductType(ProductSO product)
    {
        productReference = product;
    }

    private void Update()
    {
        if (isOpening)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("OpenAnimation") && stateInfo.normalizedTime >= 1.0f)
            {
                isOpening = false;
                isOpened = true;
            }
        }
        else if (isClosing)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("CloseAnimation") && stateInfo.normalizedTime >= 1.0f)
            {
                isClosing = false;
                isOpened = false;
            }
        }
    }

    public void ToggleBox()
    {
        if (isOpened)
        {
            CloseBox();
        }
        else
        {
            OpenBox();
        }
    }

    public void OpenBox()
    {
        if (!isClosing)
        {
            animator.SetTrigger("OpenAnimation");
            isOpening = true;
        }
    }

    public void CloseBox()
    {
        if (!isOpening)
        {
            animator.SetTrigger("CloseAnimation");
            isClosing = true;
        }
    }
}