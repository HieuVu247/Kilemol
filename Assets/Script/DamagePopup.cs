using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour // Đảm bảo kế thừa từ MonoBehaviour
{
    public TextMeshProUGUI damageText;
    private float moveSpeed = 1f;
    private float fadeTime = 1f;
    private ObjectPool<DamagePopup> pool;

    public void Initialize(ObjectPool<DamagePopup> poolReference)
    {
        pool = poolReference;
    }

    public static void Create(Vector3 position, float damage)
    {
        DamagePopup popup = DamagePopupPool.Instance.Get();
        popup.transform.position = position;
        popup.Setup(damage);
    }

    private void Setup(float damage)
    {
        damageText.text = damage.ToString("F0");
        gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = damageText.color;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            damageText.color = color;
            yield return null;
        }
        pool.Release(this);
        gameObject.SetActive(false);
    }
}