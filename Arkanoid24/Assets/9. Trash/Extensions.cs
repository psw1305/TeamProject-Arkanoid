using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// 카메라 사이즈에 맞는 엣지 콜라이더 바운드 생성
    /// </summary>
    /// <param name="screenEdge">엣지 콜라이더</param>
    public static void GenerateCameraBounds(this EdgeCollider2D screenEdge)
    {
        var halfScreenHeight = Camera.main.orthographicSize;
        var halfScreenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        
        var bounds = new Vector2[4];
        bounds[0] = new Vector2(-halfScreenWidth, -halfScreenHeight);
        bounds[1] = new Vector2(-halfScreenWidth, halfScreenHeight);
        bounds[2] = new Vector2(halfScreenWidth, halfScreenHeight);
        bounds[3] = new Vector2(halfScreenWidth, -halfScreenHeight);

        screenEdge.points = bounds;
    }
}
