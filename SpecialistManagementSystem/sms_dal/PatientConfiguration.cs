using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.DataAccessLayer
{
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.HasMany(p => p.TreadedBy).WithMany(s => s.PatientsTreating);
            builder.HasMany(p => p.AppointmentsInvitedFor).WithMany(a => a.PeopleInvited.Cast<Patient>());
            builder.HasMany(p => p.AppointmentsAccepted).WithMany(a => a.PeopleAccepted.Cast<Patient>());
            builder.HasOne(p => p.Address).WithMany().IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.PhysicalGender).IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.PersonalEmail).IsRequired();
        }
    }
}