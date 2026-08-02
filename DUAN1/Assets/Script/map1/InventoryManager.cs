using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    public ItemData[] inventory = new ItemData[4];

    [Header("UI")]
    public Button[] slotButtons;
    public Image[] slotImages;

    [HideInInspector]
    public int selectedSlot = -1;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;
            slotButtons[i].onClick.AddListener(() => SelectSlot(index));
        }

        RefreshInventory();
    }

    //=================================================

    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;

                RefreshInventory();

                return true;
            }
        }

        Debug.Log("Inventory đầy!");

        return false;
    }

    //=================================================

    public void RemoveItem(int index)
    {
        inventory[index] = null;

        CompressInventory();

        RefreshInventory();

        if (selectedSlot == index)
            selectedSlot = -1;
    }

    public void RemoveSelectedItem()
    {
        if (selectedSlot == -1)
            return;

        RemoveItem(selectedSlot);
    }

    //=================================================

    void CompressInventory()
    {
        ItemData[] temp = new ItemData[4];

        int next = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                temp[next] = inventory[i];
                next++;
            }
        }

        inventory = temp;
    }

    //=================================================

    public void RefreshInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                slotImages[i].enabled = false;
            }
            else
            {
                slotImages[i].enabled = true;
                slotImages[i].sprite = inventory[i].icon;
            }
        }
    }

    //=================================================

    public void SelectSlot(int index)
    {
        if (inventory[index] == null)
            return;

        if (selectedSlot == index)
        {
            Deselect();

            return;
        }

        selectedSlot = index;

        Debug.Log("Selected : " + inventory[index].itemName);
    }

    //=================================================

    public void Deselect()
    {
        selectedSlot = -1;

        Debug.Log("Deselected");
    }

    //=================================================

    public ItemData SelectedItem
    {
        get
        {
            if (selectedSlot == -1)
                return null;

            return inventory[selectedSlot];
        }
    }

    public int SelectedItemID
    {
        get
        {
            if (SelectedItem == null)
                return -1;

            return SelectedItem.itemID;
        }
    }

    public bool HasItem(int id)
    {
        foreach (ItemData item in inventory)
        {
            if (item != null && item.itemID == id)
                return true;
        }

        return false;
    }
}