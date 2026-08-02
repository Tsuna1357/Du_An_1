using UnityEngine;

public class TeddyPuzzle : Interactable
{
    [Header("Reference")]
    public CameraMap1 cameraMap;
    public Animator animator;

    [Header("Hidden Item")]
    public GameObject itemTrigger;

    bool solved;

    void Start()
    {
        if (itemTrigger != null)
            itemTrigger.SetActive(false);
    }

    public override void OnClick()
    {
        // Chưa focus thì không cho tương tác
        if (!cameraMap.isFocus)
            return;

        // Đã cắt rồi
        if (solved)
            return;

        // Chưa chọn item
        if (InventoryManager.Instance.SelectedItem == null)
            return;

        // Không phải kéo
        if (InventoryManager.Instance.SelectedItem.itemID != 0)
        {
            Debug.Log("Phải dùng kéo.");
            return;
        }

        // Đổi animation
        animator.SetInteger("teddy_state", 1);

        solved = true;

        // Hiện vùng click của vật phẩm
        if (itemTrigger != null)
            itemTrigger.SetActive(true);

        Debug.Log("Đã cắt gấu.");
    }

    public void ItemCollected()
    {
        animator.SetInteger("teddy_state", 2);
    }
}