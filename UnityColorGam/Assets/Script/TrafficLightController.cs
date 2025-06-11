using UnityEngine;
using UnityEngine.UI;

public class TrafficLightController : MonoBehaviour
{
    public enum LightState { Red, Green }
    public LightState currentState = LightState.Red;

    [Header("Mini TL1 Settings")]
    public Button miniButton;            // Mini 红绿灯按钮（TL1）
    public Sprite redSprite;
    public Sprite greenSprite;

    [Header("Big Panel Settings")]
    public GameObject controlUIPrefab;   // 放大红绿灯 UI Panel 预制体
    private GameObject currentUI;

    void Start()
    {
        // 设置 miniButton 的点击事件
        miniButton.onClick.AddListener(OnMiniButtonClicked);
        UpdateMiniVisual();
    }

    void UpdateMiniVisual()
    {
        // 更换 TL1 按钮的图标
        Image img = miniButton.image;
        img.sprite = (currentState == LightState.Red) ? redSprite : greenSprite;
    }

    public void ToggleState(LightState newState)
    {
        currentState = newState;
        UpdateMiniVisual();
    }

    void OnMiniButtonClicked()
    {
        if (currentUI == null)
        {
            // 挂载到主 UI Canvas 下（请确保场景中有名为 "Canvas" 的 UI 根节点）
            Transform canvas = GameObject.Find("Canvas").transform;
            currentUI = Instantiate(controlUIPrefab, canvas);

            // 绑定按钮事件
            Button redBtn = currentUI.transform.Find("RedButton").GetComponent<Button>();
            Button greenBtn = currentUI.transform.Find("GreenButton").GetComponent<Button>();
            Button closeBtn = currentUI.transform.Find("CloseButton").GetComponent<Button>();

            redBtn.onClick.AddListener(() => ToggleState(LightState.Red));
            greenBtn.onClick.AddListener(() => ToggleState(LightState.Green));
            closeBtn.onClick.AddListener(() => Destroy(currentUI));
        }
    }
}
