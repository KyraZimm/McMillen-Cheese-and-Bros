using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TSVStreamer : MonoBehaviour {
    const string TSV_PATH = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSCDyUuqxxjXo2ngMZY2tdIQVg1IdQVvB1R37a73sj9iH7K1aEb0RHDE7xmmsTx8VbHLdOVO6-r9g0b/pub?gid=0&single=true&output=tsv";

    private List<MemoData> lastUploadedData;
    [SerializeField] public List<MemoData> Memos {
        get {
            if (lastUploadedData == null || lastUploadedData.Count == 0)
                TryUpdateSheetData();
            return lastUploadedData;
        }
    }

    [System.Serializable] public struct MemoData {
        public int Day { get; private set; }
        public string Memo { get; private set; }

        public MemoData(int day, string memo) {
            this.Day = day;
            this.Memo = memo;
        }
    }


    private void TryUpdateSheetData() { StartCoroutine(ReadOnlineSheetData()); }
    private IEnumerator ReadOnlineSheetData() {
        UnityWebRequest request = UnityWebRequest.Get(TSV_PATH);
        yield return request.SendWebRequest();

        //if internet is down, do not update sheet - retain current sheet data
        if (request.result == UnityWebRequest.Result.ConnectionError)
            yield return null;
        
        //if we reach this point in the function, we should have gotten some sheet data from online
        //clear last uploaded data to fill with new sheet values
        if (lastUploadedData == null)
            lastUploadedData = new List<MemoData>();
        else
            lastUploadedData.Clear();

        //in a TSV file (Tab Separated File), the spreadsheet we have gets turned into one giant string of text, where each row is a new line, and each column is separated by a tab
        //so if we split our file text up by line breaks and tab breaks, we can get individual items on the spreadsheet

        string[] rows = request.downloadHandler.text.Split('\n'); // '\n' means "line break." We're turning each row in the file into its own string in this array
        for (int i = 0; i < rows.Length; i++) {
            string[] itemsInRow = rows[i].Split('\t'); // '\t' means "tab". All the columns in our file are separated by tabs, so this should split each row into separate items for us to grab
            MemoData data = new MemoData(int.Parse(itemsInRow[0]), itemsInRow[2]); //parsedLine[0] should be the 1st item in the row (the day), and parsedLine[2] should be the 3rd item in the row (the memo text)
            lastUploadedData.Add(data);
        }
    }
}
