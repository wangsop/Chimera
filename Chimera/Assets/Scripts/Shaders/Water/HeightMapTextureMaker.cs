using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class HeightMapTextureMaker
{
    private static int gradientLength = 16;

    public static void WaterGeneration(HashSet<Vector2Int> floor, Material waterMaterial, Texture2D heightMap, Texture2D noCaustics, Texture2D caustic, Texture2D causticHighlights, Texture2D seaFoam, int shoreLength)
    {
        var bounds = ComputeBounds(floor);
        var wallTiles = FindEdgeWalls(floor, Direction2D.eightDirectionsList);
        var noCausticsSet = FindCausticsMask(floor, wallTiles, Direction2D.eightDirectionsList, shoreLength);
        heightMap = CreateHeightMapTexture(floor, wallTiles, bounds);
        noCaustics = CreateWhiteTransparent_BlackOpaqueTexture(noCausticsSet, bounds);
        //RuntimeTexPeek.Show(noCausticArea, 256);
        var gridMin  = new Vector4(bounds.minX, bounds.minY, 0, 0);
        var gridSize = new Vector4(bounds.width, bounds.height, 0, 0);
        var tileSize = new Vector4(1, 1, 0, 0);
        waterMaterial.SetTexture("_HeightTex", heightMap);
        waterMaterial.SetFloat("_Pixelization", 16f);
        waterMaterial.SetVector("_GridMin",  new Vector4(bounds.minX, bounds.minY, 0, 0));
        waterMaterial.SetVector("_GridSize", new Vector4(bounds.width, bounds.height, 0, 0));
        waterMaterial.SetVector("_TileSize", new Vector4(1, 1, 0, 0));
        waterMaterial.SetColor("_ShallowColor", new Color(0.247f, 0.851f, 0.820f, 1.0f)/*new Color(0.247f, 0.475f, 0.659f, 1f)*/); // #3F79A8
        waterMaterial.SetColor("_DeepColor", new Color(0.016f, 0.184f, 0.239f, 1.0f)/*new Color(0.039f, 0.133f, 0.239f, 1f)*/); // #0A223D
        waterMaterial.SetTexture("_noCaustics", noCaustics);
        waterMaterial.SetColor("_CausticColor", new Color(0.310f, 1.000f, 0.878f, 1.0f)/*new Color(0.290f, 0.561f, 0.722f, 1.000f)*/); // #4A8FB8
        waterMaterial.SetColor("_CausticHighlightsColor", new Color(0.722f, 1.000f, 0.976f, 1.0f)/*new Color(0.851f, 0.953f, 1.000f, 1.000f)*/); // #D9F3FF
        waterMaterial.SetFloat("_SquashAmount", 1.4f);
        waterMaterial.SetFloat("_CausticScale", 0.08f);
        waterMaterial.SetFloat("_CausticDepth", 0.18f);
        waterMaterial.SetFloat("_CausticSpeed", 0.8f);
        waterMaterial.SetFloat("_CausticMovementScale", 0.5f);
        waterMaterial.SetFloat("_CausticMovementAmount", 0.04f);
        waterMaterial.SetFloat("_CausticHighlightsBlend", 0.08f);
        waterMaterial.SetFloat("_CausticFaderScale", 0.1f);
        waterMaterial.SetFloat("_CausticFaderMultiplier", 1f);
        waterMaterial.SetFloat("_CausticStrength", 0.8f);
        waterMaterial.SetFloat("_SpecularBlend", 0.1f);
        waterMaterial.SetFloat("_SpecularScale", 3f);
        waterMaterial.SetFloat("_SpecularScale2", 10f);
        waterMaterial.SetFloat("_SpecularSpeed", 0.2f);
        waterMaterial.SetFloat("_SpecularThreshold", -0.8f);
        waterMaterial.SetColor("_SpecularColor", new Color(0.910f, 0.984f, 1.000f, 1.0f)/*new Color(1.000f, 0.992f, 0.957f, 1.000f)*/);
        waterMaterial.SetGradient(
            new GradientColorKey[]
            {
                new GradientColorKey(new Color(0.49f, 0.66f, 0.69f), 0f),
                new GradientColorKey(new Color(0.55f, 0.74f, 0.76f), 0.276f),
                new GradientColorKey(new Color(0.19f, 0.27f, 0.30f), 1f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
            // optional: , "_GradientTex", 256
        );
        waterMaterial.SetColor("_FoamColor", new Color(0.486f, 0.812f, 0.769f, 1.000f));

        waterMaterial.SetFloat("_Alpha", 0.75f);
    }

    
    private static HashSet<Vector2Int> FindCausticsMask(HashSet<Vector2Int> floor, HashSet<Vector2Int> edges, List<Vector2Int> eightDirectionsList, int length)
    {
        HashSet<Vector2Int> noCaustics = new HashSet<Vector2Int>();
        foreach (var pos in edges)
        {
            for (int i = 1; i <= length; i++)
            {
                foreach (var dir in eightDirectionsList)
                {
                    var neighbour = pos + (dir * i);
                    if (floor.Contains(neighbour))
                    {
                        noCaustics.Add(neighbour);
                    }
                }
            }
        }
        return noCaustics;
    }

    private static Texture2D CreateHeightMapTexture(
        HashSet<Vector2Int> floorTiles,
        HashSet<Vector2Int> wallTiles,
        GridBounds bounds,
        FilterMode filter = FilterMode.Bilinear,     // controls smoothness of gradient
        TextureWrapMode wrap = TextureWrapMode.Clamp, // prevents texture repeating
        bool useAlphaMask = true,                     // toggle transparency for non-floor
        bool invert = false,                          // flip gradient (dark near walls)
        float brightness = 1f,                        // overall intensity multiplier
        float contrast = 1f,                          // raises midtones or deepens contrast
        AnimationCurve remapCurve = null              // fine control over gradient shape
        )
    {
        var distances = MultiSourceBFS(floorTiles, wallTiles, Direction2D.eightDirectionsList, bounds);
        var normalized = Normalization(distances);
        foreach (var (x, y) in normalized)
        {
            //Debug.Log((x,y));
        }

        Texture2D tex = new Texture2D(bounds.width, bounds.height, TextureFormat.RGBA32, false);
        tex.filterMode = filter;
        tex.wrapMode = wrap;
        tex.anisoLevel = 0;

        Color[] pixels = new Color[bounds.width * bounds.height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = new Color(0, 0, 0, 0);

        // Default curve: linear
        if (remapCurve == null)
            remapCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        foreach (var pos in floorTiles)
        {
            Vector2Int texPos = ToTexXY(pos, bounds);
            float h = normalized.TryGetValue(pos, out float val) ? val : 0f;

            // Gradient remap: contrast, invert, curve, brightness
            if (invert) h = 1f - h;
            h = Mathf.Pow(h, contrast);       // simple contrast curve
            h = remapCurve.Evaluate(h);       // user curve shaping
            h = Mathf.Clamp01(h * brightness);

            Color c = new Color(h, h, h, useAlphaMask ? 1f : 1f);
            pixels[texPos.y * bounds.width + texPos.x] = c;
        }

        tex.SetPixels(pixels);
        tex.Apply(false);
        return tex;
    }
    private static Texture2D CreateWhiteTransparent_BlackOpaqueTexture(
            HashSet<Vector2Int> whitePositions,
            GridBounds bounds,
            FilterMode filter = FilterMode.Point,
            TextureWrapMode wrap = TextureWrapMode.Clamp
        )
    {
        Texture2D tex = new Texture2D(bounds.width, bounds.height, TextureFormat.RGBA32, false);
        tex.filterMode = filter;
        tex.wrapMode = wrap;
        tex.anisoLevel = 0;

        // Start black, fully opaque (caustics allowed)
        Color[] pixels = new Color[bounds.width * bounds.height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = new Color(0f, 0f, 0f, 1f);

        if (whitePositions != null)
        {
            foreach (var pos in whitePositions)
            {
                // Skip anything outside the overall bounds just in case
                if (pos.x < bounds.minX || pos.x >= bounds.minX + bounds.width ||
                    pos.y < bounds.minY || pos.y >= bounds.minY + bounds.height) continue;

                Vector2Int tp = ToTexXY(pos, bounds);
                int idx = tp.y * bounds.width + tp.x;
                pixels[idx] = new Color(1f, 1f, 1f, 0f); // white, fully transparent (block caustics)
            }
        }

        tex.SetPixels(pixels);
        tex.Apply(false);
        return tex;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="floorPositions"></param>
    /// <param name="edgePositions"></param>
    /// <param name="directionList"></param>
    /// <returns></returns>
    private static Dictionary<Vector2Int, int> MultiSourceBFS(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> edgePositions, List<Vector2Int> directionList, GridBounds bounds)
    {
        var distances = new Dictionary<Vector2Int, int>(floorPositions.Count);
        foreach (var p in floorPositions) distances[p] = gradientLength;
        Queue<Vector2Int> nonVisited = new Queue<Vector2Int>();
        foreach (var pos in edgePositions)
        {
            distances[pos] = 0;
            nonVisited.Enqueue(pos);
        }
        while (nonVisited.Count != 0)
        {
            var pos = nonVisited.Dequeue();
            if (distances[pos] + 1 >= gradientLength)
            {
                continue;
            }
            foreach (var dir in directionList)
            {
                var neighbour = pos + dir;
                if (floorPositions.Contains(neighbour) && distances[neighbour] > (distances[pos] + 1))
                {
                    distances[neighbour] = distances[pos] + 1;
                    nonVisited.Enqueue(neighbour);
                }
            }
        }
        return distances;
    }




    private static HashSet<Vector2Int> FindEdgeWalls(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> edgeTiles = new HashSet<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            foreach (var dir in directionList)
            {
                var neighbourPosition = pos + dir;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    edgeTiles.Add(pos);
                    break;
                }
            }
        }
        return edgeTiles;
    }

    private static Dictionary<Vector2Int, float> Normalization(Dictionary<Vector2Int, int> distances)
    {
        Dictionary<Vector2Int, float> normalized_distances = new Dictionary<Vector2Int, float>();
        foreach (var (key, value) in distances)
        {
            normalized_distances.Add(key, value == gradientLength ? 0 : 1 - Mathf.Clamp01(value / (float)gradientLength));
        }
        return normalized_distances;
    }

    public static void SetGradient(this Material mat,
                                   GradientColorKey[] colorKeys,
                                   GradientAlphaKey[] alphaKeys,
                                   string propertyName = "_GradientTex",
                                   int width = 256)
    {
        // 1) Build a Unity Gradient from the keys you passed.
        var g = new Gradient();
        g.SetKeys(colorKeys, alphaKeys);

        // 2) Bake to a 1×N texture (HDR-capable), with mips for smoother sampling.
        var tex = new Texture2D(width, 1, TextureFormat.RGBAHalf, /*mipmap*/ true, /*linear*/ true);
        var pixels = new Color[width];
        for (int x = 0; x < width; x++)
        {
            float t = x / (width - 1f);
            pixels[x] = g.Evaluate(t);
        }
        tex.SetPixels(pixels);
        tex.wrapMode   = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Trilinear; // smoother than bilinear with mips
        tex.Apply(true);

        // 3) Send to the shader.
        mat.SetTexture(propertyName, tex);
    }

    private static Vector2Int ToTexXY(Vector2Int position, GridBounds bounds)
    {
        int x = position.x - bounds.minX;
        int y = position.y - bounds.minY;
        return new Vector2Int(x, y);
    }

    private struct GridBounds { public int minX, minY, width, height; }

    private static GridBounds ComputeBounds(HashSet<Vector2Int> floor)
    {
        int minX = int.MaxValue, minY = int.MaxValue;
        int maxX = int.MinValue, maxY = int.MinValue;
        foreach (var p in floor)
        {
            if (p.x < minX) minX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.x > maxX) maxX = p.x;
            if (p.y > maxY) maxY = p.y;
        }
        return new GridBounds
        {
            minX = minX,
            minY = minY,
            width = maxX - minX + 1,
            height = maxY - minY + 1
        };
    }
    /*

    Normalization
    maxD = max(dist[x,y]) over all finite entries
    if maxD == 0: maxD = 1

    height01[x,y] for each pixel:
        if dist[x,y] is INF:  // non-floor/void/wall interior
            height01[x,y] = 0   // or mark alpha=0 later
        else:
            t = dist[x,y] / maxD          // 0 near walls â†’ 1 far
            height01[x,y] = 1 - clamp01(t)  // 1 near walls â†’ 0 far

    MultiSourceBFS(floorPositions, edgePositions, dirs, gradientLength):
    INF = gradientLength
    dist = map from tile -> int
    for each p in floorPositions:
        dist[p] = INF

    Q = empty queue

    // seed all edge tiles at distance 0
    for each e in edgePositions:
        dist[e] = 0
        enqueue(Q, e)

    // BFS expansion (stop at cap)
    while Q not empty:
        u = dequeue(Q)
        if dist[u] + 1 >= gradientLength:
            continue   // already at/over cap; don't push farther

        for each d in dirs:
            v = u + d
            if v in floorPositions and dist[v] > dist[u] + 1:
                dist[v] = dist[u] + 1
                enqueue(Q, v)

    return dist  // tiles still at INF were > cap steps from any edge
    First implementation 
    /*foreach (var dir in directionList)
            {
                for (int i = 0; i < gradientLength; i++)
                {
                    var neighbor = pos + dir * i;
                    if (floorPositions.Contains(neighbor))
                    {
                        if (distances[neighbor] > i)
                        {
                            distances[neighbor] = i;
                        }
                        continue;
                    }
                    break;
                }
    }*/
    
}
