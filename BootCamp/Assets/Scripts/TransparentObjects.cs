using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjects : MonoBehaviour
{
    public Material mainMaterial;
    public Material transparentMaterial;
    public PlayerMovement playerMovement;

    private MeshRenderer meshRenderer;
    private bool isSideCameraActive;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void Update()
    {
        ChangingMaterials();
    }

    public void ChangingMaterials()
    {
        isSideCameraActive = playerMovement.isPlayerInCrouchingArea;

        if (isSideCameraActive)
        {
            meshRenderer.material = transparentMaterial;
        }
        else if (!isSideCameraActive)
        {
            meshRenderer.material = mainMaterial;
        }
    }
}
