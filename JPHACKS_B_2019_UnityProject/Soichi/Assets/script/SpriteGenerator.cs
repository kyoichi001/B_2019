using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using B83.Win32;
using System.Linq;

public class SpriteGenerator : SingletonMonoBehaviour<SpriteGenerator>
{
    [SerializeField] AppSprite prefab = default;

    readonly string[] extentions = { ".png" };
    void OnEnable()
    {
        FileDragAndDrop.Instance.AddOnFiles(OnFiles, extentions);
    }

    void OnFiles(string file, POINT aPos)
    {
        GenerateFromPath(file);
    }



    //https://qiita.com/r-ngtm/items/6cff25643a1a6ba82a6c
    public void GenerateFromPath(string path)
    {
        var obj = Instantiate(prefab);
        obj.Set(path);
        AppManager.Instance.AddApp(obj.gameObject);
    }

    public static bool CanGenerate(string extension)
    {
        var ex = extension.ToLower();
        if (ex == ".png") return true;
        return false;
    }

    public void Generate()
    {
        var obj = Instantiate(prefab);
        AppManager.Instance.AddApp(obj.gameObject);
    }



}
