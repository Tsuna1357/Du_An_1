using UnityEngine;

public class PalletColorPuzzle : MonoBehaviour
{
    [Header("Item ID")]
    public int redItemID;
    public int yellowItemID;

    [Header("Color Objects")]
    public GameObject colorRed;
    public GameObject colorYellow;
    public GameObject mixedColor;

    [Header("Color State")]
    public bool isRed = false;
    public bool isYellow = false;


    // ==========================================
    // GỌI HÀM NÀY KHI NGƯỜI CHƠI DÙNG ITEM
    // ==========================================

    public void CheckItem()
    {
        int selectedID = InventoryManager.Instance.SelectedItemID;


        // =========================
        // MÀU ĐỎ
        // =========================

        if (selectedID == redItemID)
        {
            colorRed.SetActive(true);

            isRed = true;

            CheckColor();

            return;
        }


        // =========================
        // MÀU VÀNG
        // =========================

        if (selectedID == yellowItemID)
        {
            colorYellow.SetActive(true);

            isYellow = true;

            CheckColor();

            return;
        }


        // =========================
        // ITEM KHÁC
        // =========================

        return;
    }


    // ==========================================
    // KIỂM TRA ĐÃ CÓ ĐỦ 2 MÀU CHƯA
    // ==========================================

    private void CheckColor()
    {
        if (isRed && isYellow)
        {
            colorRed.SetActive(false);

            colorYellow.SetActive(false);

            mixedColor.SetActive(true);
        }
    }
}