using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AIGuessSystem : MonoBehaviour
{

    [SerializeField]
    private JudgeSystem judgeSystem;

    [SerializeField]
    private Transform AIGuessResultTextParent;
    private TextMeshProUGUI[] aiGuessResultTexts;

    [SerializeField]
    private Transform AIGuessNumTextParent;
    private TextMeshProUGUI[] aiGuessNumTexts;

    private List<int[]> candidates = new List<int[]>();
    private int[] currentGuess;

    void Start()
    {
        aiGuessResultTexts = AIGuessResultTextParent.GetComponentsInChildren<TextMeshProUGUI>();
        aiGuessNumTexts = AIGuessNumTextParent.GetComponentsInChildren<TextMeshProUGUI>();
        judgeSystem.SetAICorrectNum(GenerateRandomUniqueNumbers());
        InitCandidates();
    }

    public void Guess(int turnIndex)
    {
        //最優先候補
        currentGuess = candidates[0];

        aiGuessNumTexts[turnIndex - 1].text = $"{currentGuess[0]}{currentGuess[1]}{currentGuess[2]}";
        (int hit, int blow) = judgeSystem.JudgeAINums(currentGuess);
        aiGuessResultTexts[turnIndex - 1].text = $"{hit}-{blow}";
        UpdateCandidates(currentGuess, hit, blow);
    }

    private void InitCandidates()
    {
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

    private void UpdateCandidates(int[] guess, int hit, int blow)
    {
        candidates = candidates.Where(candidate =>
        {
            int h, b;
            GetHitAndBlow(guess, candidate, out h, out b);
            return h == hit && b == blow;
        }).ToList();
    }

    private void GetHitAndBlow(int[] guess, int[] answer, out int hit, out int blow)
    {
        hit = 0;
        blow = 0;

        for (int i = 0; i < 3; i++)
        {
            if (guess[i] == answer[i])
            {
                hit++;
            }
            else if (answer.Contains(guess[i]))
            {
                blow++;
            }
        }
    }

    private int[] GenerateRandomUniqueNumbers()
    {
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 9; i++)
        {
            numbers.Add(i);
        }

        // シャッフル
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // 最初の3つを返す
        return new int[] { numbers[0], numbers[1], numbers[2] };
    }

}
