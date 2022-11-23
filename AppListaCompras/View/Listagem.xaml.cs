using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppListaCompras.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaCompras.View
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Produto> lista_produto = new ObservableCollection<Produto>();

        public Listagem()
        {
            InitializeComponent();
            lv_produto.ItemsSource = lista_produto;
        }

        private void tbi_somar_Clicked(object sender, EventArgs e)
        {
            double soma = lista_produto.Sum(i => i.qnt * i.preco);

            string msg = "O total da soma é: " + soma.ToString("F2");

            DisplayAlert("Soma total: ", msg, "Ok");
        }

        private void tbi_novo_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());

            }catch (Exception ex)
            {
                DisplayAlert("Ocorreu um erro!", ex.Message, "Ok");
            }
        }

        protected override void OnAppearing()
        {
            if (lista_produto.Count == 0)
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    List<Produto> temp = await App.Database.getAll();

                    foreach (Produto produto in temp)
                        lista_produto.Add(produto);

                    refresh_view.IsRefreshing = false;
                });
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;

            Produto produto_selecionado = (Produto) disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem certeza?", "Quer remover?", "sim", "não");

            if (confirmacao)
            {
                App.Database.delete(produto_selecionado.id);
                lista_produto.Remove(produto_selecionado);
            }
        }

        private void sb_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produto> temp = await App.Database.search(buscou);
                lista_produto.Clear();
                foreach (Produto produto in temp)
                    lista_produto.Add(produto);

                refresh_view.IsRefreshing = false;
            });
        }

        private void lv_produto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = (Produto)e.SelectedItem
            });
        }
    }
}