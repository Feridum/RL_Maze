using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerMovementController;

    GameManager gameManager;
    Vector2 move;
    
    void Start()
    {
        this.gameManager = GameManager.gameManager;
        this.move = this.gameManager.getTileDiff();
        GameObject instance = Instantiate(playerMovementController, transform.position, Quaternion.identity);
        instance.transform.SetParent(transform);
    }

    public void moveUp()
    {
        this.translatePlayer(Direction.UP);
    }

    public void moveDown()
    {
        this.translatePlayer(Direction.DOWN);
    }

    public void moveLeft()
    {
        this.translatePlayer(Direction.LEFT);
    }

    public void moveRight()
    {
        this.translatePlayer(Direction.RIGHT);
    }

    void translatePlayer(Direction direction)
    {
        Vector2 translate = this.getTranslation(direction);
        if (canMove(translate))
        {
            transform.Translate(translate);
        }
    }
    Vector2 getTranslation(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return new Vector2(0, -this.move.y);
            case Direction.RIGHT: return new Vector2(this.move.x, 0);
            case Direction.DOWN: return new Vector2(0, this.move.y);
            case Direction.LEFT: return new Vector2(-this.move.x, 0);
        }

        return new Vector2(0, 0);
    }

    bool canMove(Vector2 translate)
    {
        Vector2 newPosition = (Vector2)transform.position + translate;
        ContactFilter2D contactFilter = new ContactFilter2D();

        Collider2D cols = Physics2D.OverlapCircle(newPosition, (float)0.1, 1 << LayerMask.NameToLayer("Walls"));

        return !cols;
    }

    public List<Direction> getPossibleMoves()
    {
        List<Direction> possileMoves = new List<Direction>();

        foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            if (canMove(getTranslation(direction)))
            {
                possileMoves.Add(direction);
            }
        }


        return possileMoves;
    }

    public void resetPosition()
    {
        Vector3 startingPosition = gameManager.getStartingTile();
        startingPosition.z = -1;
        transform.position = startingPosition;

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D");
    }
}
