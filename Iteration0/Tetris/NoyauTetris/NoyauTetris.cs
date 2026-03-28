/*****************************************@file NoyauTetris.cs
 * Documentation générale du fichier : le Noyau du Jeu Tetris
 * @author Groupe 3MS
 * @version 2
 *****************************************/
namespace NoyauTetris;
/** enum qui contient des couleurs: blanc: pas de carré, gris: cadre, noir: contour des carrés, autres: couleurs des tetrinos*/
public enum TetrinoCouleur
{
    blanc,
    gris,
    noir,
    rouge,
    jaune,
    bleu
}

/**Fait apparaitre des Tetrinos au démarrage et quand le TetrinoCourant est tombé, définit le TetrinoCourant et ses déplacements*/
public class JeuTetris
{
    /**Définit le Tetrino courant*/
    public Tetrino TetrinoCourant;
    
    public static int LargeurGrille = 12;
    public static int HauteurGrille = 15;
    /**Appellée au démarrage, crée un nouveau tétrino*/
    public void Demarrer()
    {
        TetrinoCourant = Tetrino.NouveauTetrino();
    }
    /**Déplace à Gauche*/
    public void Gauche()
    {
 
        if(TetrinoCourant.PositionOrigine.x > 0)
        {
            TetrinoCourant.PositionOrigine.DeplacerGauche();
        } 

    }
    /**Déplace à Droite*/
    public void Droite()
    {
        Position[] positions = TetrinoCourant.Positions();
        //Vérifie la position de chaque carrée du Tetrino pour savoir si le déplacement est possible
        foreach (Position p in positions)
        {
            if (p.x >= LargeurGrille - 1)
            {
                return;
            }
        }

        TetrinoCourant.PositionOrigine.DeplacerDroite();
    }
    /**Déplace en Bas, si le Tetrino est tout en bas il disparaît et un nouveau Tetrino apparaît */
    public void Bas()
    {
        Position[] positions = TetrinoCourant.Positions();
        //Vérifie la position de chaque carrée du Tetrino pour savoir si le déplacement est possible
        foreach (Position p in positions)
        {
            if(p.y >= HauteurGrille - 1)
            {
                TetrinoCourant = Tetrino.NouveauTetrino();
            } 
        }
        TetrinoCourant.PositionOrigine.DeplacerBas();
    }
    /**Fait tomber en bas et fait apparaître un nouveau Tetrino*/
    public void Tombe()
    {
        TetrinoCourant.PositionOrigine.y = HauteurGrille;
        TetrinoCourant = Tetrino.NouveauTetrino();
    }


}

/**Définit la position d'un Tetrino et permet son déplacement à Gauche, Droite et Bas*/
public class Position
{
    /**abcisse de la position*/
    public int x;
    /**ordonnée de la position*/
    public int y;
    /**Construit la position*/
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    /**Déplace à gauche de 1 unité*/
    public void DeplacerGauche()
    {
        x = x - 1;
    }
    /**Déplace à droite de 1 unité*/
    public void DeplacerDroite()
    {
        x = x + 1;
    }
    /**Déplace en bas de 1 unité*/
    public void DeplacerBas()
    {
        y = y + 1;
    }
}

/**Définit un Tetrino*/
public class Tetrino
{
    /**Indice correspond à la forme dans le noyau, définissez une classe choisie (0 ou 1 ou 2)*/
    public int Indice;
    /**PositionOrigine correspond à la position de l'origine de la forme dans le repère du jeu*/
    public Position PositionOrigine;
    /**la couleur du Tetrino*/
    public TetrinoCouleur Couleur;
    /**pour l'aléatoire*/
    public static Random rand = new Random();
    /**CouleursTetrinos est le tableau des couleurs possibles d'un Tetrino*/
    public static TetrinoCouleur[] CouleursTetrinos = new TetrinoCouleur[]{TetrinoCouleur.rouge, TetrinoCouleur.jaune, TetrinoCouleur.bleu};
    /**Construit un Tetrino*/
    public Tetrino(int indice, Position position, TetrinoCouleur couleur){
        Indice = indice;
        PositionOrigine = position;
        Couleur = couleur;
    }
    /**Un tableau des formes des Tetrinos qui contiennent des tableaux avec la position de leurs carrés*/
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
    public static Tetrino NouveauTetrino()
    {
        int indice = rand.Next(TetrinosTab.GetLength(0));
        TetrinoCouleur Couleur = CouleursTetrinos[rand.Next(CouleursTetrinos.Length)];
        Position position = new Position(0,0);
        return new Tetrino(indice, position, Couleur);
    }
}
