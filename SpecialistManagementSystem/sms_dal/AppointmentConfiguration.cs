using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.DataAccessLayer
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(a => a.PeopleInvited).WithMany(p => p.AppointmentsInvitedFor);
            builder.HasMany(a => a.PeopleAccepted).WithMany(p => p.AppointmentsAccepted);
            builder.HasOne(a => a.AppointmentCreatedBy).WithMany(p => p.AppointmentCreated).IsRequired();
            builder.Property(a => a.AppointmentStart).IsRequired();
            builder.Property(a => a.AppointmentEnd).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.Summary);
        }
    }
}