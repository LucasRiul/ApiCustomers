using ApiCustomers.Data;
using ApiCustomers.Models;
using ApiCustomers.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiCustomers.Client
{
    public class ViaCepClient : IViaCepClient
    {
        private readonly HttpClient _http;
        public ViaCepClient(HttpClient http) { _http = http; }
        public async Task<ViaCepResponse> GetAddressAsync(string cep)
        {
            try
            {
                var response = await _http.GetAsync($"{cep}/json/");

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadFromJsonAsync<ViaCepResponse>();
                if (content == null) return null;

                // ViaCEP returns {"erro": true} for invalid CEPs
                return content.erro?.ToString().ToLower() == "true" ? null : content;
            }
            catch (Exception ex)
            {
                //Aqui podemos adicionar um notifiable para retornar ao usuário e um logger ao banco de dados
                return null;
            }
        }
    }
}
