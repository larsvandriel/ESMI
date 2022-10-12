using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.DataAccessLayer
{
    internal class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder.HasKey(s => s.UserId);
            builder.HasMany(s => s.PatientsTreating).WithMany(p => p.TreadedBy);
            builder.HasMany(s => s.AppointmentCreated).WithOne(a => a.AppointmentCreatedBy);
            builder.Property(s => s.ListOfProfessions);
            builder.Property(s => s.WorkEmail);
            builder.Property(s => s.Deleted);


            builder.HasMany(s => s.AppointmentsInvitedFor).WithMany(a => a.PeopleInvited.Cast<Specialist>());
            builder.HasMany(s => s.AppointmentsAccepted).WithMany(a => a.PeopleAccepted.Cast<Specialist>());
            builder.HasOne(s => s.Address).WithMany().IsRequired();
            builder.Property(s => s.FirstName).IsRequired();
            builder.Property(s => s.LastName).IsRequired();
            builder.Property(s => s.PhysicalGender).IsRequired();
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.PhoneNumber).IsRequired();
            builder.Property(s => s.PersonalEmail).IsRequired();
        }
    }
}