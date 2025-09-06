using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_SwapImage : MonoBehaviour
{
    private Image image;

    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite1;
    }

    public void SwapImage()
    {
        image.sprite = (image.sprite == sprite1) ? sprite2 : sprite1;
    }

    public void SetImage1()
    {
        image.sprite = sprite1;
    }

    public void SetImage2()
    {
        image.sprite = sprite2;
    }
}
