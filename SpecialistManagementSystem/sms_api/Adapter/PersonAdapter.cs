using SpecialistManagementSystem.API.Models;
using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.API.Adapter
{
    public static class PersonAdapter
    {
        public static Person ConvertPersonDtoToPerson(PersonDTO personDTO)
        {
            if (personDTO.Type == PersonType.Specialist)
            {
                return new Specialist() { UserId = personDTO.KnownIdentificationNumber };
            }
            if (personDTO.Type == PersonType.Patient)
            {
                return new Patient() { UserId = personDTO.KnownIdentificationNumber };
            }
            throw new ArgumentNullException(nameof(personDTO), "The person is of invallid type!");
        }

        public static List<Person> CovertListPersonDtosToListOfPeople(List<PersonDTO> personDTOs)
        {
            List<Person> people = new List<Person>();
            personDTOs.ForEach(x => people.Add(ConvertPersonDtoToPerson(x)));
            return people;
        }
    }
}
