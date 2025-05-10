using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    int[] playerCorrectNums = new int[] { 0, 0, 0 };
    int[] aiCorrectNums = new int[] { 0, 0, 0 };

    int hit, blow, no;

    public void SetPlayerCorrectNum(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            playerCorrectNums[i] = nums[i];
        }
    }
    public void SetAICorrectNum(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            aiCorrectNums[i] = nums[i];
        }
    }

    public (int hit, int blow) JudgePlayerNums(int[] nums)
    {
        hit = 0;
        blow = 0;
        no = 0;
        for (int i = 0; i < 3; i++)
        {
            if (nums[i] == aiCorrectNums[0] || nums[i] == aiCorrectNums[1] || nums[i] == aiCorrectNums[2])
            {
                if (nums[i] == aiCorrectNums[i])
                {
                    hit++;
                }
                else
                {
                    blow++;
                }
            }
            else
            {
                no++;
            }
        }
        return (hit, blow);
    }
    public (int hit, int blow) JudgeAINums(int[] nums)
    {
        hit = 0;
        blow = 0;
        no = 0;
        for (int i = 0; i < 3; i++)
        {
            if (nums[i] == playerCorrectNums[0] || nums[i] == playerCorrectNums[1] || nums[i] == playerCorrectNums[2])
            {
                if (nums[i] == playerCorrectNums[i])
                {
                    hit++;
                }
                else
                {
                    blow++;
                }
            }
            else
            {
                no++;
            }
        }
        return (hit, blow);
    }
}