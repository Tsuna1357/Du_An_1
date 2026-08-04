using UnityEngine;

public class NormalFocus : Interactable
{
    public CameraMap1 cameraMap;
    public Transform focusPoint;

    public bool isFocused = false;
    public override void OnClick()
    {
        // Nếu chưa focus thì focus trước
        if (!isFocused)
        {
            FocusIn();
            return;
        }

        // Đã focus rồi
        Debug.Log("Đã focus ");
    }

    public void FocusIn()
    {
        if (isFocused)
            return;

        cameraMap.Focus(focusPoint);

        isFocused = true;
    }

    public void ExitFocus()
    {
        cameraMap.Back();

        isFocused = false;
    }
}
