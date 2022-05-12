using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Election.MVC.Data;

namespace Election.MVC.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCanvasTypeAsync()
        {
            List<SelectListItem> list = await _context.CanvasTypes.Select(c => new SelectListItem
            {
                Text = c.Type,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Canvas Type...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboCanvasAsync()
        {
            List<SelectListItem> list = await _context.Canvas.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Canvas ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboCanvasByTypeAsync(int canvasTypeId)
        {
            List<SelectListItem> list = await _context.Canvas
                .Where(c => c.Type.Id == canvasTypeId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Canvas ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboCommentsAsync()
        {
            List<SelectListItem> list = await _context.Canvas.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Comment ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboConstituenciesAsync()
        {
            List<SelectListItem> list = await _context.Constituencies.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Constituency ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboDivisionAsync(int ConstituencyId)
        {
            List<SelectListItem> list = await _context.PollingDivisions
                .Where(c => c.Constituency.Id == ConstituencyId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Division ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboInterviewersAsync()
        {
            List<SelectListItem> list = await _context.Interviewers.Select(c => new SelectListItem
            {
                Text = c.FullName,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Interviewer ...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboPartyAsync()
        {
            List<SelectListItem> list = await _context.Parties.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Select Party ...", Value = "0" });
            return list;
        }


    }
}
