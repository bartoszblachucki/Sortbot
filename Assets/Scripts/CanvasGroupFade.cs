using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float fadingTime = 1;

    [SerializeField] private float maxAlpha = 0.7f;
    [SerializeField] private float alphaOnAwake = 1;
    [SerializeField] private bool interactableOnAwake = true;
    
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = alphaOnAwake;
        _canvasGroup.interactable = interactableOnAwake;
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadingOut());
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadingIn());
    }

    private IEnumerator FadingIn()
    {
        _canvasGroup.interactable = true;
        while (_canvasGroup.alpha < maxAlpha)
        {
            _canvasGroup.alpha += fadingTime * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadingOut()
    {
        _canvasGroup.interactable = false;
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= fadingTime * Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_canvasGroup.interactable)
            _canvasGroup.alpha += 0.3f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _canvasGroup.alpha -= 0.3f;
    }
}
