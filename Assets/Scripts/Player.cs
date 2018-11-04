using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerMovementController;

    GameManager gameManager;
    Vector2 move;
    bool isFinished = false;
    bool isStarted = false;
    Translation translation;
    void Start()
    {
        this.gameManager = GameManager.gameManager;
        translation = gameManager.getTranslationForGrid("main");
        this.move = translation.getTileDiff();
        GameObject instance = Instantiate(playerMovementController, transform.position, Quaternion.identity);
        instance.transform.SetParent(transform);
    }

    public bool moveUp()
    {
        return this.translatePlayer(Direction.UP);
    }

    public bool moveDown()
    {
        return this.translatePlayer(Direction.DOWN);
    }

    public bool moveLeft()
    {
        return this.translatePlayer(Direction.LEFT);
    }

    public bool moveRight()
    {
        return this.translatePlayer(Direction.RIGHT);
    }

    bool translatePlayer(Direction direction)
    {
        Vector2 translate = this.getTranslation(direction);
        if (canMove(translate))
        {
            transform.Translate(translate);
            return true;
        }
        return false;
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

        Collider2D wall = Physics2D.OverlapCircle(newPosition, (float)0.1, 1 << LayerMask.NameToLayer("Walls"));
        Collider2D floor = Physics2D.OverlapCircle(newPosition, (float)0.1, 1 << LayerMask.NameToLayer("Floor"));

        return !wall && floor;
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

    public PlayerSurroundings getSurroundings()
    {
        PlayerSurroundings playerSurroundings = new PlayerSurroundings();

        foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            Collider2D finish = Physics2D.OverlapCircle((Vector2)transform.position + getTranslation(direction), (float)0.1, 1 << LayerMask.NameToLayer("Finish"));
            if (finish)
            {
                playerSurroundings.finish = direction;
            }
            else if (!canMove(getTranslation(direction)))
            {
                playerSurroundings.walls.Add(direction);
            }
            else
            {
                playerSurroundings.empty.Add(direction);
            }

        }

        return playerSurroundings;
    }

    public void placeOnRandomPosition(bool removeField)
    {
        this.placeOnPosition(gameManager.getRandomPosition(removeField));

    }

    public void placeOnPosition(Vector3 position)
    {
        Vector3 startingPosition = position;
        startingPosition.z = -1;
        transform.position = startingPosition;
        gameManager.setPlayerPositionFromVector(startingPosition);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Finish")
        {
            isFinished = true;
            isStarted = false;
        }
    }

    public bool isMoveFinished()
    {
        return isFinished;
    }

    public void startMove()
    {
        isFinished = false;
        isStarted = true;
    }
}
