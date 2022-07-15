using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridSetup : MonoBehaviour
{
    public Vector2Int size;
    
    [Tooltip("Prefab of grid tile, it contains the grid sprite as well as the cell and its functionality.")]
    public GameObject gridTilePrefab;
    
    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _gridContainerRectTransform;

    private const float _gridSpriteOverlapPercentage = 0.10557185f;

    private void Start()
    {   
        GenerateAndSetGridLayoutSettings();
        InstantiateGridTiles();
    }

    /// <summary>
    /// Generates the appropriate grid layout cell size and spacing given the dimensions of the board. Then sets them.
    /// </summary>
    private void GenerateAndSetGridLayoutSettings()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _gridContainerRectTransform = GetComponent<RectTransform>();

        float cellSizeX = (float) Math.Floor(_gridContainerRectTransform.rect.width /
                                             (size.x - ((size.x - 1) * _gridSpriteOverlapPercentage)));
        float cellSizeY = (float) Math.Floor(_gridContainerRectTransform.rect.height /
                                             (size.y - ((size.y - 1) * _gridSpriteOverlapPercentage)));

        float padding = -0.116193f * cellSizeX + 0.834479f; // Using linear regression

        _gridLayoutGroup.spacing = new Vector2(padding, padding);
        _gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
    }
    
    /// <summary>
    /// Instantiates all the Grid Tiles necessary to fill the grid layout.
    /// </summary>
    private void InstantiateGridTiles()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newGridTile = Instantiate(gridTilePrefab, transform, true);
            }
        }
    }
}
