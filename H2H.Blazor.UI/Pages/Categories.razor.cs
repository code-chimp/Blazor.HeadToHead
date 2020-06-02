using System.Collections.Generic;
using System.Threading.Tasks;
using H2H.Models;

namespace H2H.Blazor.UI.Pages
{
    public partial class Categories
    {
        private List<Category> categories;
        private Category viewModel;
        private bool showEditDialog;

        protected override async Task OnInitializedAsync()
        {
            categories = (List<Category>) await @Service.Categories.GetAllAsync();
        }

        private void Add()
        {
            viewModel = new Category
            {
                Id = 0
            };

            showEditDialog = true;
        }

        private void Edit(Category category)
        {
            viewModel = new Category
            {
                Id = category.Id,
                Name = category.Name
            };

            showEditDialog = true;
        }

        private async Task Save()
        {
            showEditDialog = false;

            if (viewModel.Id == 0)
            {
                await @Service.Categories.AddAsync(viewModel);
            }
            else
            {
                await @Service.Categories.UpdateAsync(viewModel);
            }

            await @Service.SaveAsync();

            categories = (List<Category>) await @Service.Categories.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await @Service.Categories.RemoveAsync(id);
            @Service.Save();

            categories = (List<Category>) await @Service.Categories.GetAllAsync();
        }

        private void CloseModals()
        {
            showEditDialog = false;
            viewModel = null;
        }
    }
}
