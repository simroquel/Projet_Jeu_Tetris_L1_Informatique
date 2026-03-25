namespace NoyauTetris;
// blanc: pas de carré, noir: cadre et tour des carrés, autres: couleurs des tetrinos
public enum TetrinoCouleur
{
    blanc,
    gris,
    noir,
    rouge,
    jaune,
    bleu
}
/** Sert à définir la largeur et la hauteur des carrés */
public class JeuTetris
{
    public static int largeurGrille = 12;
    public static int HauteurGrille = 15;

}
/** Définit la position d'un carré avec ses positions x et y*/



/** Sert à définir la largeur et la hauteur des carrés */
/** Sert à définir la largeur et la hauteur des carrés */
public class MonJeuTetris
{
    public static int largeurGrille = 12;
    public static int HauteurGrille = 15;

    // Grille du jeu : 0 = vide, 1 = occupé
    public int[,] Grille;

    // Tetrimino courant : on utilise un tableau pour la forme et une position pour le coin supérieur gauche
    public int[,] TetriminoCourantForme;
    public Position TetriminoCourantPosition;

    // Constructeur
    public MonJeuTetris()
    {
        Grille = new int[HauteurGrille, largeurGrille];
    }

    // Initialise le jeu avec un nouveau tetrimino
    public void Demarrer()
    {
        Grille = new int[HauteurGrille, largeurGrille];
        TetriminoCourantForme = NouveauTetrimino();
        TetriminoCourantPosition = new Position(3, 0); // départ en haut au centre
    }

    // Déplace le tetrimino d'une case à droite
    public void Droite()
    {
        if (PositionValide(TetriminoCourantPosition.x + 1, TetriminoCourantPosition.y))
        {
            TetriminoCourantPosition.DeplacerDroite();
        }
    }

    // Déplace le tetrimino d'une case à gauche
    public void Gauche()
    {
        if (PositionValide(TetriminoCourantPosition.x - 1, TetriminoCourantPosition.y))
        {
            TetriminoCourantPosition.DeplacerGauche();
        }
    }

    // Déplace le tetrimino d'une case vers le bas
    public void Bas()
    {
        if (PositionValide(TetriminoCourantPosition.x, TetriminoCourantPosition.y + 1))
        {
            TetriminoCourantPosition.DeplacerBas();
        }
        else
        {
            FixerTetrimino();
            TetriminoCourantForme = NouveauTetrimino();
            TetriminoCourantPosition = new Position(3, 0);
        }
    }

    // Fait tomber le tetrimino jusqu'en bas
    public void Tombe()
    {
        while (PositionValide(TetriminoCourantPosition.x, TetriminoCourantPosition.y + 1))
        {
            TetriminoCourantPosition.DeplacerBas();
        }
        FixerTetrimino();
        TetriminoCourantForme = NouveauTetrimino();
        TetriminoCourantPosition = new Position(3, 0);
    }

    // Vérifie si la pièce peut être placée à la position donnée
    private bool PositionValide(int posX, int posY)
    {
        for (int i = 0; i < TetriminoCourantForme.GetLength(0); i++)
        {
            for (int j = 0; j < TetriminoCourantForme.GetLength(1); j++)
            {
                if (TetriminoCourantForme[i, j] == 0)
                    continue;

                int x = posX + j;
                int y = posY + i;

                if (x < 0 || x >= largeurGrille || y >= HauteurGrille)
                    return false;

                if (y >= 0 && Grille[y, x] == 1)
                    return false;
            }
        }
        return true;
    }

    // Fixe le tetrimino courant dans la grille
    private void FixerTetrimino()
    {
        for (int i = 0; i < TetriminoCourantForme.GetLength(0); i++)
        {
            for (int j = 0; j < TetriminoCourantForme.GetLength(1); j++)
            {
                if (TetriminoCourantForme[i, j] == 1)
                {
                    int x = TetriminoCourantPosition.x + j;
                    int y = TetriminoCourantPosition.y + i;

                    if (y >= 0)
                        Grille[y, x] = 1;
                }
            }
        }
    }

    // Crée un nouveau tetrimino (ici juste un carré 2x2 pour simplifier)
    private int[,] NouveauTetrimino()
    {
        return new int[,]
        {
            {1, 1},
            {1, 1}
        };
    }
}

/** Définit la position d'un carré avec ses positions x et y*/
public class Position
{
    public int x;
    public int y;
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void DeplacerGauche()
    {
        x = x - 1;
    }
    public void DeplacerDroite()
    {
        x = x + 1;
    }
    public void DeplacerBas()
    {
        y = y + 1;
    }
}



/* 
Tâche à faire:
- 
? TetrinoTab ??
*/
public class Tetrino
{
    //Indice correspond à la formDans le noyau, définissez une classee choisie (0 ou 1 ou 2)
    public int Indice;
    //PositionOrigine correspond à la position de l'origine de la forme dans le repère du jeu
    public Position PositionOrigine;
    //la couleur du Tetrino
    public TetrinoCouleur Couleur;
    // pour l'aléatoire
    public static Random rand = new Random();
    //CouleursTetrinos est le tableau des couleurs possibles d'un Tetrino
    public static TetrinoCouleur[] CouleursTetrinos = new TetrinoCouleur[]{TetrinoCouleur.rouge, TetrinoCouleur.jaune, TetrinoCouleur.bleu};
    public Tetrino(int indice, Position position, TetrinoCouleur couleur){
        Indice = indice;
        PositionOrigine = position;
        Couleur = couleur;
    }
    /**la forme des Tetrinos*/
    public static Position[][] TetrinosTab = new Position[][]
    {
    // carre
        new Position[] { new Position(0, 0), new Position(1, 0),
        new Position(0, -1), new Position(1, -1) },
    // barre horizontale
        new Position[] { new Position(0, 0), new Position(1, 0),
        new Position(2, 0), new Position(3, 0) },
    // barre verticale
        new Position[] { new Position(0, 0), new Position(0, -1),
        new Position(0, -2), new Position(0, -3) }
    };

    /**La Position du Tetrinos totale
    @return resultat position du Tetrino choisi dans le repère du jeu*/
    public Position[] Positions()
    {
        Position[] forme = TetrinosTab[Indice];
        Position[] resultat = new Position[forme.Length];
        for(int i = 0; i <forme.Length; i++)
        {
            resultat[i] = new Position(forme[i].x +  PositionOrigine.x, forme[i].y + PositionOrigine.y );
        }
        return resultat;
    }
    /**Met à jour le Tetrino avec un indice (forme) et couleur aléatoires et une position choisie */
    public void NouveauTetrino()
    {
        Indice = rand.Next(TetrinosTab.GetLength(0));
        Couleur = CouleursTetrinos[rand.Next(CouleursTetrinos.Length)];
        PositionOrigine = new Position(0,0);
    }
}
