using UnityEngine;


public class PutDeadCrow : Interactable
{

    public GameObject deadCrowInside;


    public override void OnClick()
    {

        ItemData item = InventoryManager.Instance.SelectedItem;


        if (item == null)
            return;


        // ID xác quạ = 1
        if (item.itemID == 1)
        {

            InventoryManager.Instance.RemoveSelectedItem();


            deadCrowInside.SetActive(true);


            gameObject.SetActive(false);


            PuzzleManager.instance.crowPlaced = true;


            Debug.Log("Đã đặt xác quạ vào lồng");

        }

    }
}