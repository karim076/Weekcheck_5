using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace H5_Weekcheck_AttractieScherm
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {    
            //Stelt de file-picker in en opent deze
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".csv");

            //TODO: voeg hier je eigen code toe, zoals uit H5, paragraaf 7
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                tbFileInfo.Text = "Geen geldig bestand gekozen";
            }
            else
            {
                tbFileInfo.Text = file.Path;
            }
            using (var fileAcces = await file.OpenReadAsync())
            {
                using (var stream = fileAcces.AsStreamForRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string imageUrl = reader.ReadLine(); 
                        imgAttractie.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute)); 
                        spAttractie.Visibility = Visibility.Visible;
                        themaGebied.Text = reader.ReadLine();
                        attractieNaam.Text = reader.ReadLine();
                        beschrijving.Text = reader.ReadLine();
                        minimale_Lengte.Text = reader.ReadLine();
                        var fastpass = reader.ReadLine();

                        if (fastpass == "Ja")
                        {
                            Attractie.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
    }
}
