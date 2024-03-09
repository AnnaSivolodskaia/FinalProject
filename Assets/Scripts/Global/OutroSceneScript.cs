using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ScoreSystem;

public class OutroSceneScript : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    private void Awake()
    {
        int currentScore = ScoreSystem.GetScore();
        finalText.text = string.Format("Hey, everyone! I'm thrilled to share the fantastic news – the election results are in, and it's a win for our forest village! The majority of you cast your votes in my favor, and I couldn't be happier.\r\n\r\nI want to express my deepest gratitude for your trust and support. It means the world to me, and I promise to continue working hard to make our village the best it can be.\r\n\r\nAnd again, special thanks to you, dear traveller, for being a true friend to our little forest community. Hope to see you again for the next Woodland Quest! Safe travels!\r\n\r\nYour final score: {0}/500.\r\n\r\nPress C: finish the game!\r\n", currentScore);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StatesManager.PositiveGameProgression();
        }
    }
}
