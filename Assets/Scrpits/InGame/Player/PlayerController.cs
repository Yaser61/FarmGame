using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask clickLayer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 cor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D obj = Physics2D.OverlapCircle(cor, 0.5f, clickLayer);
            if (obj == null) return;

            if (obj.TryGetComponent(out IA_Click click))
            {
                click.Click();
            }
        }
    }
}
