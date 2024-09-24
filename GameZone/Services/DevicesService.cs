﻿
namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly ApplicationDbContext _context;
        public DevicesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetDevices()
        {
            return _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
