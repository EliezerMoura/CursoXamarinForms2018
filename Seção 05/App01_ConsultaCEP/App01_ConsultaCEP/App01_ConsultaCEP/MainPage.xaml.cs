using System;
using Xamarin.Forms;
using App01_ConsultaCEP.Servico.Modelo;
using App01_ConsultaCEP.Servico;

namespace App01_ConsultaCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Validações
            if (IsValidCEP(CEP.Text))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(CEP.Text.Trim());

                    if (end != null)
                        RESULTADO.Text = string.Format("Endereço: {2} {3} {0}, {1}", end.Localidade, end.UF, end.Logradouro, end.Bairro);
                    else
                        RESULTADO.Text = "O endereço não foi encontrado para este CEP!";
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool IsValidCEP(string cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP Inválido! O CEP deve conter 8 caracteres", "OK");
                valido = false;
            }

            int NovoCEP = 0;

            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP Inválido! O CEP deve conter apenas números", "OK");
                valido = false;
            }


            return valido;
        }
    }
}
