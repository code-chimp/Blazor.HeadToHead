using H2H.Models;
using Microsoft.AspNetCore.Components;

namespace H2H.Blazor.UI.Pages
{
    public partial class BookDialog
    {
        [Parameter] public EventCallback Close { get; set; }
        [Parameter] public EventCallback Save { get; set; }
        [Parameter] public EventCallback Delete { get; set; }
        [Parameter] public Book vm { get; set; }
        [Parameter] public bool Show { get; set; }
        [Parameter] public RenderFragment HeaderContent { get; set; }
    }
}