using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform clickEffect;

    public float showTime = 0.25f;

    Coroutine hideRoutine;


    void Start()
    {
        clickEffect.gameObject.SetActive(false);
    }


    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;


        ShowEffect(Input.mousePosition);
    }


    void ShowEffect(Vector2 mousePos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera,
            out Vector2 pos
        );


        clickEffect.localPosition = pos;
        clickEffect.gameObject.SetActive(true);


        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideEffect());
    }


    IEnumerator HideEffect()
    {
        yield return new WaitForSeconds(showTime);
        clickEffect.gameObject.SetActive(false);
    }
}
