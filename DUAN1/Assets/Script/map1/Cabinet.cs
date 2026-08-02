using UnityEngine;

public class Cabinet : Interactable
{
    [Header("Camera")]
    public CameraMap1 cameraMap;
    public Transform focusPoint;

    [Header("State")]
    public bool isFocused;

    public bool leftDoorOpen;
    public bool rightDoorOpen;

    public override void OnClick()
    {
        if (!isFocused)
        {
            FocusCabinet();
            return;
        }
    }

    public void FocusCabinet()
    {
        cameraMap.Focus(focusPoint);

        isFocused = true;
    }

    public void ExitCabinet()
    {
        cameraMap.Back();

        isFocused = false;
    }
}