using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppListaCompras.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaCompras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void tbi_salvar_Clicked(object sender, EventArgs e)
        {
            try{

                Produto produto = new Produto {
                    desc = txt_desc.Text,
                    qnt = int.Parse(txt_qnt.Text),
                    preco = Convert.ToDouble(txt_preco.Text)
                };

                await App.Database.insert(produto);

                await DisplayAlert("Sucesso!", "Produto adicionando!", "OK");

                Navigation.PushAsync(new View.Listagem());
                

            }catch (Exception ex){
                DisplayAlert("Erro!", ex.Message, "OK");
            }
        }
    }
}