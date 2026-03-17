using System.Collections;
using UnityEngine;

public class Corgi : MonoBehaviour
{
    public Sprite SoberSprite;
    public Sprite DrunkSprite;
    
    private SpriteRenderer spriteRenderer;
    private bool isDrunk = false;
    private Coroutine soberUpCoroutine;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Move(Vector2 direction)
    {
        direction = ApplyDunkeness(direction);
        FaceCorrectDirection(direction);
        Vector2 movementAmount = direction * (GameParameters.CorgiMoveSpeed * Time.deltaTime);
        spriteRenderer.transform.Translate(movementAmount.x, movementAmount.y, 0);
        spriteRenderer.transform.position = SpriteTools.ConstrainToScreen(spriteRenderer);
    }

    private Vector2 ApplyDunkeness(Vector2 direction)
    {
        if (isDrunk)
        {
            direction.x = direction.x * -1;
            direction.y = direction.y * -1;
        }
        return direction;
    }

    private void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public Vector3 GetPosition()
    {
        return spriteRenderer.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Beer"))
        {
            GetDrunk();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bone"))
        {
            print("collided with bone");
        }
        else if (other.CompareTag("Pill"))
        {
            print("collided with pill");
        }
    }

    private void GetDrunk()
    {
        isDrunk = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
    }

    private void StartSoberingUp()
    {
        if (soberUpCoroutine != null)
        {
            StopCoroutine(soberUpCoroutine);
        }
        soberUpCoroutine = StartCoroutine(CountdownUntilSober());
    }

    IEnumerator CountdownUntilSober()
    {
        yield return new WaitForSeconds(GameParameters.CorgiDrunkSeconds);
        SoberUp();
    }

    private void SoberUp()
    {
        ChangeToSoberSprite();
        isDrunk = false;
    }

    private void ChangeToSoberSprite()
    {
        spriteRenderer.sprite = SoberSprite;
    }

    private void ChangeToDrunkSprite()
    {
        spriteRenderer.sprite = DrunkSprite;
    }
}
