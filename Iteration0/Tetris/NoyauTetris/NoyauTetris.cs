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

public class JeuTetris
{
    public Tetrino TetrinoCourant;
    public static int largeurGrille = 12;
    public static int HauteurGrille = 15;
    public void Demarrer()
    {
        TetrinoCourant = TetrinoCourant.NouveauTetrino();
    }
    public void Gauche()
    {
        if(TetrinoCourant.PositionOrigine.x > 0)
        {
            TetrinoCourant.PositionOrigine.DeplacerGauche();
        } 
    }
    public void Droite()
    {
        if(TetrinoCourant.PositionOrigine.x < largeurGrille)
        {
            TetrinoCourant.PositionOrigine.DeplacerGauche();
        } 
    }
    public void Bas()
    {
        if(TetrinoCourant.PositionOrigine.y <= HauteurGrille)
        {
            TetrinoCourant.PositionOrigine.DeplacerBas();
        } 
        else {
            TetrinoCourant = TetrinoCourant.NouveauTetrino();
        }
    }
    public void Tombe()
    {
        TetrinoCourant.PositionOrigine.y = HauteurGrille;
    }


}

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
    public Tetrino NouveauTetrino()
    {
        Indice = rand.Next(TetrinosTab.GetLength(0));
        Couleur = CouleursTetrinos[rand.Next(CouleursTetrinos.Length)];
        PositionOrigine = new Position(0,0);
        return new Tetrino(Indice, PositionOrigine, Couleur);
    }
}
