using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
  //先ほど作成したクラス
  public SerialHandler serialHandler;
  
  void Start()
  {
     //信号を受信したときに、そのメッセージの処理を行う
     serialHandler.OnDataReceived += OnDataReceived;
  }

  void Update()
  {
    
  }
  
  public void DataSend(string s)
  {
    //文字列を送信
    serialHandler.Write(s);
  }
    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[]{"\t"}, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try {
           
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}
