using UnityEngine;

public class MaterialHelper
{
    private const int RenderQueueDefault = -1;

    private static readonly string KeywordAlphaTest = "_ALPHATEST_ON";
    private static readonly string KeywordAlphaBlend = "_ALPHABLEND_ON";
    private static readonly string KeywordAlphaPremultiply = "_ALPHAPREMULTIPLY_ON";

    private int _shaderModeOpaque = 0;
    private int _shaderModeTransparent = 3;
    private UnityEngine.Rendering.BlendMode _srcBlendMode = UnityEngine.Rendering.BlendMode.SrcAlpha;
    private UnityEngine.Rendering.BlendMode _dstBlendMode = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
    private int _renderQueueTransparent = (int)UnityEngine.Rendering.RenderQueue.Transparent;

    private Renderer _renderer;
    private Material _materialInstance;
    private Color _originalColor;

    public MaterialHelper(Renderer renderer)
    {
        _renderer = renderer;
        _materialInstance = Object.Instantiate(_renderer.material);
        _renderer.material = _materialInstance;

        _originalColor = _materialInstance.color;
    }

    public void SetOpaque()
    {
        _materialInstance.SetFloat("_Mode", _shaderModeOpaque);
        _materialInstance.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _materialInstance.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _materialInstance.SetInt("_ZWrite", 1);

        _materialInstance.DisableKeyword(KeywordAlphaTest);
        _materialInstance.DisableKeyword(KeywordAlphaBlend);
        _materialInstance.DisableKeyword(KeywordAlphaPremultiply);

        _materialInstance.renderQueue = RenderQueueDefault;
    }

    public void SetTransparent()
    {
        _materialInstance.SetFloat("_Mode", _shaderModeTransparent);
        _materialInstance.SetInt("_SrcBlend", (int)_srcBlendMode);
        _materialInstance.SetInt("_DstBlend", (int)_dstBlendMode);
        _materialInstance.SetInt("_ZWrite", 0);

        _materialInstance.DisableKeyword(KeywordAlphaTest);
        _materialInstance.EnableKeyword(KeywordAlphaBlend);
        _materialInstance.DisableKeyword(KeywordAlphaPremultiply);

        _materialInstance.renderQueue = _renderQueueTransparent;
    }

    public void SetAlpha(float alpha)
    {
        Color color = _materialInstance.color;
        color.a = alpha;
        _materialInstance.color = color;
    }

    public void ResetToOriginal()
    {
        Color color = _originalColor;
        color.a = 1f;
        _materialInstance.color = color;
        SetOpaque();
    }
}
