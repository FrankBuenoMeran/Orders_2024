using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Orders.Shared.Entities;

namespace Orders.FrontEnd.Pages.Countries
{
    public partial class CountryForm
    {
        private EditContext editContext=null!;

        [EditorRequired, Parameter] public Country Country { get; set; }=null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }//Crea un pais                                                                           
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; } //Cancela la Accion 
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;//injecta la libreria y pinta los mensaje bonitos   
        public bool FormPostedSuccessfully { get; set; }//esta propiedad dira si se ejecuto o no se ejecuto el formulario

        protected override void OnInitialized()
        {
            editContext = new(Country);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        { 
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            { 
                return;
            }

            var result =await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text  = "¿Deseas salir de la pagina y perder los cambios?",
                Icon  = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });

            var confirm = !string.IsNullOrEmpty( result.Value);
            if (confirm) 
            {
                return;
            }
            context.PreventNavigation();    
        
        }
    }
}
