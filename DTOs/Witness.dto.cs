using carrinho_api.Entities;
using carrinho_api.Enums;

namespace carrinho_api.DTOs
{
    public class WitnessDTO
    {
        public DateTime Date { get; set; }
        public string Publishers { get; set; }
        public WitnessType WitnessType { get; set; }
        public int LocalId { get; set; }
    }

    public class WitnessCreateDTO
    {
        public DateTime Date { get; set; }
        public IEnumerable<PublisherDTO> Publishers { get; set; }
        public WitnessType WitnessType { get; set; }
        public int LocalId { get; set; }
    }
}
