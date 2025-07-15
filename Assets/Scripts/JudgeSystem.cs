using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    int[] answer = new int[3];
    public int[,] results = new int[10, 3];
    [SerializeField]
    private TurnManager turnManager;

    private void Start()
    {
        answer = AnswerGenerator.GenerateAnswer();
        Debug.Log("正解:" + answer[0] + answer[1] + answer[2]);
    }

    public int[] Judge(int[] guessNums, int[] target = null)
    {
        if (target == null)
        {
            target = answer;
        }

        int hit = 0;
        int blow = 0;

        for (int i = 0; i < 3; i++)
        {
            if (guessNums[i] == target[i])
            {
                hit++;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (guessNums[i] != target[i] && target.Contains(guessNums[i]))
            {
                blow++;
            }
        }

        int none = 3 - (hit + blow);
        return new int[] { hit, blow, none };
    }

    public void SaveResult(int[] result)
    {
        results[turnManager.turnIndex,0] = result[0];
        results[turnManager.turnIndex,1] = result[1];
        results[turnManager.turnIndex,2] = result[2];
    }


  // int[] playerCorrectNums = new int[] { 0, 0, 0 };
    // int[] aiCorrectNums = new int[] { 0, 0, 0 };

    // int hit, blow, no;

    // public void SetPlayerCorrectNum(int[] nums)
    // {
    //     for (int i = 0; i < nums.Length; i++)
    //     {
    //         playerCorrectNums[i] = nums[i];
    //     }
    // }
    // public void SetAICorrectNum(int[] nums)
    // {
    //     for (int i = 0; i < nums.Length; i++)
    //     {
    //         aiCorrectNums[i] = nums[i];
    //     }
    // }

    // public (int hit, int blow) JudgePlayerNums(int[] nums)
    // {
    //     hit = 0;
    //     blow = 0;
    //     no = 0;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         if (nums[i] == aiCorrectNums[0] || nums[i] == aiCorrectNums[1] || nums[i] == aiCorrectNums[2])
    //         {
    //             if (nums[i] == aiCorrectNums[i])
    //             {
    //                 hit++;
    //             }
    //             else
    //             {
    //                 blow++;
    //             }
    //         }
    //         else
    //         {
    //             no++;
    //         }
    //     }
    //     return (hit, blow);
    // }
    // public (int hit, int blow) JudgeAINums(int[] nums)
    // {
    //     hit = 0;
    //     blow = 0;
    //     no = 0;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         if (nums[i] == playerCorrectNums[0] || nums[i] == playerCorrectNums[1] || nums[i] == playerCorrectNums[2])
    //         {
    //             if (nums[i] == playerCorrectNums[i])
    //             {
    //                 hit++;
    //             }
    //             else
    //             {
    //                 blow++;
    //             }
    //         }
    //         else
    //         {
    //             no++;
    //         }
    //     }
    //     return (hit, blow);
    // }
}