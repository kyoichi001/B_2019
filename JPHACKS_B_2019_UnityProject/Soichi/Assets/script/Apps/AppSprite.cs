using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppSprite : MonoBehaviour,IAppElement
{
    SpriteRenderer m_renderer;
    SpriteRenderer Renderer
    {
        get{
            if(m_renderer==null)m_renderer = GetComponent<SpriteRenderer>();
            return m_renderer;
        }
    }
    Color m_defaultColor;
    void Start()
    {
        m_defaultColor = Renderer.material.GetColor("_Color");
        var clickable = GetComponent<Clickable3D>();
        if (clickable == null)
        {
            Debug.LogError("Clickable not found", gameObject);
            return;
        }
        clickable.OnMouseOverIn.AddListener(OnMouseOverIn);
        clickable.OnMouseOverExit.AddListener(OnMouseOverExit);
        var collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(Renderer.bounds.size.x, Renderer.bounds.size.y, 1);
    }
    byte[] ReadPngFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader bin = new BinaryReader(fileStream);
        byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

        bin.Close();

        return values;
    }
    public void Set(string path)
    {
        string extention = Path.GetExtension(path);
        if (!CanGenerate(extention))
        {
            Debug.LogError("SpriteGenerator : file not supported");
            return;
        }
        Debug.Log($"SpriteGenerator : load {path}");


        byte[] readBinary = ReadPngFile(path);
        int pos = 16; // 16バイトから開始

        int width = 0;
        for (int i = 0; i < 4; i++)
        {
            width = width * 256 + readBinary[pos++];
        }

        int height = 0;
        for (int i = 0; i < 4; i++)
        {
            height = height * 256 + readBinary[pos++];
        }

        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(readBinary);
        Renderer.sprite=Sprite.Create(texture, new Rect(0f, 0f, width, height), new Vector2(0.5f, 0.5f), 100f);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    public void SwitchMute()
    {
        var flag = gameObject.activeSelf;
        gameObject.SetActive(!flag);
    }

    public void OnMouseClicked()
    {

    }

    public void OnMouseOverExit()
    {
        Renderer.material.SetColor("_Color", m_defaultColor);
    }

    public void OnMouseOverIn()
    {
        Renderer.material.SetColor("_Color", m_defaultColor * new Color(0.5f, 0.5f, 0.5f, 1.0f));
    }
    public static bool CanGenerate(string extension)
    {
        var ex = extension.ToLower();
        if (ex == ".png") return true;
        return false;
    }
}
