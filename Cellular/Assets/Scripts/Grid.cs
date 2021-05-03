using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class Grid : MonoBehaviour
{
    public GameObject placeHolderGrid;
    public GameObject cellPrefab;
    public int maxSteps = 5;
    private GameObject[,] _cells;
    
    private int step = 0;
    
    public int dimension;
    private const string StringPattern = @"[Bb]([0-9]+)/[Ss]([0-9]+)";
    private static readonly Regex Pattern = new Regex(StringPattern);

    Tuple<HashSet<int>, HashSet<int>> ParseRuleSet(string rule)
    {
        #if DEBUG
        if (!Pattern.IsMatch(rule)) { Debug.LogError("Not a match"); }
        #endif

        var matches = Pattern.Match(rule);
        string birth = matches.Groups[1].Value;
        string survive = matches.Groups[2].Value;
        
        
        
        HashSet<int> birthings = new HashSet<int>();
        HashSet<int> surviving = new HashSet<int>();
        
        foreach (var c in birth)
        {
            if(!Char.IsDigit(c)) continue;
            birthings.Add(Convert.ToInt32(c) - 48);
        }
        
        foreach (var c in survive)
        {
            if(!Char.IsDigit(c)) continue;
            surviving.Add(Convert.ToInt32(c) - 48);
        }
        
        return new Tuple<HashSet<int>, HashSet<int>>(birthings,surviving);
    }

    int[,] calculateNeighbors()
    {
        int[,]  neighbors = new int[dimension,dimension];
        
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                int tot = 0;
                if (i > 0) { tot += Convert.ToUInt16(_cells[i - 1, j].GetComponent<Cell>().alive);  } // left
                if (i >  0 && j > 0) { tot += Convert.ToUInt16(_cells[i - 1, j - 1].GetComponent<Cell>().alive); } // top left
                if (j > 0) { tot += Convert.ToUInt16(_cells[i, j - 1].GetComponent<Cell>().alive); } // up
                if (j > 0 && i < dimension - 1) { tot += Convert.ToUInt16(_cells[i + 1, j - 1].GetComponent<Cell>().alive); } // up right
                if (j < dimension - 1) { tot += Convert.ToUInt16(_cells[i, j + 1].GetComponent<Cell>().alive); } // bottom
                if (i < dimension-1 && j < dimension - 1) { tot += Convert.ToUInt16(_cells[i + 1, j + 1].GetComponent<Cell>().alive); } // bottom right
                if (i < dimension-1) { tot += Convert.ToUInt16(_cells[i + 1, j].GetComponent<Cell>().alive); } // right
                if (i > 0 && j < dimension-1) { tot += Convert.ToUInt16(_cells[i - 1, j + 1].GetComponent<Cell>().alive); } // bottom left

                neighbors[i, j] = tot;

            }
        }

        return neighbors;
    }
    // Start is called before the first frame update
    public GameObject[] cellPrefabs;

    public string levelMap;

    private int getIndex(int row, int col)
    {
        return col * dimension + row;
    }

    private void SetGrid()
    {
        float edgeScale = 0.89442815f;
        
        // sprite
        RectTransform cellRectTransform = cellPrefab.GetComponent<RectTransform>();
        var scale = cellRectTransform.localScale;
        var rect1 = cellRectTransform.rect;
        float cellRealWidth = rect1.width * scale.x;
        float cellRealHeight = rect1.width * scale.y;

                // grid
        _cells = new GameObject[dimension,dimension];
        RectTransform placeHolderRectTransform = placeHolderGrid.GetComponent<RectTransform>();
        var localScale = placeHolderRectTransform.localScale;
        var rect = placeHolderRectTransform.rect;
        float holderRealWidth = rect.width * localScale.x;
        float holderRealHeight = rect.height * localScale.y;

        float rowSizeNoOver = cellRealWidth * dimension;
        float colSizeNoOver = cellRealHeight * dimension;

        float lossFactor = (1 - edgeScale) * cellRealWidth * (dimension - 1);

        float tW = rowSizeNoOver - lossFactor;
        float tH = colSizeNoOver - lossFactor;

        float rowScale = holderRealWidth / tW;
        float colScale = holderRealWidth / tH;
        
        // resize cell
        Vector2 idealSize = new Vector2(rowScale * cellRealWidth, colScale * cellRealHeight);
        float scalerX = idealSize.x / cellRealWidth;
        float scalerY = idealSize.y / cellRealHeight;

        float scaledWidth = scalerX * cellRealWidth ;
        float scaledHeight = scalerY * cellRealHeight;
        
        // setting anchor
        var position = placeHolderGrid.transform.position;
        Vector2 anchor  = new Vector2(position.x - holderRealWidth / 2 + scaledWidth / 2,position.y + holderRealHeight / 2 - scaledHeight / 2); // offsetting centered pivot
        
        
        // instantiate cells
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                // get prefab
                int idx =  Convert.ToInt32(levelMap[getIndex(i, j)]) - 48;
                GameObject prefab = cellPrefabs[idx];
                
                GameObject cell = Instantiate(prefab, new Vector2(anchor.x +  i*scaledWidth - i*(1 - edgeScale)*scaledWidth,anchor.y - j*scaledHeight + j*(1 - edgeScale)*scaledWidth), Quaternion.identity,transform);
                
                cell.transform.localScale = new Vector3(scalerX,scalerY,1);
                
                // flip x
                if (Random.Range(0, 2) > 0) { cell.GetComponent<RectTransform>().Rotate(Vector3.forward,270); }

                if (Random.Range(0, 2) > 0)
                {
                    cell.GetComponent<RectTransform>().Rotate(Vector3.forward, 90);
                }

                _cells[i, j] = cell;
            }
        }
    }

    public void Step()
    {
        if (step >= maxSteps) { return; }
        var neighbors = calculateNeighbors();

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                var c = _cells[i, j].GetComponent<Cell>();
                var cNeighbors = neighbors[i, j];
                var rules = ParseRuleSet(c.ruleSet);

                c.SetAlive(c.alive ? rules.Item2.Contains(cNeighbors) : rules.Item1.Contains(cNeighbors));
            }
        }

        step++;
    }


    public void fullWipe()
    {
        while (step < maxSteps)
        {
            Step(); // coroutined
            step++;
        }
    }

    public void Clear()
    {
        
    }
    void Start()
    {
        SetGrid();
    }
    
}
