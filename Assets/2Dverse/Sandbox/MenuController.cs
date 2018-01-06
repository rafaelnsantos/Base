using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour {

    private bool active;
    public float Time;

    private void Start () {
        active = false;

        transform.DOScaleY(0, 0);
        transform.DOScaleX(0, 0);
    }

    public void Switch () {
        active = !active;

        transform.DOScaleX(active ? 1 : 0, Time);
        transform.DOScaleY(active ? 1 : 0, Time);
    }
}