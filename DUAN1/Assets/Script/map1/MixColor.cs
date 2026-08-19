using UnityEngine;

public class MixColor : Interactable
{
    [Header("Required Item IDs")]
    public int redItemID;
    public int blueItemID;

    [Header("State")]
    public bool isred = false;
    public bool isblue = false;

    [Header("Color Objects")]
    public GameObject colorred;
    public GameObject colorblue;
    public GameObject mixedcolor;

    //========================================================
    // PLAYER CLICK
    //========================================================

    public override void OnClick()
    {
        CheckItem();
    }

    //========================================================
    // CHECK ITEM
    //========================================================

    public void CheckItem()
    {
        if (InventoryManager.Instance == null)
            return;

        int selectedID = InventoryManager.Instance.SelectedItemID;

        // Không chọn item
        if (selectedID == -1)
            return;

        //====================================================
        // RED
        //====================================================

        if (selectedID == redItemID)
        {
            // Nếu đã dùng màu đỏ rồi thì không làm lại
            if (isred)
                return;

            isred = true;

            if (colorred != null)
                colorred.SetActive(true);

            CheckColor();

            return;
        }

        //====================================================
        // BLUE
        //====================================================

        if (selectedID == blueItemID)
        {
            // Nếu đã dùng màu xanh rồi thì không làm lại
            if (isblue)
                return;

            isblue = true;

            if (colorblue != null)
                colorblue.SetActive(true);

            CheckColor();

            return;
        }

        //====================================================
        // ITEM KHÁC
        //====================================================

        return;
    }

    //========================================================
    // CHECK COLOR
    //========================================================

    public void CheckColor()
    {
        if (isred && isblue)
        {
            if (mixedcolor != null)
                mixedcolor.SetActive(true);
        }
    }
}