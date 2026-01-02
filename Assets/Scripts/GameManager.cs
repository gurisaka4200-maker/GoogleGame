using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform extractionZone;
    public float extractionRadius = 2.5f;
    public Transform player;
    public Text statusText;

    private bool extracted = false;

    void Update()
    {
        if (extracted) return;
        if (player != null && extractionZone != null)
        {
            float d = Vector3.Distance(player.position, extractionZone.position);
            statusText.text = d <= extractionRadius ? "Hold to Extract" : $"Extract in {Mathf.Max(0, d - extractionRadius):0.0}m";
            if (d <= extractionRadius)
            {
                Extract();
            }
        }
    }

    void Extract()
    {
        extracted = true;
        statusText.text = "Extracted! You win!";
        // trigger end-game UI, rewards, etc.
    }
}
