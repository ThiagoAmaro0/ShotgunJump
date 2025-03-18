using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    public static GameManager instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Cursor.SetCursor(_cursorTexture, new Vector2(32, 32), CursorMode.ForceSoftware);
        SceneLoader.Scenes.Menu.Load();
    }
}
