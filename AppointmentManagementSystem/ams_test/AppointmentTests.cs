using AppointmentManagementSystem.Logic;

namespace ams_test
{
    public class AppointmentTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AppointmentContructorValidInputCreatedAppiontment()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person};

            //Act and Assert
            Assert.DoesNotThrow(() => new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
        }

        [Test]
        public void AppointmentContructorInvalidStartInPastInputThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Appointment(person, invitedPeople, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(2)));
        }

        [Test]
        public void AppointmentContructorInvalidEndBeforeStartInputThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            //Act and Assert
            Assert.Throws<ArgumentException>(() => new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddHours(2)));
        }

        [Test]
        public void AppointmentContructorInvalidEndSameAsStartInputThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };
            DateTime dateTime = DateTime.Now.AddDays(1);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => new Appointment(person, invitedPeople, dateTime, dateTime));
        }

        [Test]
        public void StartAppointmentValidPendingStartedAppiontment()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            
            //Act
            appointment.StartAppointment();

            //Assert
            Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Started));
        }

        [Test]
        public void StartAppointmentValidAcceptedStartedAppiontment()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.AcceptAppointment(person2);

            //Act
            appointment.StartAppointment();

            //Assert
            Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Started));
        }

        [Test]
        public void StartAppointmentInvalidCancelledThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.CancelAppointment();

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.StartAppointment());
        }

        [Test]
        public void EndAppointmentValidInputEndedAppiontment()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            string summary = "All the main tasks were performed during this appointment with no special information to add.";

            //Act
            appointment.EndAppointment(summary);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Ended));
                Assert.That(summary, Is.EqualTo(appointment.Summary));
            });
        }

        [Test]
        public void EndAppointmentInvalidEndBeforeStartThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            
            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.EndAppointment("All the main tasks were performed during this appointment with no special information to add."));
        }

        [Test]
        public void EndAppointmentInvalidInputNoSummaryThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => appointment.EndAppointment(null));
        }

        [Test]
        public void EndAppointmentInvalidInputEmptySummaryThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => appointment.EndAppointment(""));
        }

        [Test]
        public void EndAppointmentInvalidInputShortSummaryThrows()
        {
            //Arrange
            Person person = new Person();
            List<Person> invitedPeople = new List<Person>() { person };

            Appointment appointment = new Appointment(person, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => appointment.EndAppointment("All good"));
        }

        [Test]
        public void AcceptAppointmentValidInputPendingAppointment()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person3 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2, person3 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act
            appointment.AcceptAppointment(person3);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Pending));
                Assert.That(appointment.PeopleAccepted, Is.EquivalentTo(new List<Person>() { person1, person3}));
                Assert.That(appointment.PeopleAccepted, Does.Not.Contain(person2));
            });
        }

        [Test]
        public void AcceptAppointmentValidInputAcceptedAppiontment()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act
            appointment.AcceptAppointment(person2);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Accepted));
                Assert.That(appointment.PeopleAccepted, Is.EquivalentTo(new List<Person>() { person1, person2 }));
            });
        }

        [Test]
        public void AcceptAppointmentInvalidAppiontmentCancelledThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.CancelAppointment();

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.AcceptAppointment(person2));
        }

        [Test]
        public void AcceptAppointmentInvalidAppiontmentStartedThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.AcceptAppointment(person2));
        }

        [Test]
        public void AcceptAppointmentInvalidNotInvitedThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person3 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act and Assert
            Assert.Throws<ArgumentException>(() => appointment.AcceptAppointment(person3));
        }

        [Test]
        public void AcceptAppointmentInvalidNullThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => appointment.AcceptAppointment(null));
        }

        [Test]
        public void AcceptAppointmentInvalidAlreadyAcceptedThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person3 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2, person3 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act and Assert
            Assert.Throws<ArgumentException>(() => appointment.AcceptAppointment(person1));
        }

        [Test]
        public void CancelAppointmentValidAppointmentCancelled()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            //Act
            appointment.CancelAppointment();

            //Arrange
            Assert.That(appointment.Status, Is.EqualTo(AppointmentStatus.Cancelled));
        }

        [Test]
        public void CancelAppointmentInvalidAppointmentEndedThrows()
        {
            //Arrange
            Person person1 = new Person()
            {
                Type = PersonType.Specialist,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            Person person2 = new Person()
            {
                Type = PersonType.Patient,
                KnownIdentificationNumber = Guid.NewGuid()
            };
            List<Person> invitedPeople = new List<Person>() { person1, person2 };

            Appointment appointment = new Appointment(person1, invitedPeople, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            appointment.StartAppointment();

            string summary = "All the main tasks were performed during this appointment with no special information to add.";

            appointment.EndAppointment(summary);

            //Act and Arrange
            Assert.Throws<InvalidOperationException>(() => appointment.CancelAppointment());
        }
    }
}