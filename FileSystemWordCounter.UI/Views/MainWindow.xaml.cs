using FileSystemWordCounter.UI.Models;
using FileSystemWordCounter.UI.Models.Services;
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
using System.Windows.Forms;
using System.Web;

namespace FileSystemWordCounter.UI.Views
{
  /// <summary>
  /// Lógica de interacción para MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Browser_Click(object sender, RoutedEventArgs e)
    {
      using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
      {
        DialogResult result = dialog.ShowDialog();
        txtBrowser.Text = dialog.SelectedPath;
        txtBrowser.Focus();
      }
    }
  }
}
