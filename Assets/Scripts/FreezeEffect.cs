using UnityEngine;

public class FreezeEffect : IShaderEffect
{
    private readonly Material _material;
    private static readonly int EffectProperty = Shader.PropertyToID("_EffectAmount");

    public FreezeEffect(Material material)
    {
        _material = material;
    }

    public void SetProgress(float progress)
    {
        _material.SetFloat(EffectProperty, progress);
    }
}