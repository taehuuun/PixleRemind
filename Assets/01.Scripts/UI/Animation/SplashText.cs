using System.Collections;
using TMPro;
using UnityEngine;

public class SplashText : MonoBehaviour
{
    [SerializeField] private TMP_Text[] splashText;
    [SerializeField] private float delay;
    [SerializeField] private float completeDelay;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float moveDuration;
    [SerializeField] private string fullText;
    
    public bool Complete { get; private set;}

    private WaitForSeconds _waitForDelay;
    private WaitForSeconds _waitForMoveDelay;
    private WaitForSeconds _waitForCompleteDelay;
    
    private void Start()
    {
        _waitForDelay = new WaitForSeconds(delay);
        _waitForMoveDelay = new WaitForSeconds(moveDuration + delay);
        _waitForCompleteDelay = new WaitForSeconds(completeDelay);
        Complete = false;
        
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            splashText[i].text = fullText[i].ToString();
            StartCoroutine(AnimateText(splashText[i].transform));
            yield return _waitForDelay;
        }

        // Step 2: Move LT up and H down
        yield return _waitForDelay;
        StartCoroutine(MoveText(splashText[0].transform, new Vector3(0, 150, 0), moveDuration));
        StartCoroutine(MoveText(splashText[1].transform, new Vector3(0, 150, 0), moveDuration));
        StartCoroutine(MoveText(splashText[2].transform, new Vector3(0, -150, 0), moveDuration));

        // Step 3: Move LT to right and H to left
        yield return _waitForMoveDelay;
        StartCoroutine(MoveText(splashText[0].transform, new Vector3(50, 0, 0), moveDuration));
        StartCoroutine(MoveText(splashText[1].transform, new Vector3(120, 0, 0), moveDuration));
        StartCoroutine(MoveText(splashText[2].transform, new Vector3(-225, 0, 0), moveDuration));
        
        yield return _waitForDelay;
        
        for (int i = 0; i < fullText.Length; i++)
        {
            StartCoroutine(AnimateText(splashText[i].transform));
            yield return _waitForDelay;
        }
        
        yield return _waitForCompleteDelay;
        
        Complete = true;
    }

    private IEnumerator AnimateText(Transform textTransform)
    {
        Vector3 originalScale = textTransform.localScale;

        yield return ScaleText(textTransform, originalScale * 1.1f, scaleDuration);
        yield return ScaleText(textTransform, originalScale, scaleDuration);
    }
    
    private IEnumerator ScaleText(Transform textTransform, Vector3 targetScale, float duration)
    {
        Vector3 originalScale = textTransform.localScale;
        float time = 0;

        while (time <= duration)
        {
            float t = time / duration;
            textTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        textTransform.localScale = targetScale;
    }
    
    private IEnumerator MoveText(Transform textTransform, Vector3 offset, float duration)
    {
        Vector3 originalPosition = textTransform.localPosition;
        Vector3 targetPosition = originalPosition + offset;
        float time = 0;

        while (time <= duration)
        {
            float t = time / duration;
            textTransform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        textTransform.localPosition = targetPosition;
    }
}