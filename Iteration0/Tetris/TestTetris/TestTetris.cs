using NoyauTetris;

namespace TestTetris;

public class TestJeuTetris
{
    [Fact]
    /**Teste si TetrinoCourant n'est pas null à l'appel de la méthode Démarrer()*/
    public void TestDemarrer()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        Assert.NotNull(jeu.TetrinoCourant);
    }
    [Fact]
    /**Teste si Gauche() ne déplace pas le Tetrino en dehors du cadre*/
    public void TestGauche_Limite()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posXAvant = jeu.TetrinoCourant.PositionOrigine.x;
        jeu.Gauche();
        Assert.Equal(posXAvant , jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**Teste si Gauche() déplace le Tetrino quand il n'est pas à la limite*/
    public void TestGauche_Deplace()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posXDebut = jeu.TetrinoCourant.PositionOrigine.x + 1;
        jeu.Gauche();
        Assert.Equal(posXDebut - 1 , jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**Teste si Droite() ne déplace pas le Tetrino en dehors du cadre*/
    public void TestDroite_Limite()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        jeu.TetrinoCourant.PositionOrigine.x = jeu.TetrinoCourant.PositionOrigine.x + JeuTetris.LargeurGrille - 1;
        int posXLimite = jeu.TetrinoCourant.PositionOrigine.x;
        jeu.Droite();
        Assert.Equal(posXLimite, jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**Teste si Droite() déplace le Tetrino quand il n'est pas à la limite*/
    public void TestDroite_Deplace()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posXAvant = jeu.TetrinoCourant.PositionOrigine.x;
        jeu.Droite();
        Assert.Equal(posXAvant + 1 , jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**Teste si Bas(), quand le Tetrino est à la limite, crée un nouveau Tetrino d'origine (0,0)*/
    public void TestBas_Limite()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        jeu.TetrinoCourant.PositionOrigine.y = jeu.TetrinoCourant.PositionOrigine.y + JeuTetris.HauteurGrille - 1;
        jeu.Bas();
        Assert.Equal(0, jeu.TetrinoCourant.PositionOrigine.y);
        Assert.Equal(0, jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**Teste si Bas() déplace le Tetrino en bas quand il n'est pas à la limite*/
    public void TestBas_Deplace()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posYAvant = jeu.TetrinoCourant.PositionOrigine.y;
        jeu.Bas();
        Assert.Equal(posYAvant + 1, jeu.TetrinoCourant.PositionOrigine.y);
    }
}
public class TestPosition()
{   [Fact]
  
    /**test deplacer a gauche a l'unité*/
    public void deplacerGauche()
    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        jeu.TetrinoCourant.PositionOrigine.x= jeu.TetrinoCourant.PositionOrigine.x + 1;
        int posXAvant = jeu.TetrinoCourant.PositionOrigine.x;
        jeu.Gauche();
        Assert.Equal(posXAvant - 1, jeu.TetrinoCourant.PositionOrigine.x);
    } 
        [Fact]
    /**test deplacer a droite a l'unité*/
    public void deplacerDroite()    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posXAvant = jeu.TetrinoCourant.PositionOrigine.x;
        jeu.Droite();
        Assert.Equal(posXAvant + 1, jeu.TetrinoCourant.PositionOrigine.x);
    }
    [Fact]
    /**test deplacer en bas a l'unité*/
    public void deplacerBas()    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int posYAvant = jeu.TetrinoCourant.PositionOrigine.y;
        jeu.Bas();
        Assert.Equal(posYAvant + 1, jeu.TetrinoCourant.PositionOrigine.y);
     } 
[Fact]
    /**test la méthode RotationDroite en bas a l'unité*/

    // Mahhdi: J'ai coommencé à faire le test pas eu le temps de finir, mais je pense que c'est ce modele à suivre
    // Si vous faites d'autre test qui marchent en procédant autrement, tant que ça marche c'est parfait 
    public void TestRotationDroite()    {
        JeuTetris jeu = new JeuTetris();
        jeu.Demarrer();
        int indice = jeu.TetrinoCourant.Indice;
        int posXAvant = jeu.TetrinoCourant.PositionOrigine.x;
        int posYAvant = jeu.TetrinoCourant.PositionOrigine.y;
        
        if (indice == 1)
        {   
            jeu.RotationDroite();
            Assert.Equal(posXAvant, jeu.TetrinoCourant.PositionOrigine.x);
            Assert.Equal(posYAvant , jeu.TetrinoCourant.PositionOrigine.y);
            Assert.Equal(2, indice);
        }
        if (indice == 2)
        {   
            jeu.RotationDroite();
            Assert.Equal(posXAvant , jeu.TetrinoCourant.PositionOrigine.x);
            Assert.Equal(posYAvant + 1, jeu.TetrinoCourant.PositionOrigine.y);
            Assert.Equal(1, indice);
        }
        
     } 
}
