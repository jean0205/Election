using Microsoft.AspNetCore.Mvc.Rendering;

namespace Election.MVC.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCanvasAsync();
        Task<IEnumerable<SelectListItem>> GetComboCanvasByTypeAsync(int canvasTypeId);
        Task<IEnumerable<SelectListItem>> GetComboCanvasTypeAsync();
        Task<IEnumerable<SelectListItem>> GetComboCommentsAsync();
        Task<IEnumerable<SelectListItem>> GetComboConstituenciesAsync();
        Task<IEnumerable<SelectListItem>> GetComboDivisionAsync(int ConstituencyId);
        Task<IEnumerable<SelectListItem>> GetComboInterviewersAsync();
        Task<IEnumerable<SelectListItem>> GetComboPartyAsync();

    }
}
