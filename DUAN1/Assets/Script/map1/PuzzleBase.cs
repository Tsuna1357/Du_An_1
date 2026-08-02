using UnityEngine;

public class PuzzleBase : Interactable
{
    [Header("Reference")]
    public CameraMap1 cameraMap;

    [Header("Puzzle States")]
    public PuzzleState[] states;

    [HideInInspector]
    public int currentState = 0;

    public override void OnClick()
    {
        // Puzzle đã kết thúc
        if (currentState == -1)
            return;

        // Chưa gán Camera
        if (cameraMap == null)
        {
            Debug.LogError(name + " chưa gán CameraMap1");
            return;
        }

        // Chưa focus
        if (!cameraMap.isFocus)
            return;

        PuzzleState state = GetCurrentState();

        if (state == null)
            return;

        //----------------------------------------
        // Check Required Item
        //----------------------------------------

        if (state.requireItemID != -1)
        {
            if (InventoryManager.Instance.SelectedItemID != state.requireItemID)
                return;

            if (state.consumeItem)
            {
                InventoryManager.Instance.RemoveSelectedItem();
            }
        }

        //----------------------------------------
        // Execute Actions
        //----------------------------------------

        if (state.actions != null)
        {
            foreach (PuzzleActionData action in state.actions)
            {
                if (action == null)
                    continue;

                Execute(action);
            }
        }

        //----------------------------------------
        // Change State
        //----------------------------------------

        if (state.autoNext)
        {
            currentState = state.nextState;
        }
    }

    //--------------------------------------------------------

    PuzzleState GetCurrentState()
    {
        foreach (PuzzleState state in states)
        {
            if (state.stateID == currentState)
                return state;
        }

        return null;
    }

    //--------------------------------------------------------

    void Execute(PuzzleActionData action)
    {
        switch (action.action)
        {
            case PuzzleAction.ActiveObject:

                if (action.targetObject != null)
                    action.targetObject.SetActive(true);

                break;

            case PuzzleAction.DeactiveObject:

                if (action.targetObject != null)
                    action.targetObject.SetActive(false);

                break;

            case PuzzleAction.AnimatorBool:

                if (action.animator != null)
                    action.animator.SetBool(action.boolName, action.boolValue);

                break;

            case PuzzleAction.AnimatorInt:

                if (action.animator != null)
                    action.animator.SetInteger(action.intName, action.intValue);

                break;

            case PuzzleAction.AddItem:

                ItemData addItem = PuzzleManager.Instance.GetItem(action.addItemID);

                if (addItem != null)
                    InventoryManager.Instance.AddItem(addItem);

                break;

            case PuzzleAction.RemoveItem:

                InventoryManager.Instance.RemoveItemByID(action.removeItemID);

                break;

            case PuzzleAction.ReplaceItem:

                ItemData newItem = PuzzleManager.Instance.GetItem(action.replaceNewID);

                if (newItem != null)
                    InventoryManager.Instance.ReplaceItem(action.replaceOldID, newItem);

                break;

            case PuzzleAction.OpenPopup:

                cameraMap.OpenPopup(action.popupUI);

                break;

            case PuzzleAction.ClosePopup:

                cameraMap.ClosePopup();

                break;

            case PuzzleAction.Finish:

                currentState = -1;

                Debug.Log(name + " Finished");

                break;
        }
    }
}