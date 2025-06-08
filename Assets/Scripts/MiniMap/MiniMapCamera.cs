using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    // このカメラでフォグを有効にするかどうかのフラグ
    public bool enableFogForThisCamera = false;
    // フォグの元の状態を保存するための変数
    private bool originalFogState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPreRender()
    {
        // 現在のフォグの状態を保存
        originalFogState = RenderSettings.fog;

        // このカメラ用のフォグ設定を適用
        RenderSettings.fog = enableFogForThisCamera;
    }

    void OnPostRender()
    {
        // 元のフォグの状態に戻す
        RenderSettings.fog = originalFogState;
    }
}
