using UnityEngine;
using UnityEngine.UI;

public class ColorSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color[] ColorArray = { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow, Color.white, new Color(1.0f, 0.5f, 0.0f, 1.0f) };
        var　circleParent = GameObject.Find("CircleParent").transform;
        ColorArray = Shuffle(ColorArray, ColorArray.Length);

        // 上部プレイヤーテキスト情報
        int i = 0;
        foreach (Transform childTransform in circleParent) {

            childTransform.GetComponent<Image>().color = ColorArray[i];
            i++;
        }
    }

    // シャッフル
    Color[] Shuffle(Color[] color, int maxIndex) // デッキをシャッフルする
    {
        // nが1より小さくなるまで繰り返す
        while (maxIndex > 1) {
            maxIndex--;
            int randIndex = UnityEngine.Random.Range(0, maxIndex + 1);

            // k番目のカードをtempに代入
            Color temp = color[randIndex];
            color[randIndex] = color[maxIndex];
            color[maxIndex] = temp;
        }
        return color;
    }

}
