using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pendu
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Initialisation de toutes les variables et des listes
        bool Lettre_dedans = false;
        bool gagne = false;
        bool perdu = false;
        bool continuer = false;
        string mot_devine = "";
        string mot_affiche = "";
        public string[] MotsFR = { "PATATE", "ROULADE", "FORCE", "ORGANE", "PILOTI", "FARCEUR", "MOTIVATION", "VAILLANCE", "BOULETTE", "CONDUCTEUR", "POMME", "CHAT", "SOLEIL", "LIVRE", "AVION", "MUSIQUE",
            "BONHEUR",
            "MONTAGNE",
            "RIVIERE",
            "ORDINATEUR",
            "ETOILE",
            "CAFE",
            "PLUIE",
            "AMOUR",
            "ECOLOGIE",
            "AVENTURE",
            "SOURIRE",
            "VOYAGE",
            "LIBERTE"};
        public string[] MotsEN = {
            "APPLE",
            "CAT",
            "SUN",
            "BOOK",
            "PLANE",
            "MUSIC",
            "HAPPINESS",
            "MOUNTAIN",
            "RIVER",
            "COMPUTER",
            "STAR",
            "COFFEE",
            "RAIN",
            "LOVE",
            "RAINBOW",
            "ECOLOGY",
            "ADVENTURE",
            "SMILE",
            "TRAVEL",
            "FREEDOM",
            "OCEAN",
            "FIRE",
            "ELEPHANT",
            "GUITAR",
            "DREAM",
            "SCIENCE",
            "WISDOM",
            "WONDER",
            "FUTURE",
            "SPACE",
            "UNITY",
            "HERO",
            "LEGEND",
            "WARRIOR",
            "MYSTERY",
            "WISDOM",
            "UNITY",
            "JOURNEY",
            "DESTINY",
            "COURAGE",
            "BELIEVE",
            "INSPIRE",
            "IMAGINE",
            "PASSION",
            "DISCOVER",
            "ACHIEVE",
            "EXPLORE",
            "INNOVATE"
        };
        string mot_fin = "";
        int vie = 9;
        bool EN = false;
        bool FR = false;

        public void Start() //fonction de commencement, se lance après avoir choisi la langue
        {
            Img_Pendu.Source = new BitmapImage(new Uri("/NewFolder1/_8.jpg", UriKind.Relative));
            if (FR == true)
            {
                Random var = new Random();
                mot_devine = MotsFR[var.Next(MotsFR.Length)];
            }
            else if (EN == true)
            {
                Random var = new Random();
                mot_devine = MotsEN[var.Next(MotsEN.Length)];
            }
            for (int i = 0; i < mot_devine.Length; i++)
            {
                mot_affiche += "_ ";
            }
            txt_mot_affiche.Text = mot_affiche;
            foreach (Button tout_bouton in Gridkeyboard.Children.OfType<Button>())
            {
                tout_bouton.IsEnabled = true;
            }
        }


        // Quand on appuie sur un bouton, définie une variable i dans une boucle for qui vérifie si chaque caractère du mot_affiche correspond à la lettre du bouton
        // Si c'est le cas, créer un sous string qui prend tous les caractères strictement avant et après le "_ " correspondant et rajoute la lettre du bouton et défini Lettre_dedans comme vrai
        // Si l'on n'a pas trouvé de correspondante, on rajoute 1 au nombre d'erreur
        // Dans tous les cas on redéfini Lettre dedans comme faux a la fin pour s'en resservir dans les autres cas.
        //si la vie est épuisée, tous les boutons sont inaccessibles
        //si le joueur réussi, la vie est mise à zéro et les boutons sont désactivés pour activer les bouton oui et non
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            if (vie < 9)
            {
                Button btn = sender as Button;
                string btnContent = btn.Content.ToString();
                btn.IsEnabled = false;

                for (int i = 0; i < mot_devine.Length; i++)
                {
                    if (btnContent == mot_devine[i].ToString())
                    {
                        Lettre_dedans = true;
                        mot_affiche = mot_affiche.Substring(0, 2 * i) + btnContent + " " + mot_affiche.Substring(2 * i + 2);
                    }
                }
                txt_mot_affiche.Text = mot_affiche;
                if (Lettre_dedans == false)
                {
                    vie = vie - 1;
                    Img_Pendu.Source = new BitmapImage(new Uri("/NewFolder1/_" + vie.ToString() + ".jpg", UriKind.Relative));
                    if (vie <= 0)
                    {
                        vie = 0;
                        foreach (Button tout_bouton in Gridkeyboard.Children.OfType<Button>())
                        {
                            tout_bouton.IsEnabled = false;
                        }
                        txt_mot_affiche.Text = "le mot était " + mot_devine;
                    }
                    TB_vie.Text = vie.ToString();

                }
                mot_fin = mot_affiche.Replace(" ", "");
                if (mot_fin == mot_devine)
                {
                    Img_Pendu.Source = new BitmapImage(new Uri("/NewFolder1/G.jpg", UriKind.Relative));
                    vie = 0;
                    foreach (Button tout_bouton in Gridkeyboard.Children.OfType<Button>())
                    {
                        tout_bouton.IsEnabled = false;
                    }
                }
                Lettre_dedans = false;
            }
        }




        private void Non_BTN_Click(object sender, RoutedEventArgs e)// ce bouton sert à arrêter le jeu
        {
            if (vie == 0) //donc si la vie est égale à 0, le jeu se ferme
            {
                Environment.Exit(0);
            }
        }

        private void Oui_BTN_Click(object sender, RoutedEventArgs e) //le bouton oui recommence une partie
        {
            if (vie == 0) // si la vie est égale à 0, toutes les variables, image, affichage et boutons sont mis à leurs états d'origine
            {
                mot_devine = "";
                mot_affiche = "";
                vie = 9;
                TB_vie.Text = "8";
                Img_Pendu.Source = new BitmapImage(new Uri("/NewFolder1/_8.jpg", UriKind.Relative));
                txt_mot_affiche.Text = mot_affiche;
                foreach (Button tout_bouton in Gridkeyboard.Children.OfType<Button>())
                {
                    tout_bouton.IsEnabled = true;
                }
            }
        }

        private void FR_BTN_Click(object sender, RoutedEventArgs e) //le bouton permettant de choisir le mode français, lance start avec le boolean français en true
        {
            if (vie == 9)
            {
                TB_titre.Text = "Jeu du pendu par COLLIGNON thomas";
                FR = true;
                mot_devine = "";
                mot_affiche = "";
                vie = 8;
                Start();
                TB_vie.Text = vie.ToString();

            }

        }

        private void EN_BTN_Click(object sender, RoutedEventArgs e) //le bouton permettant le mode anglais, lance start avec le boolean anglais e true
        {
            if (vie == 9)
            {
                TB_titre.Text = "Hangman game by COLLIGNON thomas";
                EN = true;
                mot_devine = "";
                mot_affiche = "";
                vie = 8;
                Start();
                TB_vie.Text = vie.ToString();
            }
 
        }
    }
}