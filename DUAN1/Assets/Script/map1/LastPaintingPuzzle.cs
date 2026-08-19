using UnityEngine;

public class LastPaintingPuzzle : Interactable
{
    [Header("Required Item IDs")]
    public int noItemID;
    public int nhanItemID;
    public int hoaItemID;

    [Header("Objects To Activate")]
    public GameObject noObject;
    public GameObject nhanObject;
    public GameObject hoaObject;

    [Header("Final Object")]
    public GameObject objectToDeactivate;

    [Header("States")]
    public bool isno = false;
    public bool isnhan = false;
    public bool ishoa = false;

    //==================================================
    // PLAYER CLICK
    //==================================================

    public override void OnClick()
    {
        CheckItem();
    }

    //==================================================
    // CHECK SELECTED ITEM
    //==================================================

    void CheckItem()
    {
        if (InventoryManager.Instance == null)
            return;

        int selectedID = InventoryManager.Instance.SelectedItemID;

        // Không chọn item
        if (selectedID == -1)
            return;

        //==================================================
        // NO
        //==================================================

        if (selectedID == noItemID)
        {
            // Tránh dùng lại nếu đã đặt rồi
            if (isno)
                return;

            isno = true;

            if (noObject != null)
                noObject.SetActive(true);

            // Xóa item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            CheckBool();
            return;
        }

        //==================================================
        // NHAN
        //==================================================

        if (selectedID == nhanItemID)
        {
            if (isnhan)
                return;

            isnhan = true;

            if (nhanObject != null)
                nhanObject.SetActive(true);

            // Xóa item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            CheckBool();
            return;
        }

        //==================================================
        // HOA
        //==================================================

        if (selectedID == hoaItemID)
        {
            if (ishoa)
                return;

            ishoa = true;

            if (hoaObject != null)
                hoaObject.SetActive(true);

            // Xóa item khỏi inventory
            InventoryManager.Instance.RemoveSelectedItem();

            CheckBool();
            return;
        }

        // Item khác thì không làm gì
        return;
    }

    //==================================================
    // CHECK ALL BOOL
    //==================================================

    private void CheckBool()
    {
        if (isno && isnhan && ishoa)
        {
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
        }
    }
}