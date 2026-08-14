using UnityEngine;

public class FocusPainting : Interactable
{
    [Header("Camera Focus")]
    public CameraMap1 cameraMap;
    public Transform focusPoint;

    [Header("Tutorial")]
    public Map1Tutorial tutorial;

    public override void OnClick()
    {
        // Focus giống NormalFocus
        if (cameraMap != null && focusPoint != null)
        {
            cameraMap.Focus(focusPoint);
        }

        // Báo cho Tutorial biết người chơi đã click tranh
        if (tutorial != null)
        {
            tutorial.PaintingFocused();
        }
    }
}