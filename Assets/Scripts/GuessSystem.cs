using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GuessSystem : MonoBehaviour
{
    [SerializeField]
    private JudgeSystem judgeSystem;
    [SerializeField]
    private UIManager uiManager;

    private List<int[]> candidates = new List<int[]>();

    void Start()
    {
        InitCandidates();
    }

    public int[] Guess()
    {
        return candidates[0];
    }

    private void InitCandidates()
    {
        //候補リストに全通りを追加
        candidates.Clear();
        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                if (j == i) continue;
                for (int k = 1; k <= 9; k++)
                {
                    if (k == i || k == j) continue;
                    candidates.Add(new int[] { i, j, k });
                }
            }
        }

        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = candidates[i];
            candidates[i] = candidates[j];
            candidates[j] = temp;
        }
    }

    public void UpdateCandidates(int[] guess, int[] result)
    {
        List<int[]> updated = new List<int[]>();

        foreach (int[] candidate in candidates)
        {
            int[] tmp = judgeSystem.Judge(candidate, guess);
            if (tmp.SequenceEqual(result))
            {
                updated.Add(candidate);
            }
        }

        candidates = updated;
        uiManager.SetCandidateCounter(candidates.Count);
        Debug.Log("候補アップデート");
    }

    private void GetCandidatesNum()
    {
        
    }

    // private int[] GenerateRandomUniqueNumbers()
    // {
    //     List<int> numbers = new List<int>();
    //     for (int i = 1; i <= 9; i++)
    //     {
    //         numbers.Add(i);
    //     }

    //     // シャッフル
    //     for (int i = numbers.Count - 1; i > 0; i--)
    //     {
    //         int j = Random.Range(0, i + 1);
    //         int temp = numbers[i];
    //         numbers[i] = numbers[j];
    //         numbers[j] = temp;
    //     }

    //     // 最初の3つを返す
    //     return new int[] { numbers[0], numbers[1], numbers[2] };
    // }

}
