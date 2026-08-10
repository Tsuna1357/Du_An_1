using UnityEngine;

public class DaoPickup : Interactable
{
    [Header("Cabinet")]
    public Cabinet cabinet;

    [Tooltip("Bật nếu item nằm trong cửa trái, tắt nếu item nằm trong cửa phải.")]
    public bool isLeftDoor = true;

    [Header("Item")]
    public ItemData itemData;

    public override void OnClick()
    {
        // Kiểm tra cửa tương ứng đã mở chưa
        if (isLeftDoor)
        {
            if (!cabinet.leftDoorOpen)
            {
                Debug.Log("Phải mở cửa trái trước.");
                return;
            }
        }
        else
        {
            if (!cabinet.rightDoorOpen)
            {
                Debug.Log("Phải mở cửa phải trước.");
                return;
            }
        }

        // Thêm item vào inventory
        if (InventoryManager.Instance.AddItem(itemData))
        {
            // Nhặt thành công thì tắt object
            gameObject.SetActive(false);
        }
    }
}