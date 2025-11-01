using UnityEngine;

public class RuntimeTexPeek : MonoBehaviour
{
    static RuntimeTexPeek _inst;
    Texture _tex;
    Rect _rect;

    public static void Show(Texture2D tex, int size = 256, int margin = 12)
    {
        if (tex == null) { Debug.LogWarning("RuntimeTexPeek.Show: tex is null"); return; }

        if (_inst == null)
        {
            var go = new GameObject("[TexPeek]");
            DontDestroyOnLoad(go);
            _inst = go.AddComponent<RuntimeTexPeek>();
        }

        _inst._tex = tex;

        // keep aspect ratio
        float w = size, h = size;
        float aspect = (float)tex.width / tex.height;
        if (aspect > 1f) h = w / aspect; else w = h * aspect;

        // top-left of Game view (IMGUI coords use y from top)
        _inst._rect = new Rect(margin, margin, w, h);
    }

    void OnGUI()
    {
        if (_tex == null) return;
        GUI.DrawTexture(_rect, _tex, ScaleMode.ScaleToFit, false); // no alpha blending
        GUI.Label(new Rect(_rect.x, _rect.y - 18, 600, 18),
                  $"HeightMap  ({_tex.width}×{_tex.height})");
    }
}
