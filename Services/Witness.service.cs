using AutoMapper;
using carrinho_api.Data;
using carrinho_api.DTOs;
using carrinho_api.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace carrinho_api.Services
{
    public class WitnessService
    {
        private DataContext _context;
        private IMapper _mapper;

        public WitnessService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WitnessDTO>> GetAll()
        {
            var witnesses = await _context.Witness
                .Include(w => w.Local)
                .ToListAsync();

            witnesses.ToList().ConvertAll(witness =>
            {
                var publishers = JsonConvert.DeserializeObject<IEnumerable<string>>(witness.Publishers);
                witness.Publishers = String.Join(" & ", publishers);
                return witness;
            });

            var witnessesDTO = _mapper.Map<IEnumerable<WitnessDTO>>(witnesses);

            return witnessesDTO;
        }

        public async Task<WitnessDTO> FindById(int id)
        {
            var witness = await _context.Witness.FirstOrDefaultAsync(w => w.WitnessId == id);
            var witnessDTO = _mapper.Map<WitnessDTO>(witness);
            return witnessDTO;
        }

        public async Task<bool> Create(WitnessCreateDTO witnessDTO)
        {
            var witness = _mapper.Map<Witness>(witnessDTO);

            var publisherList = witnessDTO.Publishers.ToList().ConvertAll(publisher => publisher.Name);

            witness.Publishers = JsonConvert.SerializeObject(publisherList);

            _context.Witness.Add(witness);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Update(Witness witness)
        {
            _context.Witness.Update(witness);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var witness = await FindById(id);
            _context.Witness.Remove(witness);
            return _context.SaveChanges() > 0;
        }
    }
}
