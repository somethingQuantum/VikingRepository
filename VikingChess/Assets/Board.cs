using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[,] boardMatrix = new GameObject[11,11];
    public GameObject selectedCell;
    public ArrayList legalMoves = new ArrayList();
    void Start()
    {
        //Boucle pour générer les cases du tableau de jeu ainsi que leur position en (i,j) dans la matrice. La case (0,0) se trouve ne haut à gauche du tableau
        for(int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 11; j++)
            {
                //S'il s'agit de la case centrale
                if (i == 5 && j == 5)
                {
                   boardMatrix[i, j] = Instantiate(Resources.Load<GameObject>("KingCell"), transform);
                }
                //S'il s'agit d'une case corner
                else if((i == 0 && j == 0) || (i==0 && j==10) || (i==10 && j==0) || (i==10 && j==10))
                {
                    boardMatrix[i, j] = Instantiate(Resources.Load<GameObject>("CornerCell"), transform);
                }
                else
                {
                    boardMatrix[i, j] = Instantiate(Resources.Load<GameObject>("Cell"), transform);
                }

                boardMatrix[i, j].GetComponent<Cell>().i = i;
                boardMatrix[i, j].GetComponent<Cell>().j = j;
            }
        }
        //Assignation de position des black pieces (attaquant)
        boardMatrix[0,3].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[0,3].transform);
        boardMatrix[0, 4].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[0, 4].transform);
        boardMatrix[0, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[0, 5].transform);
        boardMatrix[0, 6].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[0, 6].transform);
        boardMatrix[0, 7].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[0, 7].transform);
        boardMatrix[1, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[1, 5].transform);

        boardMatrix[3, 0].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[3, 0].transform);
        boardMatrix[3, 10].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[3, 10].transform);
        boardMatrix[4, 0].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[4, 0].transform);
        boardMatrix[4, 10].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[4, 10].transform);
        boardMatrix[5, 0].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[5, 0].transform);
        boardMatrix[5, 1].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[5, 1].transform);
        boardMatrix[5, 9].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[5, 9].transform);
        boardMatrix[5, 10].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[5, 10].transform);
        boardMatrix[6, 0].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[6, 0].transform);
        boardMatrix[6, 10].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[6, 10].transform);
        boardMatrix[7, 0].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[7, 0].transform);
        boardMatrix[7, 10].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[7, 10].transform);

        boardMatrix[9, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[9, 5].transform);
        boardMatrix[10, 3].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[10, 3].transform);
        boardMatrix[10, 4].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[10, 4].transform);
        boardMatrix[10, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[10, 5].transform);
        boardMatrix[10, 6].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[10, 6].transform);
        boardMatrix[10, 7].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("BlackPiece"), boardMatrix[10, 7].transform);

        //Assignation de position des White pieces (Défenseur)
        boardMatrix[3, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[3, 5].transform);
        boardMatrix[4, 4].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[4, 4].transform);
        boardMatrix[4, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[4, 5].transform);
        boardMatrix[4, 6].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[4, 6].transform);
        boardMatrix[5, 3].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[5, 3].transform);
        boardMatrix[5, 4].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[5, 4].transform);
        boardMatrix[5, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("KingWhitePiece"), boardMatrix[5, 5].transform);
        boardMatrix[5, 6].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[5, 6].transform);
        boardMatrix[5, 7].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[5, 7].transform);
        boardMatrix[6, 4].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[6, 4].transform);
        boardMatrix[6, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[6, 5].transform);
        boardMatrix[6, 6].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[6, 6].transform);
        boardMatrix[7, 5].GetComponent<Cell>().Piece = Instantiate(Resources.Load<GameObject>("WhitePiece"), boardMatrix[7, 5].transform);

        //Boucle pour assigner une référence à cette classe à toutes les cases du tableau de jeu
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                boardMatrix[i, j].GetComponent<Cell>().board = this;
            }
        }
    }

}
