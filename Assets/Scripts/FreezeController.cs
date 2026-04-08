using System.Collections;
using UnityEngine;

public class FreezeController : MonoBehaviour
{
    [SerializeField] private Renderer _characterRenderer;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private AnimationCurve _smoothCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private IShaderEffect _freezeEffect;
    private Coroutine _activeRoutine;

    void Start()
    {
        if (_characterRenderer != null)
        {
            _freezeEffect = new FreezeEffect(_characterRenderer.material);
            _freezeEffect.SetProgress(0f);
        }
    }

    public void ToggleFreeze()
    {
        if (_activeRoutine != null) StopCoroutine(_activeRoutine);
        _activeRoutine = StartCoroutine(ProcessEffectRoutine());
    }

    private IEnumerator ProcessEffectRoutine()
    {
        yield return ChangeProgress(0f, 1f);

        yield return new WaitForSeconds(1f);

        yield return ChangeProgress(1f, 0f);
    }

    private IEnumerator ChangeProgress(float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            float value = Mathf.Lerp(start, end, _smoothCurve.Evaluate(t));

            _freezeEffect.SetProgress(value);
            yield return null;
        }
        _freezeEffect.SetProgress(end);
    }
}
