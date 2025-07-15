using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSlot : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar; // Scrollbarをアタッチ
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private UIManager uiManager;

    float scroll_pos = 1f;
    float[] pos;

    private bool isAutoScrolling = false;
    private float targetValue = 1f;
    private int snappedIndex = -1;

    void Update()
    {
        int childCount = transform.childCount;
        if (childCount <= 1) return;

        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;

        // スナップ位置を再計算（上が1, 下が0）
        pos = new float[childCount];
        float distance = 1f / (childCount - 1);
        for (int i = 0; i < childCount; i++)
        {
            pos[i] = 1f - (distance * i); // 縦方向なので逆
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            isAutoScrolling = false;
        }
        else if (!isAutoScrolling)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2f) && scroll_pos > pos[i] - (distance / 2f))
                {
                    float lerped = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    scrollbar.GetComponent<Scrollbar>().value = lerped;

                    // スナップ済みかどうか
                    if (Mathf.Abs(lerped - pos[i]) < 0.001f && snappedIndex != i)
                    {
                        snappedIndex = i;
                        uiManager.SetResult(snappedIndex);
                    }
                }
            }
        }

        if (isAutoScrolling)
        {
            float current = scrollbar.GetComponent<Scrollbar>().value;
            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(current, targetValue, 0.1f);

            if (Mathf.Abs(current - targetValue) < 0.001f)
            {
                scrollbar.GetComponent<Scrollbar>().value = targetValue;
                isAutoScrolling = false;

                // 自動スクロール後に通知
                int index = FindNearestIndex(targetValue);
                if (snappedIndex != index)
                {
                    snappedIndex = index;
                    uiManager.SetResult(snappedIndex);
                }
            }
        }

        // 拡大縮小・透明度
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2f) && scroll_pos > pos[i] - (distance / 2f))
            {
                Transform centerChild = transform.GetChild(i);
                centerChild.localScale = Vector2.Lerp(centerChild.localScale, new Vector2(1f, 1f), 0.1f);
                var centerCG = centerChild.GetComponent<CanvasGroup>();
                if (centerCG != null) centerCG.alpha = Mathf.Lerp(centerCG.alpha, 1f, 0.1f);

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        Transform otherChild = transform.GetChild(j);
                        otherChild.localScale = Vector2.Lerp(otherChild.localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        var otherCG = otherChild.GetComponent<CanvasGroup>();
                        if (otherCG != null)
                        {
                            if (Mathf.Abs(i - j) <= 2)
                            {
                                otherCG.alpha = Mathf.Lerp(otherCG.alpha, (Mathf.Abs(i - j) == 1 ? 0.66f : 0.33f), 0.1f);
                            }
                            else
                            {
                                otherCG.alpha = Mathf.Lerp(otherCG.alpha, 0f, 0.1f);
                            }
                        }
                    }
                }
            }
        }
    }

    public void MoveScrollbar()
    {
        int childCount = transform.childCount;
        if (childCount <= 1) return;

        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);
        for (int i = 0; i < childCount; i++)
        {
            pos[i] = 1f - (distance * i); // 縦方向
        }

        int turnIndex = Mathf.Clamp(turnManager.turnIndex, 0, pos.Length - 1);
        targetValue = pos[turnIndex];
        isAutoScrolling = true;
    }

    private int FindNearestIndex(float value)
    {
        int nearest = 0;
        float minDiff = Mathf.Infinity;
        for (int i = 0; i < pos.Length; i++)
        {
            float diff = Mathf.Abs(pos[i] - value);
            if (diff < minDiff)
            {
                minDiff = diff;
                nearest = i;
            }
        }
        return nearest;
    }
}
