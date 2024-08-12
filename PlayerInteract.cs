using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerLook playerLook = GetComponent<PlayerLook>();
        if (playerLook != null)
        {
            cam = playerLook.cam;
        }
        else
        {
            Debug.LogError("PlayerLook component not found on this GameObject.");
        }

        playerUI = GetComponent<PlayerUI>();
        if (playerUI == null)
        {
            Debug.LogError("PlayerUI component not found on this GameObject.");
        }

        inputManager = GetComponent<InputManager>();
        if (inputManager == null)
        {
            Debug.LogError("InputManager component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null || playerUI == null || inputManager == null)
        {
            return; // Exit early if essential components are missing
        }

        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}

