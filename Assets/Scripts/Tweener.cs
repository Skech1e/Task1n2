using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum AnimType
{
    Position, Scale, Fade
}
public enum Axis
{
    Horizontal, Vertical, Both
}
public class Tweener : MonoBehaviour
{
    public AnimType anim;
    public Axis axis;
    public Ease easeType;

    public float startValue, endValue;
    [Range(0f, 10f)]
    public float duration;
    [Range(0, 20f)]
    public float delayAnim;
    public bool RunOnEnable;

    private void OnEnable()
    {
        if (RunOnEnable)
            Animate();
    }
    private void OnDisable()
    {

    }

    private void OnValidate()
    {
        if (anim == AnimType.Fade)
        {
            Image img;
            if (transform.TryGetComponent(out img))
            {
                endValue = img.color.a;
            }
        }
    }

    public void Animate() => StartCoroutine(Tween());

    IEnumerator Tween()
    {
        if (anim == AnimType.Position)
        {
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            Vector2 pos = transform.localPosition;
            switch (axis)
            {
                case Axis.Horizontal:
                    pos.x = startValue;
                    transform.localPosition = pos;
                    yield return new WaitForSeconds(delayAnim);
                    rectTransform.DOAnchorPosX(endValue, duration).SetEase(easeType);
                    break;
                case Axis.Vertical:
                    pos.y = startValue;
                    transform.localPosition = pos;
                    yield return new WaitForSeconds(delayAnim);
                    rectTransform.DOAnchorPosY(endValue, duration).SetEase(easeType);
                    break;
            }
        }
        else if (anim == AnimType.Scale)
        {
            Vector2 scale = transform.localScale;
            switch (axis)
            {
                case Axis.Horizontal:
                    scale.x = startValue;
                    transform.localScale = scale;
                    yield return new WaitForSeconds(delayAnim);
                    transform.DOScaleX(endValue, duration).SetEase(easeType);
                    break;
                case Axis.Vertical:
                    scale.y = startValue;
                    transform.localScale = scale;
                    yield return new WaitForSeconds(delayAnim);
                    transform.DOScaleY(endValue, duration).SetEase(easeType);
                    break;
                case Axis.Both:
                    scale.x = scale.y = startValue;
                    transform.localScale = scale;
                    yield return new WaitForSeconds(delayAnim);
                    transform.DOScale(endValue, duration).SetEase(easeType);
                    break;
            }
        }
        else
        {
            Image img;
            if (transform.TryGetComponent(out img))
            {
                var colour = img.color;
                colour.a = startValue;
                img.color = colour;
                img.DOFade(endValue, duration).SetEase(easeType);
            }

        }

    }
    public void SceneLoad(int index) => SceneManager.LoadScene(index);
}
