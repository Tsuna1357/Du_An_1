using UnityEditor.Build;
using UnityEngine;

public class PaintingStateControoler : MonoBehaviour
{
    [SerializeField] private int aniState;
    [SerializeField] private GameObject painting;
    private Animator paintingAni;
    private void Awake()
    {
        paintingAni = painting.GetComponent<Animator>();
        paintingAni.SetInteger("Painting_State", aniState);
    }
}
