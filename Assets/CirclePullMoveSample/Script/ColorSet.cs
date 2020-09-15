using UnityEngine;
using UnityEngine.UI;

public class ColorSet : MonoBehaviour {
    void Start() {
        Color[] ColorArray = { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow, Color.white, new Color(1.0f, 0.5f, 0.0f, 1.0f) };
        var circleParent = GameObject.Find("CircleParent").transform;
        ColorArray = Shuffle(ColorArray, ColorArray.Length);

        // 円
        int i = 0;
        foreach (Transform childTransform in circleParent) {
            childTransform.GetComponent<Image>().color = ColorArray[i];
            i = (i) % ColorArray.Length + 1;
        }
    }

    // シャッフル
    Color[] Shuffle(Color[] color, int maxIndex) {
        while (maxIndex > 1) {
            maxIndex--;
            int randIndex = UnityEngine.Random.Range(0, maxIndex + 1);

            // Swap
            Color temp = color[randIndex];
            color[randIndex] = color[maxIndex];
            color[maxIndex] = temp;
        }
        return color;
    }

}
