/**************************************** Fichier MainWindow.axaml.cs
 * Gère l'interface du jeu de Tetris : la fenêtre graphique et 
 * l'ensemble des interactions du jeu.
 * Auteur : ...
 * Version : alpha
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
    public int TailleCarre = 22;
    public int LargeurCadre = 12;
    public int Marge = 40;

    public MainWindow()
    {
        InitializeComponent();
        // Défini la taille de la fenêtre à partir des constantes
        Width = 300;
        Height = 600 ;
        // Définit le texte de InfoText
        InfoText.Text = "Zone de texte";
        // Défini la taille du canvas à partir des constantes
        TetrisCanvas.Width = 200;
        TetrisCanvas.Height = 400+ Marge*2;
        // Défini la taille des boutons à partir des constantes
        StartButton.Width = 200;
        StartButton.Height = 40+ Marge*2;
        QuitButton.Width = 200;
        QuitButton.Height = 40+ Marge*2; 
        // Initialise le minuteur pour faire descendre le tetrino courant toutes les 500 milisecondes
        Minuteur = new DispatcherTimer();
        Minuteur.Interval = TimeSpan.FromMilliseconds(500);
        Minuteur.Tick += (s, e) => { BasInterface();};   
        // détecte le clic sur le bouton Démarrer, déclanche l'évènement Demarrer, puis appelle la méthode DemarrerTetris
        StartButton.Click += (s, e) => { DemarrerInterface();};
        // détecte le clic sur le bouton Quitter, déclanche l'évènement Quiter, puis ferme la fenêtre
        QuitButton.Click += (s, e) => { Close();};
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
    //Traduit les couleurs de l'énum en couleurs affichables
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

            default:
            return Brushes.White;
        }
    }




    /* Dessine un rectangle dans le TetrisCanvas, à la position (x, y), de largeur width, 
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


    public void DessinerCarre(int x, int y, TetrinoCouleur couleur)
    {
        
        DessinerRectangle(x, y, TailleCarre, TailleCarre, Couleur2Affichable(couleur) );
    }


    /* ... */
    public void DemarrerInterface()
    {
        Console.WriteLine("Démarrage du jeu de Tetris à coder...");
        DessinerCarre(0, 0, TetrinoCouleur.rouge);
        DessinerCarre(22, 22, TetrinoCouleur.jaune);
        DessinerCarre(44, 44, TetrinoCouleur.bleu);


    }

    /* ... */
    public void DroiteInterface()
    {
        Console.WriteLine("Déplacement à droite à coder...");
    }

    /* ... */
    public void GaucheInterface()
    {
        Console.WriteLine("Déplacement à gauche à coder...");
    }

    /* ... */
    public void BasInterface()
    {
        Console.WriteLine("Déplacement en bas à coder...");
    }

    /* ... */
    public void TombeInterface()
    {
        Console.WriteLine("Déplacement rapide en bas à coder...");

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
public class DessinerCadre
{
    public static void cadre(Canvas canvas)
    {
        // Dessine le cadre du TetrisCanvas
        canvas.Children.Add(new Avalonia.Controls.Shapes.Rectangle
        {
            Width = 200,
            Height = 400,
            Stroke = Avalonia.Media.Brushes.Black,
            StrokeThickness = 2,
            Margin = new Thickness(0, 0, 0, 0) 
        });
    }
}
}
