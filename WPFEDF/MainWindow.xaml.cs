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

namespace WPFEDF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        edfEntities gst;
        controleur leControlleur;
        public MainWindow(controleur unControlleur)
        {
            InitializeComponent();
            leControlleur = unControlleur;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
            List<client> lesClientDuControleur = gst.client.ToList().FindAll(cl => cl.idcontroleur == leControlleur.id);
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

        private void lstClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAjoutReleve_Click(object sender, RoutedEventArgs e)
        {
            if(lstClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un client", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

            }else if(txtNouveauReleve.Text == "")
            {
                MessageBox.Show("Veuillez saisir un releve", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                ClientPerso selectedClient = (lstClients.SelectedItem as ClientPerso);

                if(Convert.ToInt16(txtNouveauReleve.Text) >= selectedClient.DerneirReleve)
                {
                    int ancienRelve = selectedClient.DerneirReleve;
                    int derneierReleve = Convert.ToInt16(txtNouveauReleve.Text);
                    gst.client.First(cl => cl.identifiant == selectedClient.Id).ancienReleve = ancienRelve;
                    gst.client.First(cl => cl.identifiant == selectedClient.Id).dernierReleve = derneierReleve;

                    gst.SaveChanges();

                    List<client> lesClientDuControleur = gst.client.ToList().FindAll(cl => cl.idcontroleur == leControlleur.id);
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
                else
                {
                    MessageBox.Show("Relevé saisie doit etre supérieur au dernier relevé du client");
                }
            }
        }
    }
}
