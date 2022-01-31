using UnityEngine;

public class DNA : MonoBehaviour
{
    // gene for color
    public float r;
    public float g;
    public float b;

    // gene for size
    // public float size;

    // person state
    bool dead = false;

    // analytics
    public float timeOfDeath = 0;

    // references
    SpriteRenderer sRenderer;
    Collider2D sCollider;

    private void OnMouseDown()
    {
        dead = true;
        timeOfDeath = PopulationManager.elapsed;
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
        // transform.localScale = new Vector3(size, size, size);
    }
}
