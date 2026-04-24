/**************************************** * @file MainWindow.axaml.cs
 * Documentation générale du fichier : Gère l'interface graphique du jeu Tetris.
 * Ce fichier assure la liaison entre la vue (Avalonia) et la logique métier.
 * * @author Groupe 3MS
 * @version 2
 *****************************************/

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using Avalonia.Threading;
using Avalonia.Media;
using NoyauTetris;

namespace InterfaceTetris;

/** * Gère la fenêtre principale du jeu et l'ensemble des interactions utilisateur.
 */
public partial class MainWindow : Window
{
    /** Minuteur gérant la cadence de descente automatique des pièces. */
    public DispatcherTimer Minuteur;
    
    /** Dimension en pixels du côté d'un carré élémentaire. */
    public int TailleCarre = 20; // Mis à jour (20px)
    
    /** Épaisseur visuelle du cadre de la grille de jeu. */
    public int LargeurCadre = 10;
    
    /** Marge de sécurité pour l'affichage. */
    public int Marge = 50; // Mis à jour (50px)

    // Création de l'instance de notre class JeuTetris
    public JeuTetris Jeu = new JeuTetris();

    /**
     * Constructeur de la fenêtre principale.
     * Initialise les composants graphiques, les dimensions et les écouteurs d'événements.
     */
    public MainWindow()
    {
        InitializeComponent();
        /** Définit la taille de la fenêtre à partir des constantes. */
        Width = 300;
        Height = 600;
        /** Définit le texte de InfoText. */
        InfoText.Text = "Jeu Tetris de l'équipe 3MS";
        /** Définit la taille du canvas à partir de la largeur de la grille et de la taille des carrés. */
        TetrisCanvas.Width = JeuTetris.LargeurGrille * TailleCarre;
        TetrisCanvas.Height = JeuTetris.HauteurGrille * TailleCarre;
        /** Définit la taille des boutons à partir de la largeur du canvas. */
        StartButton.Width = TetrisCanvas.Width;
        StartButton.Height = 36; // Mis à jour (36px)
        QuitButton.Width = TetrisCanvas.Width;
        QuitButton.Height = 36; // Mis à jour (36px)
        /** Initialise le minuteur pour faire descendre le tetrino courant toutes les 500 milisecondes. */
        Minuteur = new DispatcherTimer();
        Minuteur.Interval = TimeSpan.FromMilliseconds(500);
        Minuteur.Tick += (s, e) => { BasInterface(); };   
        /** détecte le clic sur le bouton Démarrer, déclanche l'évènement DemarrerInterface, puis appelle la méthode DemarrerTetris. */
        StartButton.Click += (s, e) => { DemarrerInterface(); };
        /** détecte le clic sur le bouton Quitter, déclanche l'évènement Quiter, puis ferme la fenêtre*/
        QuitButton.Click += (s, e) => { Close(); };
        /** Initialisation de l'instance du jeu. */
        Jeu = new JeuTetris();
        /** détecte la pression d'une touche du clavier, et déclanche l'évènement correspondant. */
        KeyDown += (s, e) =>
        {
            if (e.Key == Key.Left) GaucheInterface();
            else if (e.Key == Key.Right) DroiteInterface();
            else if (e.Key == Key.X) RotationDroiteInterface();
            else if (e.Key == Key.W) RotationGaucheInterface();
            else if (e.Key == Key.Down) TombeInterface();
        };
    } 

    /** * Assure le rendu graphique complet de l'état actuel du jeu.
     * 
Efface le canvas et redessine le cadre ainsi que le Tetrino courant.
@todo Changer la place de la clear pour garder le tetrino posé
     */
    public void DessinerJeu()
    {
        TetrisCanvas.Children.Clear();
        DessinerCadre(); 
        /** Définit la position du tetrino */
        Position[] positionsDuTetrino = Jeu.TetrinoCourant.Positions();
        foreach (Position p in positionsDuTetrino)
        {
             if (p.y >= 0)
            {
                DessinerCarre(p.x * TailleCarre, p.y * TailleCarre, Jeu.TetrinoCourant.Couleur);
            }
        }
        // Dessiner les carrés figés de la grille 
        // On parcourt la grille
        for (int y = 0; y < 20; y++)
        {
            for(int x =0; x < 10; x++)
            {
                //On récupère la couleur stockée dans le tableau à 2 dimensions
                TetrinoCouleur couleurCase = Jeu.Grille[x,y];
                if (couleurCase != TetrinoCouleur.blanc)
                {
                    DessinerCarre(x * TailleCarre, y * TailleCarre, couleurCase);
                }
            }
        }
        // Dessin du tétrino courant en cours de chutte
        foreach (Position p in positionsDuTetrino)
        {
            // On ne dessine que les carrés à l'intérieur des limites visibles
            if (p.y >= 0)
            {
                DessinerCarre(p.x * TailleCarre, p.y * TailleCarre, Jeu.TetrinoCourant.Couleur);
            }
        }
        
    }
    
    /**
     * Convertit une couleur logique du noyau en ressource graphique affichable.
     * @param couleur La couleur issue de l'énumération TetrinoCouleur.
     * @return Une brosse (IBrush) utilisable par le moteur de rendu.
     */
    public IBrush Couleur2Affichable(TetrinoCouleur couleur)
    {
        switch (couleur)
        {
            case TetrinoCouleur.noir: return Brushes.Black;
            case TetrinoCouleur.rouge: return Brushes.Red;
            case TetrinoCouleur.jaune: return Brushes.Yellow;
            case TetrinoCouleur.bleu: return Brushes.Blue;
            case TetrinoCouleur.gris: return Brushes.Gray;
            default: return Brushes.White;
        }
    }

    /** * Dessine une forme rectangulaire sur le canvas.
     * @param x Position horizontale.
     * @param y Position verticale.
     * @param width Largeur du rectangle.
     * @param height Hauteur du rectangle.
     * @param couleur Couleur de remplissage.
     */
    public void DessinerRectangle(int x, int y, int width, int height, Avalonia.Media.IBrush couleur)
    {
        TetrisCanvas.Children.Add(new Avalonia.Controls.Shapes.Rectangle
        {
            Width = width,
            Height = height,
            Fill = couleur,
            Margin = new Thickness(x, y, 0, 0) 
        });
    }

    /** * Affiche un carré de jeu avec une bordure de contraste.
     * @param x Coordonnée X en pixels.
     * @param y Coordonnée Y en pixels.
     * @param couleur Couleur thématique du carré.
     */
    public void DessinerCarre(int x, int y, TetrinoCouleur couleur)
    {
        DessinerRectangle(x, y, TailleCarre, TailleCarre, Couleur2Affichable(TetrinoCouleur.noir));
        DessinerRectangle(x + 1, y + 1, TailleCarre - 2, TailleCarre - 2, Couleur2Affichable(couleur));
    }

    /** * Initialise une nouvelle partie.
     * Active la logique du jeu et démarre le processus de chute automatique.
     */
    public void DemarrerInterface()
    {
        DessinerCadre();
        Jeu.Demarrer(); 
        Minuteur.Start(); 
        DessinerJeu();
    }

    /** Commande le déplacement de la pièce vers la droite. */
    public void DroiteInterface()
    {
        Jeu.Droite();
        DessinerJeu();
    }

    /** Commande le déplacement de la pièce vers la gauche. */
    public void GaucheInterface()
    {
        Jeu.Gauche();
        DessinerJeu();
    }

    /** Gère la descente d'un rang de la pièce. */
    public void BasInterface()
    {
        Jeu.Bas();
        DessinerJeu();
    }

    /** Provoque la chute instantanée de la pièce au bas de la grille. */
    public void TombeInterface()
    {
        Jeu.Tombe();
        DessinerJeu();
    }

    /** * Effectue une rotation horaire de la pièce.
     * @todo Implémenter la logique de rotation dans le noyau.
     */
    public void RotationDroiteInterface()
    {
        Jeu.RotationDroite();
    }

    /** * Effectue une rotation anti-horaire de la pièce.
     * @todo Implémenter la logique de rotation dans le noyau.
     */
    public void RotationGaucheInterface()
    {
        Jeu.RotationGauche();
    }

    /** * Dessine les limites visuelles (bordures) de la zone de jeu.
     */
    public void DessinerCadre()
    {
        int largeur = (int)TetrisCanvas.Width;
        int hauteur = (int)TetrisCanvas.Height;
        IBrush couleurCadre = Couleur2Affichable(TetrinoCouleur.gris);

        DessinerRectangle(-LargeurCadre, 0, LargeurCadre, hauteur + LargeurCadre, couleurCadre);
        DessinerRectangle(largeur, 0, LargeurCadre, hauteur + LargeurCadre, couleurCadre);
        DessinerRectangle(0, hauteur, largeur, LargeurCadre, couleurCadre);
    }
}
