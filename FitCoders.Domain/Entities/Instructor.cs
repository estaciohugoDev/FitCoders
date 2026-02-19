using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    public sealed class Instructor : BaseEntity
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; } 
        public string Email { get; private set; }
        public InstructorShift Shift { get; private set; }

        private readonly List<Member> _clients = new();
        public IReadOnlyCollection<Member> Clients => _clients.AsReadOnly();
        public Instructor(Guid id, string name, string cpf, string email, InstructorShift shift) :base(id)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Shift = shift;
        }
        void AddClient(Member client)
        {
            ArgumentNullException.ThrowIfNull(client);

            if (_clients.Any(c => c.Id == client.Id)) throw new InvalidOperationException($"Client already assigned to Instructor {Name}.");

            _clients.Add(client);
        }
        void RemoveClient(Member client)
        {
            _clients.Remove(client);
        }
    }
}