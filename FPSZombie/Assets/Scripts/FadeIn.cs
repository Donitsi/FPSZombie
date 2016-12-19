using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    void Start () {
        StartCoroutine(FadeAnimation());
    }

    IEnumerator FadeAnimation() {
        Image fadeInImage = GetComponent<Image>();
        Color fadeColor;

        while (true) {
            fadeColor = fadeInImage.color;
            fadeColor.a -= 0.01f;
            fadeInImage.color = fadeColor;

            if (fadeColor.a > 0f) yield return new WaitForSeconds(0.1f);
            else break;
        }
    }
}
