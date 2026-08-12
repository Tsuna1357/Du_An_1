using UnityEngine;

public class SafeInteraction : Interactable
{
    [Header("Cabinet")]
    public Cabinet cabinet;

    [Tooltip("Két nằm trong cửa trái hay cửa phải")]
    public bool isLeftDoor = true;

    [Header("Safe UI / Object")]
    public GameObject safePanel;

    public override void OnClick()
    {
        // Kiểm tra Cabinet đã được gán chưa
        if (cabinet == null)
        {
            Debug.LogError("SafeInteraction: Chưa gán Cabinet!");
            return;
        }

        // Kiểm tra cửa tương ứng đã mở chưa
        if (isLeftDoor)
        {
            if (!cabinet.leftDoorOpen)
            {
                Debug.Log("Phải mở cửa trái trước mới tương tác được với két sắt.");
                return;
            }
        }
        else
        {
            if (!cabinet.rightDoorOpen)
            {
                Debug.Log("Phải mở cửa phải trước mới tương tác được với két sắt.");
                return;
            }
        }

        // Nếu cửa đã mở thì hiện két sắt
        OpenSafe();
    }

    public void OpenSafe()
    {
        if (safePanel != null)
        {
            safePanel.SetActive(true);
        }
    }

    public void CloseSafe()
    {
        if (safePanel != null)
        {
            safePanel.SetActive(false);
        }
    }
}