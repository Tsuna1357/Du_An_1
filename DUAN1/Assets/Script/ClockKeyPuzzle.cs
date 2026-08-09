using UnityEngine;

public class ClockKeyPuzzle : Interactable
{
    [Header("Required Items")]
    public int kimPhutItemID = 11;
    public int kimGioItemID = 10;

    [Header("Clock Objects")]
    public GameObject kimPhut;
    public GameObject kimGio;

    [Header("Lock")]
    public GameObject khoaTu;

    [Header("State")]
    public bool kimGioOn = false;
    public bool kimPhutOn = false;

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
        // KIM PHÚT - ITEM ID 11
        //====================================================

        if (selectedID == kimPhutItemID)
        {
            kimPhutOn = true;

            // Hiện kim phút
            if (kimPhut != null)
                kimPhut.SetActive(true);

            // Xóa kim phút khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            // Kiểm tra cả 2 kim
            checkBool();

            return;
        }

        //====================================================
        // KIM GIỜ - ITEM ID 10
        //====================================================

        if (selectedID == kimGioItemID)
        {
            kimGioOn = true;

            // Hiện kim giờ
            if (kimGio != null)
                kimGio.SetActive(true);

            // Xóa kim giờ khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            // Kiểm tra cả 2 kim
            checkBool();

            return;
        }

        //====================================================
        // ITEM KHÁC
        //====================================================

        return;
    }

    //========================================================
    // CHECK BOTH BOOL
    //========================================================

    private void checkBool()
    {
        if (kimGioOn && kimPhutOn)
        {
            if (khoaTu != null)
                khoaTu.SetActive(false);
        }
    }
}