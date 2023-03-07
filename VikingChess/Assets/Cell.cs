using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector]public GameObject Piece = null;
    public int i;
    public int j;
    public Board board;
    private bool isSelected = false;
    public Color normal;
    public Color selected;
    /// <summary>
    /// Fonction appel�e lorsqu'on clique sur une case du tableau de jeu
    /// </summary>
    private void OnMouseDown()
    {
        //Si la case selectionn�e n'a pas encore �t� s�lectionn�e et ne contient aucune pi�ce
        if(!isSelected && Piece != null && board.selectedCell == null)
        {
            HighlightCell();
            ShowLegalMoves();
            board.selectedCell = gameObject;
        }
        //Si le joueur clique sur une nouvelle pi�ce apr�s avoir cliqu�e sur une pr�c�demment
        else if(!isSelected && Piece != null && board.selectedCell != null)
        {
            //D�selectionne les cases
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();
            ResetLegalMoves();
            //S�lectionne la nouvelle case
            board.selectedCell = gameObject;
            HighlightCell();
            ShowLegalMoves();

        }
        //Si la case cliqu�e est une des cases possibles � jouer dans LegalMoves
        else if (isSelected && Piece == null)
        {
            GameObject tempPiece = board.selectedCell.GetComponent<Cell>().Piece;
            //�change de position sur le tableau de jeu
            tempPiece.transform.parent = transform;
            tempPiece.transform.localPosition = Vector3.zero;
            //�change de pi�ce dans la m�moire des cases
            Piece = tempPiece;
            board.selectedCell.GetComponent<Cell>().Piece = null;
            //D�selectionne les cases
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();
            ResetLegalMoves();
            board.selectedCell = null;
        }
        //Si la case cliqu�e contient la pi�ce s�lectionn�e pour jouer ou contient aucune pi�ce et n'est pas un coup l�gal, d�selectionne tout
        else if((isSelected && Piece != null) || (!isSelected && Piece == null))
        {
            //S'il y a une pi�ce de s�lectionn�e
            if(board.selectedCell != null)
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();

            ResetLegalMoves();
            board.selectedCell = null;
        }
    }
    /// <summary>
    /// D�selectionne toute les cases des coups l�gaux
    /// </summary>
    private void ResetLegalMoves()
    {
        foreach(GameObject cell in board.legalMoves)
        {
            cell.GetComponent<Cell>().UnhighlightCell();
        }

        board.legalMoves.Clear();
    }
    /// <summary>
    /// Met en �vidence la case
    /// </summary>
    private void HighlightCell()
    {
        GetComponent<SpriteRenderer>().color = selected;
        isSelected = true;
    }
    /// <summary>
    /// D�selectionne la case
    /// </summary>
    private void UnhighlightCell()
    {
        GetComponent<SpriteRenderer>().color = normal;
        isSelected = false;
    }
    /// <summary>
    /// Fonction regroupant tout les coups l�gaux pour une pi�ce selectionn�e
    /// </summary>
    private void ShowLegalMoves()
    {
        //Regarde s'il existe un move legal haut
        for(int v = 1; v < i + 1; v++)
        {
            if (board.boardMatrix[(i - v), j].GetComponent<Cell>().Piece==null)
            {
                board.legalMoves.Add(board.boardMatrix[(i - v), j]);
            }
            else
            {
                v = i + 1;
            }
        }
        //Regarde s'il existe un move legal bas
        for(int v = 1; v < (11 - i); v++)
        {
            if (board.boardMatrix[(i + v), j].GetComponent<Cell>().Piece == null)
            {
                board.legalMoves.Add(board.boardMatrix[(i + v), j]);
            }
            else
            {
                v = 11 - i;
            }
        }
        //Regarde s'il existe un move legal gauche
        for (int h = 1; h < j + 1; h++)
        {
            if (board.boardMatrix[i, (j - h)].GetComponent<Cell>().Piece == null)
            {
                board.legalMoves.Add(board.boardMatrix[i, (j - h)]);
            }
            else
            {
                h = j + 1;
            }
        }
        //Regarde s'il existe un move legal droite
        for(int h = 1; h < 11 - j; h++)
        {
            if (board.boardMatrix[i, (j + h)].GetComponent<Cell>().Piece == null)
            {
                board.legalMoves.Add(board.boardMatrix[i, (j + h)]);
            }
            else
            {
                h = 11 - j;
            }
        }
        //Met en �vidence tout les coups l�gaux
        foreach(GameObject cell in board.legalMoves)
        {
            cell.GetComponent<Cell>().HighlightCell();
        }
    }
}
