using UnityEngine;
using System.Collections;

public class Door : Interactable
{
    public Cabinet cabinet;

    [Header("Door")]
    public bool isLeftDoor;

    // THÊM: cho phép SafeCodeUI gọi mở cửa
    public void OpenByPuzzle()
    {
        if (rotating)
            return;

        bool isOpen = isLeftDoor
            ? cabinet.leftDoorOpen
            : cabinet.rightDoorOpen;

        // Chỉ mở nếu cửa đang đóng
        if (!isOpen)
        {
            StartCoroutine(RotateDoor());
        }
    }

    private bool rotating;

    private Quaternion closeRotation;
    private Quaternion openRotation;

    void Start()
    {
        closeRotation = transform.localRotation;

        if (isLeftDoor)
        {
            openRotation = closeRotation * Quaternion.Euler(0, -180, 0);
        }
        else
        {
            openRotation = closeRotation * Quaternion.Euler(0, 180, 0);
        }
    }

    public override void OnClick()
    {
        // Chưa focus thì focus
        if (!cabinet.cameraMap.isFocus)
        {
            cabinet.FocusCabinet();
            return;
        }

        if (rotating)
            return;

        StartCoroutine(RotateDoor());
    }

    IEnumerator RotateDoor()
    {
        rotating = true;

        bool isOpen = isLeftDoor
            ? cabinet.leftDoorOpen
            : cabinet.rightDoorOpen;

        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = isOpen ? closeRotation : openRotation;

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * 2f;

            transform.localRotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                time
            );

            yield return null;
        }

        transform.localRotation = targetRotation;

        // Cập nhật trạng thái
        if (isLeftDoor)
            cabinet.leftDoorOpen = !cabinet.leftDoorOpen;
        else
            cabinet.rightDoorOpen = !cabinet.rightDoorOpen;

        rotating = false;
    }
}