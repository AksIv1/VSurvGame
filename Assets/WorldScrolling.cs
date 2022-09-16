using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

   // public int TerrainTileVerticalCount { get => terrainTileVerticalCount; set => terrainTileVerticalCount = value; }

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        PlayerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        PlayerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        if (CurrentTilePosition != PlayerTilePosition)
        {
            CurrentTilePosition = PlayerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false)
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth; pov_x++)
        {
            for(int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(PlayerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(PlayerTilePosition.y + pov_y, true);

                Debug.Log("tileToUpdate_x" + tileToUpdate_x + "tileToUpdate_y" + tileToUpdate_y);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculatTilePosition(
                    PlayerTilePosition.x + pov_x,
                    PlayerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculatTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}