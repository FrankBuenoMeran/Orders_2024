using Microsoft.AspNetCore.Components;
using Orders.FrontEnd.Repositorios;
using Orders.Shared.Entities;

namespace Orders.FrontEnd.Pages.Countries
{
    public partial class CountriesIndex
    {
        //inyectamos el repositorio 
        [Inject] private IRepository Repository { get; set; } = null!;
        public List<Country>? Countries { get; set; }
        //OnInitializedAsync, Cuando la pagina carga ejecuta automaticamente el codigo que esta dentro del metodo 
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //hacemos la peticion al repo
            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response;
        }


    }
}
