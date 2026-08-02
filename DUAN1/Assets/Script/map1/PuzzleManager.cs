using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("All ItemData")]
    public ItemData[] items;

    private void Awake()
    {
        Instance = this;
    }

    public ItemData GetItem(int id)
    {
        foreach (ItemData item in items)
        {
            if (item.itemID == id)
                return item;
        }

        return null;
    }
}