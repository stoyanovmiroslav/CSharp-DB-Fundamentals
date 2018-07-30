namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos.Import;
    using PetClinic.Models;

    public class Deserializer
    {
        public const string SUCCESS_MESSAGE = "Record successfully imported.";
        public const string ERROR_MESSAGE = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            List<AnimalAid> animalAids = JsonConvert.DeserializeObject<List<AnimalAid>>(jsonString);

            List<AnimalAid> validAnimalAids = new List<AnimalAid>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var animalAid in animalAids)
            {
                if (!IsValid(animalAid) || validAnimalAids.Any(x => x.Name == animalAid.Name))
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var isExist = context.AnimalAids.Any(x => x.Name == animalAid.Name);

                if (isExist)
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                validAnimalAids.Add(animalAid);
                stringBuilder.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(validAnimalAids);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var isoConvert = new IsoDateTimeConverter();
            isoConvert.DateTimeFormat = "dd-MM-yyyy";

            List<Animal> animals = JsonConvert.DeserializeObject<List<Animal>>(jsonString, isoConvert);

            List<Animal> validAnimals = new List<Animal>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var animal in animals)
            {
                bool isPassportValid = IsValid(animal.Passport);
                bool isAnimalValid = IsValid(animal);

                if (!isAnimalValid || !isPassportValid || validAnimals.Any(x => x.Passport.SerialNumber == animal.Passport.SerialNumber))
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var isExist = context.Passports.Any(x => x.SerialNumber == animal.PassportSerialNumber);

                if (isExist)
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                validAnimals.Add(animal);
                stringBuilder.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(validAnimals);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));

            var vetsDto = (VetDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder stringBuilder = new StringBuilder();
            List<Vet> vets = new List<Vet>();
            List<string> phoneNumbers = new List<string>();

            foreach (var vetDto in vetsDto)
            {
                if (IsValid(vetDto) && !phoneNumbers.Any(x => x == vetDto.PhoneNumber))
                {
                    var vet = Mapper.Map<Vet>(vetDto);
                    vets.Add(vet);
                    phoneNumbers.Add(vet.PhoneNumber);
                    stringBuilder.AppendLine($"Record {vet.Name} successfully imported.");
                }
                else
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }
        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));

            var proceduresDto = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder stringBuilder = new StringBuilder();

            List<Procedure> procedures = new List<Procedure>();

            foreach (var procedure in proceduresDto)
            {
                if (!IsValid(procedure))
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var vet = context.Vets.FirstOrDefault(x => x.Name == procedure.VetName);
                var animal = context.Animals.FirstOrDefault(x => x.PassportSerialNumber == procedure.AnimalSerialNumber);

                bool isDateTimeCorrect = DateTime.TryParseExact(procedure.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);

                if (vet == null || animal == null || !isDateTimeCorrect)
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                List<AnimalAid> animalAids = new List<AnimalAid>();
                bool isAnimalAidsExist = true;

                foreach (var procedureanimalAid in procedure.ProcedureAnimalAids)
                {
                    var animalAid = context.AnimalAids.FirstOrDefault(x => x.Name == procedureanimalAid.Name);

                    if (animalAid == null || animalAids.Any(x => x.Name == animalAid.Name))
                    {
                        isAnimalAidsExist = false;
                        break;
                    }

                    animalAids.Add(animalAid);
                }

                if (!isAnimalAidsExist)
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var procudure = new Procedure
                {
                    Vet = vet,
                    Animal = animal,
                    DateTime = dateTime,
                };

                foreach (var animalAid in animalAids)
                {
                    ProcedureAnimalAid procedureAnimalAid = new ProcedureAnimalAid
                    {
                        AnimalAid = animalAid,
                        Procedure = procudure
                    };

                    procudure.ProcedureAnimalAids.Add(procedureAnimalAid);
                }

                if (!IsValid(procudure))
                {
                    stringBuilder.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                procedures.Add(procudure);
                stringBuilder.AppendLine(SUCCESS_MESSAGE);
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}