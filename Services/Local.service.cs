using carrinho_api.Data;
using carrinho_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace carrinho_api.Services
{
    public class LocalService
    {
        private DataContext _context;

        public LocalService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Local>> GetAll()
        {
            var locals = await _context.Locals.ToListAsync();
            return locals;
        }

        public async Task<Local> FindById(int id)
        {
            var local = await _context.Locals.FirstOrDefaultAsync(l => l.LocalId == id);
            return local;
        }

        public async Task<bool> Create(Local local)
        {
            _context.Locals.Add(local);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Update(Local local)
        {
            _context.Locals.Update(local);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var local = await FindById(id);
            _context.Locals.Remove(local);
            return _context.SaveChanges() > 0;
        }
    }
}
