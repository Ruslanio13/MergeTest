using System.Collections;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] private TextMesh _healthText;
    [SerializeField] private int _health;
    public int GetHealth() => _health;

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText() => _healthText.text = _health.ToString();

    public void UpgradeHumanoid(int amount)
    {
        _health *= amount;
        UpdateText();
        float finalScale = gameObject.transform.localScale.x * (1f + amount * 0.05f);
        StartCoroutine(SmoothGrowing(amount, finalScale));
    }

    private void OnMouseEnter() => GameManager.Instance.AddHumanoid(this);

    IEnumerator SmoothGrowing(int amount, float finalScale)
    {
        while (finalScale > gameObject.transform.localScale.x)
        {
            gameObject.transform.localScale += Vector3.one * amount * 0.1f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        gameObject.transform.localScale = new Vector3(finalScale, finalScale, finalScale);
    }
}