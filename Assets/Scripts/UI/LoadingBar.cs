
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

    public static LoadingBar Instance;

    [SerializeField] Image imageComp;

    private float loadTime;
    private float startTime;
    private bool isLoading;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameObject.SetActive(false);
        isLoading = false;
    }

    public void StartLoading(float time)
    {
        loadTime = time;
        startTime = Time.time;
        isLoading = true;
        imageComp.fillAmount = 0.0f;
        gameObject.SetActive(true);
    }

	void Update () {
        if (!isLoading) return;

        float elapsed = Time.time - startTime;
        imageComp.fillAmount = Mathf.Clamp01(elapsed / loadTime);

        if (elapsed >= loadTime)
        {
            isLoading = false;
            gameObject.SetActive(false);
        }
    }
}
