using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.FrontEnd.Repositorios;
using Orders.Shared.Entities;

namespace Orders.FrontEnd.Pages.Countries
{
    public partial class CountryCreate
    {
        //objeto que vamos a crear
        private Country country = new();

        //Representacion codigo blazor en codigo C#
        private CountryForm? countryForm;

        //inyectamos el repositorio(injesion de dependencia)
        [Inject] private IRepository repository { get; set;} = null!;

        //inyectamos el SweetAlert para avisar cuando se cre o no un pais
        [Inject] private SweetAlertService sweetAlertService { get; set; }=null!;

        //inyectamos NavigationManager para redireccionar
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        //Recordar que las inyecciones usadas aqui se encuentran en la clase program del proyecto Oders.FrontEnd
        //excepto NavigationManager, recordemos que este es propio del entity Framework, viene por defecto para inyectarlo
        //donde lo necesitemos.

        //Creamos el metodo para crear el pais
        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("/api/countries", country);

            if (responseHttp.Error)
            { 
                var message=await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error",message);
                return;
            }
            Return();

            var toast=sweetAlertService.Mixin(new SweetAlertOptions
            { 
                    Toast =true,
                    Position = SweetAlertPosition.BottomEnd,
                    ShowConfirmButton = true,
                    Timer=300
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success,message:"Registro creado con éxito");
        
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/countries");
        }
    }
}
