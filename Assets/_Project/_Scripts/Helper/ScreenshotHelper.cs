using UnityEngine;
using System.IO;
public static class ScreenshotHelper
{
    private static string _folderName = "Level Previews";
    public static Texture2D CaptureFromCamera(Camera camera, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        Object.Destroy(rt);

        return screenshot;
    }

    public static Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    public static void SaveTextureToFile(Texture2D texture, string fileName)
    {
        string folderPath = GetPreviewFolder();
        string filePath = Path.Combine(folderPath, fileName);
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

    }

    public static Texture2D LoadTextureFromFile(string fileName)
    {
        string folderPath = GetPreviewFolder();
        string filePath = Path.Combine(folderPath, fileName);
        if (!File.Exists(filePath)) return null;
        byte[] bytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        return texture;
    }

    public static string GetPreviewFolder()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, _folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return folderPath;
    }
}
