using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyMemo : MonoBehaviour, ILevelLoadField {
    [SerializeField] TMP_Text memoText;

    TSVStreamer streamedMemoData;

    private void Awake() { streamedMemoData = new TSVStreamer(); }
    void ILevelLoadField.OnLevelLoad(LevelValues level){
        foreach (TSVStreamer.MemoData memo in streamedMemoData.Memos) {
            if (memo.Day == level.Day)
                memoText.text = memo.Memo;
        }
    }

}
