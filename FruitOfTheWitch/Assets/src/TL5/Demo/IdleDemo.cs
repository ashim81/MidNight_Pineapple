using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class IdleDemo : MonoBehaviour
{
    public float idleTime = 10f;
    public VideoPlayer player;
    public VideoClip[] videos;
    public GameObject menuCanvas;

    float timer;
    bool started;

    void Update()
    {
        bool hasInput =
            (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null && (
                Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame ||
                Mouse.current.middleButton.wasPressedThisFrame ||
                Mouse.current.delta.ReadValue() != Vector2.zero
            ));

        if (hasInput)
        {
            timer = 0f;

            if (started)
            {
                player.Stop();
                Time.timeScale = 1f;
                started = false;

                if (menuCanvas != null)
                    menuCanvas.SetActive(true);
            }

            return;
        }

        if (started) return;

        timer += Time.unscaledDeltaTime;

        if (timer >= idleTime)
        {
            player.clip = videos[Random.Range(0, videos.Length)];

            if (menuCanvas != null)
                menuCanvas.SetActive(false);

            Time.timeScale = 0f;
            player.Play();
            started = true;
        }
    }
}