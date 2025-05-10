//旧予想コード問題あり！！！

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class guesssystem : MonoBehaviour
//{
//    [SerializeField]
//    private judgesystem judgeSystem;

//    //可能性のある数を格納する動的配列
//    List<List<int>> digit = new List<List<int>>();
//    List<int> nums = new List<int>();

//    //同時存在の可能性
//    List<List<int>> selectNum = new List<List<int>>();
//    List<int> avaivalNums = new List<int>();

//    //予想中使用していない数を格納する動的配列
//    List<int> notUsingNums = new List<int>();

//    //現在予想の数を格納する静的配列
//    int[] guessNums = { 0, 0, 0 };

//    //数評価
//    int[] numsScore = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

//    private void Start()
//    {
//        //初期値
//        for (int i = 0; i < 10; i++)
//        {
//            nums.Add(i);
//            avaivalNums.Add(i);
//            notUsingNums.Add(i);
//        }
//        //省略不可
//        for (int i = 0; i < 10; i++)
//        {
//            selectNum.Add(avaivalNums);
//        }
//        for (int i = 0; i < 3; i++)
//        {
//            digit.Add(nums);
//        }

//    }

//    private void Update()
//    {
//        if (Input.GetKeyUp(KeyCode.Return))
//        {
//            GuessNum();
//        }
//    }



//    private void GuessNum()
//    {
//        //数評価に基づいた予想
//        for (int i = 0; i < 3; i++)
//        {
//            for (int j = 0; j < digit[i].Count; j++)
//            {
//                if (numsScore[digit[i][j]] > numsScore[guessNums[i]] && notUsingNums.Contains(j) && selectNum[guessNums[0]].Contains(j) && selectNum[guessNums[1]].Contains(j))
//                {
//                    guessNums[i] = j;
//                }
//            }
//            notUsingNums.Remove(guessNums[i]);
//        }

//        Debug.Log($"{guessNums[0]}{guessNums[1]}{guessNums[2]}");
//        Debug.Log($"{judgeSystem.JudegeSystem(guessNums[0], guessNums[1], guessNums[2])}");
//        Debug.Log($"{selectNum[8].Count}");

//        switch (judgeSystem.JudegeSystem(guessNums[0], guessNums[1], guessNums[2]))
//        {
//            //ヒット単体処理
//            case "10":
//                digit[0].RemoveAll(num => num == guessNums[1] && num == guessNums[2]);
//                digit[1].RemoveAll(num => num == guessNums[0] && num == guessNums[2]);
//                digit[2].RemoveAll(num => num == guessNums[0] && num == guessNums[1]);

//                selectNum[guessNums[0]].RemoveAll(num => num == guessNums[1] || num == guessNums[2]);
//                selectNum[guessNums[1]].RemoveAll(num => num == guessNums[0] || num == guessNums[2]);
//                selectNum[guessNums[2]].RemoveAll(num => num == guessNums[0] || num == guessNums[1]);
//                break;
//            case "20":
//                digit[0].RemoveAll(num => num == guessNums[1] && num == guessNums[2]);
//                digit[1].RemoveAll(num => num == guessNums[0] && num == guessNums[2]);
//                digit[2].RemoveAll(num => num == guessNums[0] && num == guessNums[1]);
//                break;
//            //ブロー単体処理
//            case "01":
//                digit[0].RemoveAll(num => num == guessNums[0]);
//                digit[1].RemoveAll(num => num == guessNums[1]);
//                digit[2].RemoveAll(num => num == guessNums[2]);

//                selectNum[guessNums[0]].RemoveAll(num => num == guessNums[1] || num == guessNums[2]);
//                selectNum[guessNums[1]].RemoveAll(num => num == guessNums[0] || num == guessNums[2]);
//                selectNum[guessNums[2]].RemoveAll(num => num == guessNums[0] || num == guessNums[1]);
//                break;
//            case "02":
//                digit[0].RemoveAll(num => num == guessNums[0]);
//                digit[1].RemoveAll(num => num == guessNums[1]);
//                digit[2].RemoveAll(num => num == guessNums[2]);
//                break;
//            case "03":
//                digit[0].RemoveAll(num => num != guessNums[1] && num != guessNums[2]);
//                digit[1].RemoveAll(num => num != guessNums[0] && num != guessNums[2]);
//                digit[2].RemoveAll(num => num != guessNums[0] && num != guessNums[1]);
//                break;
//            //混合処理
//            default:
//                break;

//        }
//        for (int i = 0; i < 10; i++)
//        {
//            selectNum.Add(selectNum[i]);
//        }
//        for (int i = 0; i < 3; i++)
//        {
//            digit.Add(digit[i]);
//            notUsingNums.Add(guessNums[i]);
//            guessNums[i] = 0;
//        }
//    }
//}
