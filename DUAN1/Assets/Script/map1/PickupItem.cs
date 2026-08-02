using UnityEngine;

public class PickupItem : Interactable
{
    public ItemData itemData;

    public override void OnClick()
    {
        if (InventoryManager.Instance.AddItem(itemData))
        {
            gameObject.SetActive(false);
        }
    }
}