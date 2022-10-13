docker build -t larsvandriel/appointmentmanagementsystem -f ./AppointmentManagementSystem/ams_api/Dockerfile .
docker push larsvandriel/appointmentmanagementsystem
docker build -t larsvandriel/specialistmanagementsystem -f ./SpecialistManagementSystem/sms_api/Dockerfile .
docker push larsvandriel/specialistmanagementsystem