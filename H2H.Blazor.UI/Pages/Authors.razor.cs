using System.Collections.Generic;
using System.Threading.Tasks;
using H2H.Models;

namespace H2H.Blazor.UI.Pages
{
    public partial class Authors
    {
        private List<Author> authors;
        private Author viewModel;
        private bool showEditDialog;

        protected override async Task OnInitializedAsync()
        {
            authors = (List<Author>) await @Service.Authors.GetAllAsync();
        }

        private void Add()
        {
            viewModel = new Author
            {
                Id = 0
            };

            showEditDialog = true;
        }

        private void Edit(Author author)
        {
            viewModel = new Author
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Location = author.Location
            };

            showEditDialog = true;
        }

        private async Task Save()
        {
            showEditDialog = false;

            if (viewModel.Id == 0)
            {
                await @Service.Authors.AddAsync(viewModel);
            }
            else
            {
                await @Service.Authors.UpdateAsync(viewModel);
            }

            await @Service.SaveAsync();

            authors = (List<Author>) await @Service.Authors.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await @Service.Authors.RemoveAsync(id);
            @Service.Save();

            authors = (List<Author>) await @Service.Authors.GetAllAsync();
        }

        private void CloseModals()
        {
            showEditDialog = false;
            viewModel = null;
        }
    }
}
