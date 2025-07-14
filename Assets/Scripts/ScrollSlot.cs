using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSlot : MonoBehaviour
{
    public GameObject scrollbar; // Scrollbar をアタッチ
    float scroll_pos = 0;
    float[] pos;

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1);

        for (int i = 0; i < pos.Length; i++)
        {
        pos[i] = distance * i;
        }
        
        if (Input.GetMouseButton(0))
        {
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }else{
        for(int i = 0; i < pos.Length; i++){
            if(scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)){
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
        for(int i = 0; i < pos.Length; i++){
            if(scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)){
                Transform centerChild = transform.GetChild(i);
                centerChild.localScale = Vector2.Lerp(centerChild.localScale, new Vector2(1f, 1f), 0.1f);
                
                CanvasGroup centerCG = centerChild.GetComponent<CanvasGroup>();
                if(centerCG != null){
                    centerCG.alpha = Mathf.Lerp(centerCG.alpha, 1f, 0.1f);
                }

                for(int j = 0; j < pos.Length; j++){
                    if(j != i){
                        Transform otherChild = transform.GetChild(j);
                        otherChild.localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        CanvasGroup otherCG = otherChild.GetComponent<CanvasGroup>();
                        if(otherCG != null){
                            if(i-2<=j && j<=i+2){
                                if(i-1<=j && j<=i+1){
                                    otherCG.alpha = Mathf.Lerp(otherCG.alpha, 0.66f, 0.1f);
                                }else{
                                    otherCG.alpha = Mathf.Lerp(otherCG.alpha, 0.33f, 0.1f);
                                }
                            }else{
                                otherCG.alpha = Mathf.Lerp(otherCG.alpha, 0f, 0.1f);
                            }
                        }
                    }
                }
            }
        }
    }
}
