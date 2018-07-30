namespace PetClinic.DataProcessor
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos.Export;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals.Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                                 .Select(x => new AnimalDto
                                 {
                                     AnimalName = x.Name,
                                     Age = x.Age,
                                     OwnerName = x.Passport.OwnerName,
                                     SerialNumber = x.PassportSerialNumber,
                                     RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy")
                                 })
                                 .OrderBy(x => x.Age)
                                 .ThenBy(x => x.SerialNumber)
                                 .ToArray();

            var jsonAnimals = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);

            return jsonAnimals;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures.OrderBy(x => x.DateTime).ThenBy(x => x.Animal.PassportSerialNumber).Select(x => new ProcedureDto
            {
                AnimalSerialNumber = x.Animal.PassportSerialNumber,
                DateTime = x.DateTime.ToString("dd-MM-yyyy"),
                OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                TotalPrice = x.ProcedureAnimalAids.Sum(e => e.AnimalAid.Price),
                AnimalAid = x.ProcedureAnimalAids.Select(p => new AnimalAidDto
                {
                    Name = p.AnimalAid.Name,
                    Price = p.AnimalAid.Price
                }).ToArray()
            }).ToArray();

            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));

            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            StringBuilder sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), procedures, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }
    }
}