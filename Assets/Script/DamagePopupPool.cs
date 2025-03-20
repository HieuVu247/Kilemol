using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    public static DamagePopupPool Instance { get; private set; }
    public GameObject popupPrefab; // Prefab của DamagePopup
    private ObjectPool<DamagePopup> pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pool = new ObjectPool<DamagePopup>(() =>
        {
            GameObject obj = Instantiate(popupPrefab);
            DamagePopup popup = obj.GetComponent<DamagePopup>();
            popup.Initialize(pool);
            return popup;
        }, 5); // Tạo pool với 20 phần tử ban đầu
    }

    public DamagePopup Get() => pool.Get();
    public void Release(DamagePopup popup) => pool.Release(popup);
}