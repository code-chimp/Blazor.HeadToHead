using System.Collections.Generic;
using System.Threading.Tasks;
using H2H.Models;

namespace H2H.Blazor.UI.Pages
{
    public partial class Publishers
    {
        private List<Publisher> publishers;
        private Publisher viewModel;
        private bool showEditDialog;

        protected override async Task OnInitializedAsync()
        {
            publishers = (List<Publisher>) await @Service.Publishers.GetAllAsync();
        }

        private void Add()
        {
            viewModel = new Publisher
            {
                Id = 0
            };

            showEditDialog = true;
        }

        private void Edit(Publisher publisher)
        {
            viewModel = new Publisher
            {
                Id = publisher.Id,
                Name = publisher.Name
            };

            showEditDialog = true;
        }

        private async Task Save()
        {
            showEditDialog = false;

            if (viewModel.Id == 0)
            {
                await @Service.Publishers.AddAsync(viewModel);
            }
            else
            {
                await @Service.Publishers.UpdateAsync(viewModel);
            }

            await @Service.SaveAsync();

            publishers = (List<Publisher>) await @Service.Publishers.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await @Service.Publishers.RemoveAsync(id);
            @Service.Save();

            publishers = (List<Publisher>) await @Service.Publishers.GetAllAsync();
        }

        private void CloseModals()
        {
            showEditDialog = false;
            viewModel = null;
        }
    }
}
