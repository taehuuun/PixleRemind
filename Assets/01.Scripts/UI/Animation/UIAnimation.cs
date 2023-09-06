using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIAnimation
{
    public static IEnumerator FadeEffect(Graphic ui, float duration, FadeType type)
    {
        float elapsedTime = 0f;
        Color color = ui.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            color.a = type == FadeType.In
                ? Mathf.Lerp(0, 1, elapsedTime / duration)
                : Mathf.Lerp(1, 0, elapsedTime / duration);

            ui.color = color;
            yield return null;
        }
    }
    public static IEnumerator ShowTextOneByOne(TMP_Text[] text, string fullText, float delay, float scaleDuration)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            text[i].text = fullText[i].ToString();
            yield return ScaleUI(text[i].transform, text[i].transform.localScale * 1.1f, scaleDuration);
            yield return new WaitForSeconds(delay);
        }
    }
    public static IEnumerator ScaleUI(Transform uiTransform, Vector3 targetScale, float duration)
    {
        Vector3 originScale = uiTransform.localScale;
        float time = 0;
        while (time <= duration)
        {
            float t = time / duration;
            uiTransform.localScale = Vector3.Lerp(originScale, targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        uiTransform.localScale = originScale;
    }
    public static IEnumerator MoveUI(Transform uiTransform, Vector3 offset, float duration)
    {
        Vector3 originPosition = uiTransform.localPosition;
        Vector3 targetPosition = originPosition + offset;
        float time = 0;

        while (time <= duration)
        {
            float t = time / duration;
            uiTransform.localPosition = Vector3.Lerp(originPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        uiTransform.localPosition = targetPosition;
    }
}