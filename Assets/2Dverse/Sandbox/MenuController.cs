using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour {
    public enum VerticalOrHorizontal {
        Vertical,
        Horizontal
    };

    public VerticalOrHorizontal Align;
    
    private bool showing;
    public float Time;
    
    private void Start () {
        showing = false;

        switch (Align) {
            case VerticalOrHorizontal.Vertical:
                transform.DOScaleY(0, 0);
            break;
            
            case VerticalOrHorizontal.Horizontal:
                transform.DOScaleX(0, 0);
            break;
        }
    }

    public void Switch () {
        showing = !showing;

        switch (Align) {
            case VerticalOrHorizontal.Vertical:
                transform.DOScaleY(showing ? 1 : 0, Time);
            break;
            
            case VerticalOrHorizontal.Horizontal:
                transform.DOScaleX(showing ? 1 : 0, Time);
                break;
        }
    }
}
