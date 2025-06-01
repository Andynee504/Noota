
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadDefaultKeybinds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDefaultKeybinds()
    {
        // Padrões iniciais, podem ser carregados de PlayerPrefs futuramente
        keybinds["Jump"] = KeyCode.Space;
        keybinds["Attack"] = KeyCode.Mouse0;
        keybinds["Left"] = KeyCode.A;
        keybinds["Right"] = KeyCode.D;
        keybinds["Dash"] = KeyCode.LeftShift;
        keybinds["Inventory"] = KeyCode.I;
        keybinds["Menu"] = KeyCode.Escape;
    }

    public bool GetKey(string action)
    {
        return Input.GetKey(keybinds[action]);
    }

    public bool GetKeyDown(string action)
    {
        return Input.GetKeyDown(keybinds[action]);
    }

    public bool GetKeyUp(string action)
    {
        return Input.GetKeyUp(keybinds[action]);
    }

    public void SetKey(string action, KeyCode newKey)
    {
        if (keybinds.ContainsKey(action))
            keybinds[action] = newKey;
    }

    public KeyCode GetKeyCode(string action)
    {
        return keybinds.ContainsKey(action) ? keybinds[action] : KeyCode.None;
    }
}
