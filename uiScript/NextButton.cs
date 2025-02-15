using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  public void OnOK(){
        SceneManager.LoadScene("Select");
    }
    public void OnNext(){
     string currentSceneName = SceneManager.GetActiveScene().name;
            // 正規表現を使用してシーン名から数字部分を抽出
            Regex regex = new Regex(@"Stage(\d+)");
            Match match = regex.Match(currentSceneName);

            // 正規表現が一致した場合
            if (match.Success)
            {
                // 一致したグループ（数字部分）を整数に変換
                int currentStage = int.Parse(match.Groups[1].Value);

                // 次のステージ番号を計算
                int nextStage = currentStage + 1;
                string nextSceneName = "Stage" + nextStage;

                // 次のシーンをロード
                SceneManager.LoadScene(nextSceneName);
            }
    }
}
