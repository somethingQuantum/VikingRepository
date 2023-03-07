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
    /// Fonction appelée lorsqu'on clique sur une case du tableau de jeu
    /// </summary>
    private void OnMouseDown()
    {
        //Si la case selectionnée n'a pas encore été sélectionnée et ne contient aucune pièce
        if(!isSelected && Piece != null && board.selectedCell == null)
        {
            HighlightCell();
            ShowLegalMoves();
            board.selectedCell = gameObject;
        }
        //Si le joueur clique sur une nouvelle pièce après avoir cliquée sur une précédemment
        else if(!isSelected && Piece != null && board.selectedCell != null)
        {
            //Déselectionne les cases
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();
            ResetLegalMoves();
            //Sélectionne la nouvelle case
            board.selectedCell = gameObject;
            HighlightCell();
            ShowLegalMoves();

        }
        //Si la case cliquée est une des cases possibles à jouer dans LegalMoves
        else if (isSelected && Piece == null)
        {
            GameObject tempPiece = board.selectedCell.GetComponent<Cell>().Piece;
            //Échange de position sur le tableau de jeu
            tempPiece.transform.parent = transform;
            tempPiece.transform.localPosition = Vector3.zero;
            //Échange de pièce dans la mémoire des cases
            Piece = tempPiece;
            board.selectedCell.GetComponent<Cell>().Piece = null;
            //Déselectionne les cases
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();
            ResetLegalMoves();
            board.selectedCell = null;
        }
        //Si la case cliquée contient la pièce sélectionnée pour jouer ou contient aucune pièce et n'est pas un coup légal, déselectionne tout
        else if((isSelected && Piece != null) || (!isSelected && Piece == null))
        {
            //S'il y a une pièce de sélectionnée
            if(board.selectedCell != null)
            board.selectedCell.GetComponent<Cell>().UnhighlightCell();

            ResetLegalMoves();
            board.selectedCell = null;
        }
    }
    /// <summary>
    /// Déselectionne toute les cases des coups légaux
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
    /// Met en évidence la case
    /// </summary>
    private void HighlightCell()
    {
        GetComponent<SpriteRenderer>().color = selected;
        isSelected = true;
    }
    /// <summary>
    /// Déselectionne la case
    /// </summary>
    private void UnhighlightCell()
    {
        GetComponent<SpriteRenderer>().color = normal;
        isSelected = false;
    }
    /// <summary>
    /// Fonction regroupant tout les coups légaux pour une pièce selectionnée
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
        //Met en évidence tout les coups légaux
        foreach(GameObject cell in board.legalMoves)
        {
            cell.GetComponent<Cell>().HighlightCell();
        }
    }
}
