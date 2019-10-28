using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaveOnDownloads
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<ISaveAndLoad>();


            var perm = new PermissaoService();

            perm.Permissao(Permission.Storage);

            service.SaveText("MyTxtFile", "Is there anybody... out there?");

            myLabel.Text = service.LoadText("MyTxtFile");

        }
    }
}
