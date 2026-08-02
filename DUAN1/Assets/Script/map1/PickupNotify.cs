using UnityEngine;

public class PickupNotify : MonoBehaviour
{
    public TeddyPuzzle teddyPuzzle;

    void OnDisable()
    {
        if (teddyPuzzle != null)
        {
            teddyPuzzle.ItemCollected();
        }
    }
}