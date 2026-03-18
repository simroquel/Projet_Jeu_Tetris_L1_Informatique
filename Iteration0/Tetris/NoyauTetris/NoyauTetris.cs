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
