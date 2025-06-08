using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    // ���̃J�����Ńt�H�O��L���ɂ��邩�ǂ����̃t���O
    public bool enableFogForThisCamera = false;
    // �t�H�O�̌��̏�Ԃ�ۑ����邽�߂̕ϐ�
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
        // ���݂̃t�H�O�̏�Ԃ�ۑ�
        originalFogState = RenderSettings.fog;

        // ���̃J�����p�̃t�H�O�ݒ��K�p
        RenderSettings.fog = enableFogForThisCamera;
    }

    void OnPostRender()
    {
        // ���̃t�H�O�̏�Ԃɖ߂�
        RenderSettings.fog = originalFogState;
    }
}
