using UnityEngine;

public class PicturePuzzle : Interactable
{
    [Header("Item IDs")]
    public int picture0_0ItemID = 14;
    public int picture1_0ItemID = 15;
    public int picture0_1ItemID = 16;

    [Header("Picture 0-0 Objects")]
    public GameObject picture0_0_Object1;
    public GameObject picture0_0_Object2;

    [Header("Picture 1-0 Objects")]
    public GameObject picture1_0_Object1;
    public GameObject picture1_0_Object2;

    [Header("Picture 0-1 Objects")]
    public GameObject picture0_1_Object1;
    public GameObject picture0_1_Object2;

    [Header("Final Objects")]
    public GameObject[] objectsToDeactivate = new GameObject[8];

    public GameObject[] objectToActivate = new GameObject[2];

    [Header("State")]
    public bool picture0_0On = false;
    public bool picture1_0On = false;
    public bool picture0_1On = false;

    //========================================================
    // PLAYER CLICK
    //========================================================

    public override void OnClick()
    {
        CheckSelectedItem();
    }

    //========================================================
    // CHECK SELECTED ITEM
    //========================================================

    void CheckSelectedItem()
    {
        if (InventoryManager.Instance == null)
            return;

        int selectedID = InventoryManager.Instance.SelectedItemID;

        // Không chọn item
        if (selectedID == -1)
            return;

        //====================================================
        // PICTURE 0-0 - ITEM ID 14
        //====================================================

        if (selectedID == picture0_0ItemID)
        {
            picture0_0On = true;

            // Active 2 object
            if (picture0_0_Object1 != null)
                picture0_0_Object1.SetActive(true);

            if (picture0_0_Object2 != null)
                picture0_0_Object2.SetActive(true);

            // Remove item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            // Check cả 3 picture
            checkBool();

            return;
        }

        //====================================================
        // PICTURE 1-0 - ITEM ID 15
        //====================================================

        if (selectedID == picture1_0ItemID)
        {
            picture1_0On = true;

            // Active 2 object
            if (picture1_0_Object1 != null)
                picture1_0_Object1.SetActive(true);

            if (picture1_0_Object2 != null)
                picture1_0_Object2.SetActive(true);

            // Remove item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            // Check cả 3 picture
            checkBool();

            return;
        }

        //====================================================
        // PICTURE 0-1 - ITEM ID 16
        //====================================================

        if (selectedID == picture0_1ItemID)
        {
            picture0_1On = true;

            // Active 2 object
            if (picture0_1_Object1 != null)
                picture0_1_Object1.SetActive(true);

            if (picture0_1_Object2 != null)
                picture0_1_Object2.SetActive(true);

            // Remove item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            // Check cả 3 picture
            checkBool();

            return;
        }

        // Item khác → không làm gì
        return;
    }

    //========================================================
    // CHECK 3 BOOL
    //========================================================

    private void checkBool()
    {
        if (picture0_0On && picture1_0On && picture0_1On)
        {
            // Deactive 4 object
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                if (objectsToDeactivate[i] != null)
                    objectsToDeactivate[i].SetActive(false);
            }

            // Active object cuối
            for (int e = 0; e < objectToActivate.Length; e++)
            {
                if (objectToActivate[e] != null)
                    objectToActivate[e].SetActive(true);
            }
        }
    }
}