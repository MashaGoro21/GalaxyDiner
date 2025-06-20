using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject introCutscene;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (SaveManager.ShouldLoadGame)
        {
            SaveManager.ShouldLoadGame = false;
            SaveManager.Instance.LoadGame();
            introCutscene.SetActive(false);
        }
    }
}
