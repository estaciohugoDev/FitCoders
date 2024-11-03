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
        public List<Member>? Clients { get; private set; } = [];

        public Instructor(int id, string name, string cpf, string email, InstructorShift shift) :base(id)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Shift = shift;
        }

        public void AddClient(Member client)
        {
            Clients!.Add(client);
        }
    }
}