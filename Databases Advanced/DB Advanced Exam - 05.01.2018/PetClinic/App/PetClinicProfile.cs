namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.DataProcessor.Dtos.Export;
    using PetClinic.DataProcessor.Dtos.Import;
    using PetClinic.Models;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<Vet, VetDto>().ReverseMap();
            CreateMap<AnimalAid, DataProcessor.Dtos.Export.AnimalAidDto>().ReverseMap();
        }
    }
}
