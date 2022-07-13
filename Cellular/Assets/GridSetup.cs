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
        // Should input grid settings, and create corresponding Grid Tiles
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _gridContainerRectTransform = GetComponent<RectTransform>();

        float spaceToRemove = (size.x-1) * _gridSpriteOverlapPercentage * _gridContainerRectTransform.rect.width;

        float cellSizeX = (float) Math.Floor(_gridContainerRectTransform.rect.width /
                           (size.x - ((size.x - 1) * _gridSpriteOverlapPercentage)));
        float cellSizeY = (float) Math.Floor(_gridContainerRectTransform.rect.height /
                           (size.y - ((size.y - 1) * _gridSpriteOverlapPercentage)));

        float padding = -0.116193f * cellSizeX + 0.834479f; // Using linear regression

        _gridLayoutGroup.spacing = new Vector2(padding, padding);
        _gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newGridTile = Instantiate(gridTilePrefab, transform, true);
            }
        }
    }
}
