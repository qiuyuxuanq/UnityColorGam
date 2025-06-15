using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum LightState { Red, Yellow, Green }

public class TrafficLightController : MonoBehaviour
{
    [Header("UI Prefab & Button")]
    public GameObject controlUIPrefab;
    public Button miniButton;

    [Header("Light Icons")]
    public Sprite redSprite;
    public Sprite yellowSprite;
    public Sprite greenSprite;

    private GameObject currentUI;
    private Dictionary<string, LightState> lightStates;

    void Start()
    {
        // 初始化灯状态，默认全部红灯
        lightStates = new Dictionary<string, LightState>
        {
            { "North", LightState.Red },
            { "South", LightState.Red },
            { "East", LightState.Red },
            { "West", LightState.Red }
        };

        if (miniButton != null)
        {
            miniButton.onClick.AddListener(OnMiniButtonClicked);
        }
        else
        {
            Debug.LogWarning("❗ MiniButton 未设置！");
        }
    }

    public bool CanGo(string direction)
    {
        if (lightStates != null && lightStates.TryGetValue(direction, out LightState state))
        {
            Debug.Log($"🟢 [CanGo] {direction} = {state}");
            return state == LightState.Green;
        }
        Debug.LogWarning($"❗ [CanGo] 未找到方向：{direction}");
        return false;
    }

    public void SetLight(string direction, LightState state)
    {
        if (lightStates == null)
        {
            Debug.LogError("❗ lightStates 未初始化！");
            return;
        }

        if (lightStates.ContainsKey(direction))
        {
            lightStates[direction] = state;
            Debug.Log($"✅ [SetLight] {direction} → {state}");
        }
        else
        {
            Debug.LogWarning($"❗ SetLight：方向 {direction} 不存在");
        }
    }

    public void OnMiniButtonClicked()
    {
        if (controlUIPrefab == null)
        {
            Debug.LogError("❗ controlUIPrefab 未设置！");
            return;
        }

        if (currentUI != null)
        {
            Destroy(currentUI);
            return;
        }

        Transform canvas = GameObject.Find("Canvas")?.transform;
        if (canvas == null)
        {
            Debug.LogError("❗ Canvas 未找到！");
            return;
        }

        currentUI = Instantiate(controlUIPrefab, canvas);
        Debug.Log("📋 控制面板已创建");

        string[] dirs = { "North", "South", "East", "West" };

        foreach (string dir in dirs)
        {
            Button red = currentUI.transform.Find($"{dir}/Red")?.GetComponent<Button>();
            Button yellow = currentUI.transform.Find($"{dir}/Yellow")?.GetComponent<Button>();
            Button green = currentUI.transform.Find($"{dir}/Green")?.GetComponent<Button>();

            if (red && yellow && green)
            {
                red.onClick.AddListener(() => SetLight(dir, LightState.Red));
                yellow.onClick.AddListener(() => SetLight(dir, LightState.Yellow));
                green.onClick.AddListener(() => SetLight(dir, LightState.Green));
            }
            else
            {
                Debug.LogWarning($"❗ 未找到 {dir} 的按钮");
            }
        }

        Button closeBtn = currentUI.transform.Find("Close")?.GetComponent<Button>();
        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(() =>
            {
                Destroy(currentUI);
                currentUI = null;
                Debug.Log("🧹 控制面板已关闭");
            });
        }
        else
        {
            Debug.LogWarning("❗ Close 未找到");
        }
    }
}
