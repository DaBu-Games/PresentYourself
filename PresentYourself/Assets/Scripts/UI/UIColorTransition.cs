using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIColorTransition : MonoBehaviour
{
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float holdDuration = 0.25f;
    
    private Color _targetColor;
    private Image _image;
    private Coroutine _routine;
    
    private void Start()
    { 
        _image = GetComponent<Image>();
        _image.enabled = false;

        GameEvents.OnColorTransition += Play;
    }

    private void OnDestroy()
    {
        GameEvents.OnColorTransition -= Play;
    }
    
    private void Play(bool isCorrect)
    {
        _targetColor = isCorrect ? correctColor : incorrectColor;
        
        if (_routine != null)
            StopCoroutine(_routine);

        _routine = StartCoroutine(ChangeImage());
    }

    private IEnumerator ChangeImage()
    {
        _image.enabled = true;

        Color start = _image.color;
        start.a = 0f;
        _image.color = start;
        
        yield return StartCoroutine(FadeToTarget(start, _targetColor));
        
        yield return new WaitForSeconds(holdDuration);
        
        yield return StartCoroutine(FadeToTarget(_targetColor, start));

        _image.color = start;
        _image.enabled = false;
    }

    private IEnumerator FadeToTarget(Color start, Color target)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            _image.color = Color.Lerp(start, target, t / fadeDuration);
            yield return null;
        }
        
        _image.color = target;
    }
}