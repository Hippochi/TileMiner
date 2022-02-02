using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tRow, tCol, resNum;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color baseColor, lowColor, midColor, highColor, hiddenColor;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameManager GM;
    [SerializeField] private GridManager GridM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GridM = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        setHiddenColor();
    }

    void setHiddenColor()
    {
        if (resNum >= 100)
        {
            hiddenColor = highColor;
        }
        else if (resNum >= 50)
        {
            hiddenColor = midColor;
        }
        else if (resNum >= 25)
        {
            hiddenColor = lowColor;
        }
        else
        {
            hiddenColor = baseColor;
        }
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
    public void toHiddenCol()
    {
        _renderer.color = hiddenColor;
    }

    private void OnMouseDown()
    {
        if (GM.scanMode == true && GM.scans > 0)
        {
            toHiddenCol();
            int nbRow, nbCol;

            for (int r = -1; r < 2; r++)
            {
                nbRow = tRow + r;
                if (nbRow >= 0 && nbRow < 32)
                {
                    for (int c = -1; c < 2; c++)
                    {
                        nbCol = tCol + c;
                        if (nbCol >= 0 && nbCol < 32)
                        {
                            Tile nbTile = GameObject.Find($"Tile {nbRow} {nbCol}").GetComponent<Tile>();
                            nbTile.toHiddenCol();
                        }
                    }
                }

            }
            GM.scans--;
        }

        if (GM.scanMode == false && GM.extracts > 0)
        {
            GM.total += resNum;
            resNum = 0;

            int nbRow, nbCol;

            for (int r = -2; r < 3; r++)
            {
                nbRow = tRow + r;
                if (nbRow >= 0 && nbRow < 32)
                {
                    for (int c = -2; c < 3; c++)
                    {
                        nbCol = tCol + c;
                        if (nbCol >= 0 && nbCol < 32)
                        {
                            if (Mathf.Abs(r) == 2 || Mathf.Abs(c) == 2)
                            {
                                var midTile = GridM.GetTileAtPosition(new Vector2Int(nbRow, nbCol));
                                midTile.resNum -= 25;
                            }
                            else if (Mathf.Abs(r) == 1 || Mathf.Abs(c) == 1)
                            {
                                var midTile = GridM.GetTileAtPosition(new Vector2Int(nbRow, nbCol));
                                midTile.resNum -= 25;
                            }
                        }
                    }
                }

            }
            GM.extracts--;
        }
    }
}
