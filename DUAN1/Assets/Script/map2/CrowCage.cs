using UnityEngine;


public class CrowCage : MonoBehaviour
{

    public GameObject deadCrow;
    public GameObject aliveCrow;


    public void PutCrow()
    {

        ItemData item = InventoryManager.Instance.SelectedItem;


        if (item != null && item.itemID == 1)
        {

            InventoryManager.Instance.RemoveSelectedItem();


            deadCrow.SetActive(true);


            Map2Manager.Instance.crowPlaced = true;


        }

    }


    public void Revive()
    {

        if (Map2Manager.Instance.crowPlaced)
        {

            deadCrow.SetActive(false);

            aliveCrow.SetActive(true);


            Map2Manager.Instance.crowAlive = true;

        }

    }

}