using AppointmentManagementSystem.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentManagementSystem.DataAccessLayer
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.AppointmentsInvitedFor).WithMany(a => a.PeopleInvited);
            builder.HasMany(p => p.AppointmentsAccepted).WithMany(a => a.PeopleAccepted);
            builder.HasMany(p => p.AppointmentCreated).WithOne(a => a.AppointmentCreatedBy);
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.KnownIdentificationNumber).IsRequired();
        }
    }
}