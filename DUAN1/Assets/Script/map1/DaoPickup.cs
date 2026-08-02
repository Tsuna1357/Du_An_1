using UnityEngine;

public class DaoPickup : Interactable
{
    public Cabinet cabinet;

    public ItemData itemData;

    public override void OnClick()
    {
        if (!cabinet.leftDoorOpen)
        {
            Debug.Log("Phải mở cửa trái trước.");

            return;
        }

        if (InventoryManager.Instance.AddItem(itemData))
        {
            gameObject.SetActive(false);
        }
    }
}