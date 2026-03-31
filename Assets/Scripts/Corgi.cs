using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corgi : MonoBehaviour
{
    public Sprite SoberSprite;
    public Sprite DrunkSprite;
    public UI UI;
    
    private SpriteRenderer spriteRenderer;
    private bool isDrunk = false;
    private bool isPlastered = false;
    private Coroutine soberUpCoroutine;
    private int randomMoveCounter = 0;
    private int lastRandomDirection = 0;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (isPlastered)
        {
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        int direction = lastRandomDirection;
        if (randomMoveCounter == 0)
        {
            direction = Random.Range(0,4);
            lastRandomDirection = direction;
            randomMoveCounter = Random.Range(20,60);
        }
        switch  (direction)
        {
            case 0:
                Move(new Vector2(1,0));
                break;
            case 1:
                Move(new Vector2(-1, 0));
                break;
            case 2:
                Move(new Vector2(0, 1));
                break;
            case 3:
                Move(new Vector2(0, -1));
                break;
        }
        randomMoveCounter--;
    }

    public void MoveManually(Vector2 direction)
    {
        if (!isPlastered)
        {
            Move(direction);
        }
    }

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
            ScoreKeeper.AddPoint();
            UI.SetScoreText(ScoreKeeper.GetScore());
            // print("Score: " + ScoreKeeper.GetScore());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Pill"))
        {
            SoberUp();
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Moonshine"))
        {
            Destroy(other.gameObject);
            GetPlastered();
        }
    }

    private void GetPlastered()
    {
        isPlastered = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
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
        isPlastered = false;
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
