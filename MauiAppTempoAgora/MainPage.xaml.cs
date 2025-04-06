using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                                $"Longitude: {t.lon} \n" +
                                                $"Nascer do Sol: {t.sunrise} \n" +
                                                $"Por do Sol: {t.sunset} \n" +
                                                $"Descrição do clima: {t.description} \n" +
                                                $"Velocidade do vento: {t.speed} \n" +
                                                $"Visibilidade: {t.visibility} \n" +
                                                $"Temp Máx: {t.temp_max} \n" +
                                                $"Temp Min: {t.temp_min} \n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada. Por favor, verifique o nome e tente novamente.";
                    }
                }
                else
                {
                    lbl_res.Text = "Por favor, preencha o nome da cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de Conexão", "Sem conexão com a internet. Verifique sua conexão e tente novamente.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            }
        }

    }

}
