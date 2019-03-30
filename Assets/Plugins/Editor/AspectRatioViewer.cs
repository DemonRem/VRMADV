using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AspectRatioViewer : EditorWindow
{
    [MenuItem("Tools/AspectRatio Viewer")]
    static void Open()
    {
        GetWindow<AspectRatioViewer>(); // ウィンドウを開く
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("画面解像度を表示します");

        var sizes = UnityStats.screenRes.Split('x');
        var w = float.Parse(sizes[0]); // 横のサイズをもとめる
        var h = float.Parse(sizes[1]); // 縦のサイズをもとめる

        this.ShowValue("Width", w.ToString());
        this.ShowValue("Height", h.ToString());
        this.ShowValue("Aspect Ratio", string.Format("{0} : {1}", 1f, h / w));
    }

    void Update()
    {
        this.Repaint(); // ウィンドウにフォーカスが乗っていないときでも画面描画を更新
    }

    void ShowValue(string label, string value)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField(label);
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField(value.ToString());
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}