using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerGenerator
{
    public static int[] GenerateAnswer()
    {
        List<int> numbers = new List<int> {1,2,3,4,5,6,7,8,9};
        int[] result = new int[3];

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, numbers.Count);
            result[i] = numbers[rand];
            numbers.RemoveAt(rand);
        }

        return result;
    } 
}
