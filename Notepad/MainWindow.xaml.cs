using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numberOfChar = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            numberOfChar = txtBox.Text.Length;
            lbl.Content = $"{numberOfChar}/1000";
            if (AutoSave.IsChecked == true)
                File.WriteAllText("SavedFile.txt", txtBox.Text);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("SavedFile.txt", txtBox.Text);
            if (comboBox.Items.Count == 1)
                comboBox.Items.Add(Directory.GetCurrentDirectory() + "\\SavedFile.txt");
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e) => txtBox.Copy();

        private void btnCut_Click(object sender, RoutedEventArgs e) => txtBox.Cut();

        private void btnPaste_Click(object sender, RoutedEventArgs e) => txtBox.Paste();

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Focus();
            txtBox.SelectAll();
        }

        private void AutoSave_Checked(object sender, RoutedEventArgs e)
        {
            if (AutoSave.IsChecked == true)
                File.WriteAllText("SavedFile.txt", txtBox.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem != null)
            {
                string? selectedFilePath = comboBox.SelectedItem.ToString();

                var openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(selectedFilePath);

                if (openFileDialog.ShowDialog() == true)
                    comboBox.SelectedItem = openFileDialog.FileName;

            }
        }
    }
}
