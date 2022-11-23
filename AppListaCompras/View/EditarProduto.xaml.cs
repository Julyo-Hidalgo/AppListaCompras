using AppListaCompras.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaCompras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto produto_anexado = BindingContext as Produto;

                Produto p = new Produto
                {
                    id = produto_anexado.id,
                    desc = txt_desc.Text,
                    qnt = Convert.ToInt32(txt_qnt.Text),
                    preco = Convert.ToDouble(txt_preco.Text)
                };

                await App.Database.update(p);

                await DisplayAlert("Sucesso!", "Produto editado!", "OK");

                await Navigation.PushAsync(new View.Listagem());


            }
            catch (Exception ex)
            {
                DisplayAlert("Erro!", ex.Message, "OK");
            }
        }
    }
}