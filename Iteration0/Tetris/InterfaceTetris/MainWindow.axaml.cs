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
// à ajouter à partir de l'itération 1
//using NoyauTetris;

namespace InterfaceTetris;

/* Gère la fenêtre principale du jeu de Tetris, et l'ensemble des interactions du jeu. */
public partial class MainWindow : Window
{
    /* Minuteur qui déclanche régulièrement un évènement. */
    public DispatcherTimer Minuteur;
    
    public MainWindow()
    {
        InitializeComponent();
        // Défini la taille de la fenêtre à partir des constantes
        Width = 300;
        Height = 600;
        // Définit le texte de InfoText
        InfoText.Text = "Zone de texte";
        // Défini la taille du canvas à partir des constantes
        TetrisCanvas.Width = 200;
        TetrisCanvas.Height = 400;
        // Défini la taille des boutons à partir des constantes
        StartButton.Width = 200;
        StartButton.Height = 30;
        QuitButton.Width = 200;
        QuitButton.Height = 30; 
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
    private int posX;
private int posY;
private int taille = 30;

/* Dessine un rectangle dans le TetrisCanvas, à la position (x, y), de largeur width, 
de hauteur height (en pixels) et de couleur couleur. */
public void DessinerRectangle(int x, int y, int with, int height, Avalonia.Media.IBrush couleur)
{
    TetrisCanvas.Children.Add(new Avalonia.Controls.Shapes.Rectangle
    {
        Width = with,
        Height = height,
        Fill = couleur,
        Margin = new Avalonia.Thickness(x, y, 0, 0)
    });
}

/* ... */
public void DemarrerInterface()
{
    Console.WriteLine("Démarrage du jeu de Tetris...");
    
    TetrisCanvas.Children.Clear();
    posX = 100;
    posY = 0;
}

/* ... */
public void DroiteInterface()
{
    Console.WriteLine("Déplacement à droite");

    posX += taille;

    TetrisCanvas.Children.Clear();
}

/* ... */
public void GaucheInterface()
{
    Console.WriteLine("Déplacement à gauche");

    posX -= taille;

    TetrisCanvas.Children.Clear();
}

/* ... */
public void BasInterface()
{
    Console.WriteLine("Déplacement en bas");

    posY += taille;

    TetrisCanvas.Children.Clear();
}

/* ... */
public void TombeInterface()
{
    Console.WriteLine("Déplacement rapide en bas");

    posY += taille * 5;

    TetrisCanvas.Children.Clear();
}

/* ... */
public void RotationDroiteInterface()
{
    Console.WriteLine("Rotation à droite");

    TetrisCanvas.Children.Clear();
}

/* ... */
public void RotationGaucheInterface()
{
    Console.WriteLine("Rotation à gauche");

    TetrisCanvas.Children.Clear();
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
