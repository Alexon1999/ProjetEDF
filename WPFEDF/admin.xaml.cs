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
    /// Logique d'interaction pour admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        controleur leControlleur;
        edfEntities gst;
        public admin(controleur unControlleur)
        {
            InitializeComponent();
            leControlleur = unControlleur;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
            lstControleurs.ItemsSource = gst.controleur.ToList();
        }

        private void lstControleurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstControleurs.SelectedItem != null)
            {
                List<client> lesClientDuControleur = gst.client.ToList().FindAll(cl => cl.idcontroleur == (lstControleurs.SelectedItem as controleur).id);

                //var query = (from cl in gst.client
                //             where cl.idcontroleur == (lstControleurs.SelectedItem as controleur).id
                //             select new ClientPerso
                //             {
                //                 Id = cl.identifiant,
                //                 NomClient = cl.nom,
                //                 PrenomClient = cl.prenom,
                //                 AncienReleve = cl.ancienReleve,
                //                 DerneirReleve = cl.dernierReleve,
                //                 Consommation = cl.dernierReleve - cl.ancienReleve
                //             }
                //             ).ToList();

                var query = (from cl in lesClientDuControleur
                             select new ClientPerso
                             {
                                 Id = cl.identifiant,
                                 NomClient = cl.nom,
                                 PrenomClient = cl.prenom,
                                 AncienReleve = cl.ancienReleve,
                                 DerneirReleve = cl.dernierReleve,
                                 Consommation = cl.dernierReleve - cl.ancienReleve
                             }).ToList();

                //lstClients.ItemsSource = gst.client.ToList().FindAll(cl => cl.idcontroleur == (lstControleurs.SelectedItem as controleur).id);
                lstClients.ItemsSource = query;

            }
        }

        private void btnAjoutControleur_Click(object sender, RoutedEventArgs e)
        {
            if(txtNomControleur.Text == "")
            {
                MessageBox.Show("Veuillez saisir un nom du controleur", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }else if(txtPrenomControleur.Text == "")
            {
                MessageBox.Show("Veuillez saisir un prenom du controleur", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //  MessageBox.Show(gst.controleur.ToList().Max(ct => ct.id).ToString());
                int maxId = gst.controleur.ToList().Max(ct => ct.id);
                // MessageBox.Show(txtNomControleur.Text.Substring(0,1) + txtPrenomControleur.Text.Substring(0,1) + "123");
                controleur nouveauControleur = new controleur()
                {
                    id = maxId + 1,
                    nom = txtNomControleur.Text,
                    prenom = txtPrenomControleur.Text,
                    login = txtNomControleur.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1).ToLower(),
                    mdp = txtNomControleur.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1).ToLower() + "123",
                    statut = "ctrl"
                };

                gst.controleur.Add(nouveauControleur);
                gst.SaveChanges();

                lstControleurs.ItemsSource = gst.controleur.ToList();
            }
        }

        private void btnAjoutClient_Click(object sender, RoutedEventArgs e)
        {
            if(txtNomClient.Text == "")
            {
                MessageBox.Show("Veuillez saisir un nom du client", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if(txtPrenomClient.Text == "")
            {
                MessageBox.Show("Veuillez saisir un prenom du client", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if(lstControleurs.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un controlleur", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                int maxId = gst.client.ToList().Max(cl => cl.identifiant);

                client nouveauClient = new client()
                {
                    identifiant = maxId + 1,
                    dernierReleve = 0,
                    ancienReleve = 0,
                    nom = txtNomClient.Text,
                    prenom = txtPrenomClient.Text,
                    idcontroleur = (lstControleurs.SelectedItem as controleur).id
                };

                gst.client.Add(nouveauClient);
                gst.SaveChanges();

                List<client> lesClientDuControleur = gst.client.ToList().FindAll(cl => cl.idcontroleur == (lstControleurs.SelectedItem as controleur).id);
                var query = (from cl in lesClientDuControleur
                             select new ClientPerso
                             {
                                 Id = cl.identifiant,
                                 NomClient = cl.nom,
                                 PrenomClient = cl.prenom,
                                 AncienReleve = cl.ancienReleve,
                                 DerneirReleve = cl.dernierReleve,
                                 Consommation = cl.dernierReleve - cl.ancienReleve
                             }).ToList();

                lstClients.ItemsSource = query;
            }
           
        }
    }
}
