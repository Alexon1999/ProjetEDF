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
using System.Windows.Shapes;

namespace WPFEDF
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        edfEntities gst;
        public Authentification()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtLogin.Text == "")
            {
                MessageBox.Show("veuillez saisir un login", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(txtMdp.Text == "")
                {
                    MessageBox.Show("veuillez saisir un mot de passe", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //controleur leCtrl = gst.controleur.ToList().Find(ct => ct.login == ?? && ct.mdp == ??);
                    //if (leCtr == null) 

                    //ok
                    var query = from unControlleur in gst.controleur
                                where unControlleur.login == txtLogin.Text && unControlleur.mdp == txtMdp.Text
                                select unControlleur;

                    if(query.Count() == 0)
                    {
                        txtMessage.Text = "Vos identifiants sont incorrects";
                    }
                    else
                    {
                        // MessageBox.Show(query.First().nom);
                            controleur lecontroleur = query.First();
                        if(query.First().statut == "admin")
                        {
                            admin a = new admin(lecontroleur);
                            a.Show();
                        }
                        else
                        {
                            MainWindow frm = new MainWindow(lecontroleur);
                            frm.Show();
                        }
                    }

                }
            }
        }
    }
}
