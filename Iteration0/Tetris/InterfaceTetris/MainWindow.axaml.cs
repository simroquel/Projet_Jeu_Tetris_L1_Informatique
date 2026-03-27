/**************************************** Fichier MainWindow.axaml.cs
 * Gère l'interface du jeu de Tetris : la fenêtre graphique et 
 * l'ensemble des interactions du jeu.
 * Auteur : Le groupe 3MS
 * Version : 2
 *****************************************/

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using Avalonia.Threading;
using Avalonia.Media;
// à ajouter à partir de l'itération 1
using NoyauTetris;

namespace InterfaceTetris;

/* Gère la fenêtre principale du jeu de Tetris, et l'ensemble des interactions du jeu. */
public partial class MainWindow : Window
{
    /* Minuteur qui déclanche régulièrement un évènement. */
    public DispatcherTimer Minuteur;
    /** Sert à initialiser la largeur et la hauteur des carrés et à définir la taille des pixels des marges*/    
    public int TailleCarre = 22;
    public int LargeurCadre = 10;
    public int Marge = 40;
    // Attribut pour gerer la logique du jeu
    public JeuTetris Jeu;

    public MainWindow()
    {
        InitializeComponent();
        // Défini la taille de la fenêtre à partir des constantes
        Width = 300;
        Height = 600;
        // Définit le texte de InfoText
        InfoText.Text = "Jeu Tetris de l'équipe 3MS";
        // Défini la taille du canvas à partir des constantes
        TetrisCanvas.Width = JeuTetris.LargeurGrille * TailleCarre;
        TetrisCanvas.Height = JeuTetris.HauteurGrille * TailleCarre;
        // Défini la taille des boutons à partir des constantes
        StartButton.Width =  TetrisCanvas.Width;
        StartButton.Height = 35;
        QuitButton.Width = TetrisCanvas.Width;
        QuitButton.Height = 35; 
        // Initialise le minuteur pour faire descendre le tetrino courant toutes les 500 milisecondes
        Minuteur = new DispatcherTimer();
        Minuteur.Interval = TimeSpan.FromMilliseconds(500);
        Minuteur.Tick += (s, e) => { BasInterface();};   
        // détecte le clic sur le bouton Démarrer, déclanche l'évènement Demarrer, puis appelle la méthode DemarrerTetris
        StartButton.Click += (s, e) => { DemarrerInterface();};
        // détecte le clic sur le bouton Quitter, déclanche l'évènement Quiter, puis ferme la fenêtre
        QuitButton.Click += (s, e) => { Close();};

        // Initialisation de l'instance du jeu
        Jeu = new JeuTetris();

        // détecte la pression d'une touche du clavier, et déclanche l'évènement correspondant
        KeyDown += (s, e) =>
        {
            // Choix des touches à modifier si besoin (voir la documentation de l'énumération Key)
            if (e.Key == Key.Left)
            {
                GaucheInterface();
            }
            else if (e.Key == Key.Right)
            {
                DroiteInterface();
            }
            else if (e.Key == Key.X)
            // si vous disposer d'un pavé numérique, choisir Key.PageUp
            {
                RotationDroiteInterface();
            }
            else if (e.Key == Key.W)
            // si vous disposer d'un pavé numérique, choisir Key.Home
            {
                RotationGaucheInterface();
            }
            else if (e.Key == Key.Down)
            {
                TombeInterface();
            }
        };
    } 
    /** Méthode qui déssine le jeu*/
    public void DessinerJeu()
    {
        // Vide le canvas pour redéssiner à chaque mouvement
        TetrisCanvas.Children.Clear();
        DessinerCadre(); 
        // Récupère les positions réelles du tetrino dans le jeu 
        Position[] positionsDuTetrino = Jeu.TetrinoCourant.Positions();
        foreach (Position p in positionsDuTetrino)
        {
            // On n'affiche que si l'ordonnée est positive (y >= 0)
             if (p.y >= 0)
            {
                DessinerCarre(
                p.x * TailleCarre,
                p.y * TailleCarre,
                Jeu.TetrinoCourant.Couleur
                );
            }
        }
    }
    
    /**Traduit les couleurs de l'énum TetrinoCouleur en couleurs affichables
    @param couleur une couleur de l'énum TetrinoCouleur
    @return IBrush retrourne une valeur de type IBrush
    */
    public IBrush Couleur2Affichable(TetrinoCouleur couleur)
    {
        switch (couleur)
        {
            case TetrinoCouleur.noir:
            return Brushes.Black;

            case TetrinoCouleur.rouge:
            return Brushes.Red;

            case TetrinoCouleur.jaune:
            return Brushes.Yellow;

            case TetrinoCouleur.bleu:
            return Brushes.Blue;

            case TetrinoCouleur.gris:
            return Brushes.Gray;

            default:
            return Brushes.White;
        }
    }

    /** Dessine un rectangle dans le TetrisCanvas, à la position (x, y), de largeur with, 
    de hauteur height (en pixels) et de couleur couleur. */
    public void DessinerRectangle(int x, int y, int with, int height, Avalonia.Media.IBrush couleur)
    {
        TetrisCanvas.Children.Add(new Avalonia.Controls.Shapes.Rectangle
        {
            Width = with,
            Height = height,
            Fill = couleur,
            Margin = new Thickness(x, y, 0, 0) 
        });
    }

    /** 
    Sert à dessiner les carrés du jeu avec un contour noir en prenant comme parametre:
    @param x type int: position x du carré
    @param y type int: position y du carré
    @param couleur type TetrinoCouleur: La couleur qu'on veut donner au carré
    **/
    public void DessinerCarre(int x, int y, TetrinoCouleur couleur)
    {

        DessinerRectangle(x, y, TailleCarre, TailleCarre, Couleur2Affichable(TetrinoCouleur.noir));
        DessinerRectangle(x + 1, y+ 1, TailleCarre -2 , TailleCarre-2, Couleur2Affichable(couleur));
        
    }


    /** Dessine le cadre, Démarre le jeu et le dessine*/
    public void DemarrerInterface()
    {
        Console.WriteLine("Démarrage du jeu de Tetris à coder...");
        DessinerCadre();
        DessinerCarre(10, 0, TetrinoCouleur.rouge);
        DessinerCarre(32, 22, TetrinoCouleur.jaune);
        DessinerCarre(54, 44, TetrinoCouleur.bleu);
        Jeu.Demarrer();    // Initialise le tetrino courant 
        Minuteur.Start();  // Lance la descente automatique 
        DessinerJeu();
    }

    
    /* ... */
    public void DroiteInterface()
    {
        Jeu.Droite(); // Déplace le tetrino d'une case vers la droite
        DessinerJeu(); // Rafraîchit l'écran
    }
    

    /* ... */
    public void GaucheInterface()
    {
        Jeu.Gauche();
        DessinerJeu();
    }

    /* ... */
    public void BasInterface()
    {
        Jeu.Bas();
        DessinerJeu();
    }

    /* ... */
    public void TombeInterface()
    {
        Jeu.Tombe();
        DessinerJeu();
    }

    /* ... */
    public void RotationDroiteInterface()
    {
        Console.WriteLine("Rotation à droit à coder...");
    }

    /* ... */
    public void RotationGaucheInterface()
    {
        Console.WriteLine("Rotation à gauche à coder...");
    }
    /**Dessine le cadre du terrain de jeu sur le canvas en traçant
    les bordures gauche, droite et basse avec une épaisseur fixe*/
    public void DessinerCadre()
    {
        int largeur = (int)TetrisCanvas.Width;
        int hauteur = (int)TetrisCanvas.Height;
        IBrush couleurCadre = Couleur2Affichable(TetrinoCouleur.gris);

        // Fond blanc

        // Bord gauche
        DessinerRectangle(-LargeurCadre, 0, LargeurCadre, hauteur + LargeurCadre, couleurCadre);

        // Bord droit
        DessinerRectangle(largeur, 0, LargeurCadre, hauteur + LargeurCadre, couleurCadre);

        // Bord bas
        DessinerRectangle(0, hauteur, largeur, LargeurCadre, couleurCadre);
    }
}
