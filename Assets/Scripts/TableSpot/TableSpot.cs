using TMPro;
using UnityEngine;

public class TableSpot : MonoBehaviour
{
    public bool isOccupied;

    [SerializeField] GameObject payingMoneyPrefab;
    [SerializeField] GameObject payingCrystalPrefab;

    private TextMeshPro payingMoneyText;

    private void Awake()
    {
        payingMoneyText = payingMoneyPrefab.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        payingMoneyPrefab.SetActive(false);
        payingCrystalPrefab.SetActive(false);

        payingMoneyPrefab.transform.rotation = Quaternion.Euler(payingMoneyPrefab.transform.rotation.x,
            payingMoneyPrefab.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y,
            payingMoneyPrefab.transform.rotation.z);

        payingCrystalPrefab.transform.rotation = Quaternion.Euler(payingCrystalPrefab.transform.rotation.x,
            payingCrystalPrefab.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y,
            payingCrystalPrefab.transform.rotation.z);
    }

    public void Reserve()
    {
        isOccupied = true;
    }

    public void Free()
    {
        isOccupied = false;
    }

    public void PlayPayingMoneyAnimation(float amount)
    {
        payingMoneyText.text = "+" + amount + "$";
        payingMoneyPrefab.SetActive(true);
    }

    public void PlayPayingCrystalAnimation()
    {
        payingCrystalPrefab.SetActive(true);
    }
}
