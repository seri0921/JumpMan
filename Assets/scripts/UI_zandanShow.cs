using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_zandanShow : MonoBehaviour
{
    public GameObject textPrefab_WorldSpace;
    public Transform uiCanvas;
    void Start()
    {
        // (1) Prefabを生成
        GameObject textInstance = Instantiate(textPrefab_WorldSpace);

        // (2) 自分（Player）の子オブジェクトにする
        textInstance.transform.SetParent(uiCanvas);

        UI_zandan zandanScript = textInstance.GetComponent<UI_zandan>();
        if (zandanScript != null)
        {
            // zandan.cs が持っている 'target' 変数に、
            // このスクリプトがアタッチされているオブジェクト(自機)の Transform を渡す
            zandanScript.target = this.transform;
        }
        else
        {
            Debug.LogError("Prefabに zandan.cs がアタッチされていません！");
        }

    }
}
