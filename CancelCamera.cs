using UnityEngine;
using StarterAssets;

public class CancelCamera : MonoBehaviour
{
    private ThirdPersonController playerController;

    private void OnEnable()
    {
        playerController = FindAnyObjectByType<ThirdPersonController>();

        playerController.LockCameraPosition = true;
        playerController.CancelMove(false);
    }

    private void OnDisable()
    {
        playerController = FindAnyObjectByType<ThirdPersonController>();

        playerController.LockCameraPosition = false;
        playerController.CancelMove(true);
    }
}