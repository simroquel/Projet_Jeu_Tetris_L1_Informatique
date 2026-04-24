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
    public static Tetrino NouveauTetrino()
    {
        int indice = rand.Next(TetrinosTab.GetLength(0));
        TetrinoCouleur Couleur = CouleursTetrinos[rand.Next(CouleursTetrinos.Length)];
        int x_Max = 0;
        for(int i = 0; i < TetrinosTab[indice].Length; i++)
        {
            if(TetrinosTab[indice][i].x > x_Max) x_Max = TetrinosTab[indice][i].x;
        }
        Position position = new Position(rand.Next(JeuTetris.LargeurGrille - x_Max),0);
        return new Tetrino(indice, position, Couleur);
    }

     /**Effectue une rotation à Droite*/
  public void RotationDroite()
    {
        if (Indice == 1)
        {
            PositionOrigine.x += 1;
            PositionOrigine.y -= 1;
            Indice = 2;
            return;
        }

        if (Indice == 2)
        {
            PositionOrigine.y += 1;
            Indice = 1;
            return;
        }
    }
     /**Effectue une rotation à Gauche*/

    public void RotationGauche()
    {
        if (Indice == 1)
        {
            PositionOrigine.y -=1;
            Indice = 2;
            return;
        }  
        if (Indice == 2)
        {
            PositionOrigine.x -=1;
            PositionOrigine.y += 1;
            Indice = 1;
            return;
        }


    }
}
/**Fait apparaitre des Tetrinos au démarrage et quand le TetrinoCourant est tombé, définit le TetrinoCourant et ses déplacements*/
public class JeuTetris
{
    /**Définit le Tetrino courant*/
    public Tetrino TetrinoCourant;
    /**Définit la Grille du Jeu*/
    public TetrinoCouleur[,] Grille;
    /**Définit la largeur de la Grille*/
    public static int LargeurGrille = 10;
    /**Définit la hauteur de la Grille*/
    public static int HauteurGrille = 20;
    /**Construit le jeu*/
    public JeuTetris()
    {
        Grille = new TetrinoCouleur[LargeurGrille, HauteurGrille];
    }
    
    /**Appellée au démarrage, crée un nouveau tétrino*/
    public void Demarrer()
    {
        Grille = new TetrinoCouleur[LargeurGrille, HauteurGrille];
        for(int i = 0; i < LargeurGrille; i++)
        {
            for(int j = 0; j < HauteurGrille; j++)
            {
                Grille[i, j] = TetrinoCouleur.blanc;
            }
        }
        TetrinoCourant = Tetrino.NouveauTetrino();
    }
    /**Déplace à Gauche*/
    public void Gauche()
    {
        Position[] positions = TetrinoCourant.Positions();
        /**Test si un carré est dans la grille*/
        bool EstDansLaGrille(Position p)
        {
            return p.x >= 0 && p.x < LargeurGrille && p.y >= 0 && p.y < HauteurGrille;
        }
        if(TetrinoCourant.PositionOrigine.x > 0)
        {
            foreach (Position p in positions)
            {  
                //Ignore les carrés qui ne sont pas dans la grille
                if (p.y < 0)
                {
                    continue;
                }
                //Ne déplace pas à gauche si il y a  déja un tetrino figé
                if(EstDansLaGrille(p) == false || Grille[p.x - 1, p.y] != TetrinoCouleur.blanc)
                {
                    return;
                }
            }
            TetrinoCourant.PositionOrigine.DeplacerGauche();
        } 

    }
    /**Déplace à Droite*/
    public void Droite()
    {
        Position[] positions = TetrinoCourant.Positions();
        /**Test si un carré est dans la grille*/
        bool EstDansLaGrille(Position p)
        {
            return p.x >= 0 && p.x < LargeurGrille && p.y >= 0 && p.y < HauteurGrille;
        }
        //Vérifie la position de chaque carrée du Tetrino pour savoir si le déplacement est possible
        foreach (Position p in positions)
        {
            if (p.x >= LargeurGrille - 1)
            {
                return;
            }
            if (p.y < 0)
            {
                continue;
            }
            //Ne déplace pas à droite si il y a  déja un tetrino figé
            if(EstDansLaGrille(p) == false || Grille[p.x + 1, p.y] != TetrinoCouleur.blanc)
            {
                return;
            }
        }
        TetrinoCourant.PositionOrigine.DeplacerDroite();
    }
    /**Déplace en Bas, si le Tetrino est tout en bas il se fige, si il y a des carrés figés juste en dessous il se fige, puis un nouveau Tetrino apparaît */
    public void Bas()
    {
        Position[] positions = TetrinoCourant.Positions();
        //Vérifie la position de chaque carrée du Tetrino pour savoir si le déplacement est possible
        foreach (Position p in positions)
        {  
            if (p.x >= LargeurGrille || p.x < 0 || p.x > HauteurGrille)
            {
                return;
            }
            //Fige si tout en bas
            if(p.y == HauteurGrille - 1)
            {
                FigerTetrino();
                TetrinoCourant = Tetrino.NouveauTetrino();
                return;
            } 

            //Fige si collision avec un bloc
            if(p.y >= 0 && Grille[p.x, p.y + 1] != TetrinoCouleur.blanc)
            {
                FigerTetrino();
                TetrinoCourant = Tetrino.NouveauTetrino();
                return;
            }
        }
        TetrinoCourant.PositionOrigine.DeplacerBas();
    }
    



    /**Fait tomber en bas et fait apparaître un nouveau Tetrino*/
    public void Tombe()
    {
        bool peutTomber = true;
        while (peutTomber == true)
        {
            Position[] positions = TetrinoCourant.Positions();
            //Vérifie la position de chaque carrée du Tetrino pour savoir si le déplacement est possible
            foreach (Position p in positions)
            {  
                //Fige si tout en bas
                if(p.y == HauteurGrille - 1)
                {
                    FigerTetrino();
                    TetrinoCourant = Tetrino.NouveauTetrino();
                    peutTomber = false;
                    return;
                } 

                //Fige si collision avec un bloc
                if(p.y >= 0 && Grille[p.x, p.y + 1] != TetrinoCouleur.blanc)
                {
                    FigerTetrino();
                    TetrinoCourant = Tetrino.NouveauTetrino();
                    peutTomber = false;
                    return;
                }
            }
            TetrinoCourant.PositionOrigine.DeplacerBas();
        }    
    }
    /**Stock la position du Tetrino Courant dans un tableau 
    Met à jour la forme du Tetrino avec RotationDroite()
    Si le Tetrino n'est pas dans la grille, il recupere sa position stocké dans le tableau ancienne position
    et donc ne fera pas une Rotation à Droite*/

    public void RotationDroite()
    {
        int ancienneIndice = this.TetrinoCourant.Indice;
        Position anciennePosition = new Position(this.TetrinoCourant.PositionOrigine.x, this.TetrinoCourant.PositionOrigine.y);

        this.TetrinoCourant.RotationDroite();

         bool EstDansLaGrille(Position p)
        {
            return p.x >= 0 && p.x < LargeurGrille && p.y >= 0 && p.y < HauteurGrille && this.Grille[p.x, p.y] == TetrinoCouleur.blanc;
        }

        foreach (Position p in this.TetrinoCourant.Positions())
        {
            if (!EstDansLaGrille(p))
            {
                this.TetrinoCourant.Indice = ancienneIndice;
                this.TetrinoCourant.PositionOrigine.y = anciennePosition.y;
                this.TetrinoCourant.PositionOrigine.x = anciennePosition.x;
            }
        }
    }


     /**Stock la position du Tetrino Courant dans un tableau 
    Met à jour la forme du Tetrino avec RotationGauche()
    Si le Tetrino n'est pas dans la grille, il recupere sa position stocké dans le tableau ancienne position
    et donc ne fera pas une Rotation à Gauche*/
public void RotationGauche()
    {
        int ancienneIndice = this.TetrinoCourant.Indice;
        Position anciennePosition = new Position(this.TetrinoCourant.PositionOrigine.x, this.TetrinoCourant.PositionOrigine.y);
        this.TetrinoCourant.RotationGauche();

         bool EstDansLaGrille(Position p)
        {
            return p.x >= 0 && p.x < LargeurGrille && p.y >= 0 && p.y < HauteurGrille && this.Grille[p.x, p.y] == TetrinoCouleur.blanc;
        }

        foreach (Position p in this.TetrinoCourant.Positions())
        {
            if (!EstDansLaGrille(p))
            {
                this.TetrinoCourant.Indice = ancienneIndice;
                this.TetrinoCourant.PositionOrigine.y = anciennePosition.y;
                this.TetrinoCourant.PositionOrigine.x = anciennePosition.x;
            }
        }
    }
    
    
    /**Fige le Tetrino courant dans la grille: ajoute les carrés dans la grille et supprime les lignes pleines de la grille*/
    public void FigerTetrino()
    {
        //Ajouter les carrés du Tetrino Courant dans la Grille
        Position[] positions = TetrinoCourant.Positions();
        foreach(Position p in positions)
        {
            if(p.y >= 0 && p.y < HauteurGrille)
            {
                Grille[p.x, p.y] = TetrinoCourant.Couleur;
            }
            
        }

        //Vérifier si une ligne de la grille est pleine
        for(int y = 0; y < HauteurGrille; y++)
        {
            bool LignePleine = true;
            for(int x = 0; x < LargeurGrille; x++){
                if(Grille[x, y] == TetrinoCouleur.blanc)
                {
                   LignePleine = false;
                   break;
                }
            }

            //Si LignePleine reste true, alors on la supprime (on déclale toutes les lignes au dessus d'un cran vers le bas)
            if(LignePleine == true)
            {
                for(int x = 0; x < LargeurGrille; x++)
                {
                    for(int j = y; j > 0; j--)
                    {
                        Grille[x,j] = Grille[x, j-1];
                    }
                }
                //on vide la ligne tout en haut
                for(int x = 0; x < LargeurGrille; x++)
                {
                    Grille[x, 0] = TetrinoCouleur.blanc;
                }
            
            }
        }

    }

}
